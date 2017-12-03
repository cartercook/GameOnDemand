using System.Collections;using System.Collections.Generic;using UnityEngine;public class LandTile : MonoBehaviour{    public new Collider2D collider;    // Use this for initialization    void Start ()    {        collider = GetComponent<Collider2D>();        status = Status.dirt;
    }    public bool IsCountingDown = false;    public enum Status { dirt, tilled, planted, watered };    public Status status;

    public enum GrowingStatus { not_planted, planted, sprouted, bigger, ready };    public GrowingStatus growing_status;

    float currCountdownValue;

    public IEnumerator StartCountdown(float countdownValue = 5)
    {
        IsCountingDown = true;

        if (growing_status != GrowingStatus.ready)
        {
            currCountdownValue = countdownValue;

            while (currCountdownValue > 0)
            {
                Debug.Log("Countdown: " + currCountdownValue);
                yield return new WaitForSeconds(1.0f);
                currCountdownValue--;
            }

            growing_status++;


            print("Growing Status: " + growing_status);
            // Display right sprite
        }

        IsCountingDown = false;
    }

    // Update is called once per frame
    void Update ()    {        foreach (Touch touch in Controls.GetTouchesAndMouse())        {            if (touch.phase == TouchPhase.Moved)
            {                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                if (collider.OverlapPoint(touchPos))
                {
                    if (Controls.mode == Controls.Mode.tilling)
                    {
                        TillTile();
                    }
                    else if (Controls.mode == Controls.Mode.watering)
                    {
                        WaterTile();
                    }
                }
            }        }    }    public bool TillTile()    {        bool result = false;        if (status == Status.dirt)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();

            // Green
            renderer.color = new Color(0f, 167f, 8f, 255f);

            status = Status.tilled;

            result = true;
        }        return result;    }    public bool PlantTile()    {
        bool result = false;        if (status == Status.tilled)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();

            renderer.color = new Color(255f, 167f, 8f, 255f);

            status = Status.planted;
            growing_status = GrowingStatus.planted;

            StartCoroutine(StartCountdown());

            result = true;
        }        return result;    }

    public bool WaterTile()    {
        bool result = false;        if (status == Status.planted)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();

            renderer.color = new Color(0f, 167f, 255f, 255f);

            status = Status.watered;

            result = true;
        }        return result;    }}