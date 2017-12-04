using System.Collections;using System.Collections.Generic;using UnityEngine;public class LandTile : MonoBehaviour{    public new Collider2D collider;    // Use this for initialization    void Start ()    {        collider = GetComponent<Collider2D>();        status = Status.untilled;
    }    public bool IsCountingDown = false;    public bool watered = false;
    public enum Status { untilled, tilled, planted, sprouted, ready, pulling };    public Status status;

    float currCountdownValue;

    public IEnumerator StartCountdown(float countdownValue = 5)
    {
        IsCountingDown = true;

        if (status != Status.ready)
        {
            currCountdownValue = countdownValue;

            while (currCountdownValue > 0)
            {
                Debug.Log("Countdown: " + currCountdownValue);
                yield return new WaitForSeconds(1.0f);
                currCountdownValue--;
            }
            if (status == Status.tilled || status == Status.planted || status == Status.sprouted)            {
                status++;            }


            print("Growing Status: " + status);

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
    void Update ()    {        foreach (Touch touch in Controls.GetTouchesAndMouse())        {            if (touch.phase == TouchPhase.Moved)
            {                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                if (collider.OverlapPoint(touchPos))
                {
                    if (Controls.mode == Controls.Mode.tilling)
                    {                        Bunny bunny = Controls.GetComponentAtPos<Bunny>(touchPos, "Bunny");                        if (bunny == null)                        {
                            TillTile();                        }
                    }
                }
            }        }    }    public bool TillTile()    {        if (status == Status.untilled)
        {
            status = Status.tilled;

            return true;
        }        return false;    }    public bool PlantTile()    {        if (status == Status.tilled)
        {
            status = Status.planted;
            if (watered)            {                // grow                StartCoroutine(StartCountdown());            }
            return true;
        }        return false;    }

    public bool WaterTile()    {        if (!watered)        {            // water
            watered = true;            GetComponent<SpriteRenderer>().color = Color.cyan;
            if (status == Status.planted)            {                // grow
                StartCoroutine(StartCountdown());            }

            return true;
        }        return false;    }    public void ResetTile()    {        watered = false;        GetComponent<SpriteRenderer>().color = Color.white;        status = Status.untilled;    }}