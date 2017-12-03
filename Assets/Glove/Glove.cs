using UnityEngine;
using System.Collections;
using System.Threading;

public class Glove : MonoBehaviour
{
    public Vector2 init_position;
    // Use this for initialization
    new Collider2D collider;

    public bool IsCountingDown = false;

    public Timer timer;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        init_position = transform.position;
    }

    float currCountdownValue;

    public IEnumerator StartCountdown(float countdownValue = 5)
    {
        IsCountingDown = true;

        currCountdownValue = countdownValue;

        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        LandTile tile = Controls.GetComponentAtPos<LandTile>(transform.position, "Tile");

        if (tile != null)
        {
            tile.growing_status = LandTile.GrowingStatus.not_planted;
            tile.status = LandTile.Status.dirt;

            // Put dirt sprite
        }

        while (Vector2.Distance(transform.position, init_position) > 10)
        {
            // Drop with Lerp
            if (Vector2.Distance(transform.position, init_position) < 2f)
            {
                transform.position = init_position;
                Controls.mode = Controls.Mode.gathering_seeds;
            }
            else
            {
                transform.position = Vector2.Lerp(transform.position, init_position, 0.3f);
            }

            //wait 1 frame
            yield return 0;
        }

        IsCountingDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        Touch[] touches = Controls.GetTouchesAndMouse();

        if (!IsCountingDown)
        {
            foreach (Touch touch in touches)
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                // check for click
                if (touch.phase == TouchPhase.Began && collider.OverlapPoint(touchPos))
                {
                    Controls.mode = Controls.Mode.pulling;
                }

                if (Controls.mode == Controls.Mode.pulling)
                {
                    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    {
                        // Drag
                        transform.position = touchPos;
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        LandTile tile = Controls.GetComponentAtPos<LandTile>(transform.position, "Tile");

                        if (tile != null && tile.status == LandTile.Status.watered && tile.growing_status == LandTile.GrowingStatus.ready)
                        {
                            StartCoroutine(StartCountdown());
                        }
                    }
                }
            }

            if (touches.Length <= 0)
            {
                // Drop with Lerp
                if (Vector2.Distance(transform.position, init_position) < 2f)
                {
                    transform.position = init_position;
                    Controls.mode = Controls.Mode.gathering_seeds;
                }
                else
                {
                    transform.position = Vector2.Lerp(transform.position, init_position, 0.3f);
                }
            }
        }
    }
}
