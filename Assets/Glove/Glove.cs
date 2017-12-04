using UnityEngine;
using System.Collections;
using System.Threading;

public class Glove : MonoBehaviour
{
    public GameObject Bunny;

    public Vector3 init_position;
    // Use this for initialization
    new Collider2D collider;
    new SpriteRenderer renderer;

    public bool IsCountingDown = false;

    public Timer timer;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
        init_position = transform.position;
    }

    float currCountdownValue;

    public IEnumerator StartCountdown(float countdownValue = 1)
    {
        IsCountingDown = true;
        currCountdownValue = countdownValue;

        // hide glove
        renderer.enabled = false;

        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        // show glove
        renderer.enabled = true;

        LandTile tile = Controls.GetComponentAtPos<LandTile>(transform.position, "Tile");

        if (tile != null)
        {
            tile.ResetTile();

            Vector3 spawnPos = tile.transform.position;
            spawnPos.z = -1;
            Instantiate(Bunny, spawnPos, Quaternion.identity);
        }

        //return to original position
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
                transform.position = Vector3.Lerp(transform.position, init_position, 0.3f);
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

                        // a bunny obsructs your path!
                        Bunny bunny = Controls.GetComponentAtPos<Bunny>(transform.position, "Bunny");

                        if (tile != null && bunny == null && tile.watered && tile.status == LandTile.Status.ready)
                        {
                            transform.position = (Vector2)tile.transform.position;
                            tile.status = LandTile.Status.pulling;

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
