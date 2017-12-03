﻿using System.Collections;
    }

    public enum GrowingStatus { not_planted, planted, sprouted, bigger, ready };

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

            StartCoroutine(StartCountdown());
            // Display right sprite
        }
        else
        {
            // Display Ready sprite
        }

        IsCountingDown = false;
    }

    // Update is called once per frame
    void Update ()
            {

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
            }
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();

            // Green
            renderer.color = new Color(0f, 167f, 8f, 255f);

            status = Status.tilled;

            result = true;
        }
        bool result = false;
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();

            renderer.color = new Color(255f, 167f, 8f, 255f);

            status = Status.planted;
            growing_status = GrowingStatus.planted;

            StartCoroutine(StartCountdown());

            result = true;
        }

    public bool WaterTile()
        bool result = false;
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();

            renderer.color = new Color(0f, 167f, 255f, 255f);

            status = Status.watered;

            result = true;
        }