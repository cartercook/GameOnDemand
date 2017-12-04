﻿using UnityEngine;
using System.Collections;
using System.Threading;

public class Glove : MonoBehaviour
{
    public Vector2 init_position;
    // Use this for initialization
    new Collider2D collider;    public bool IsCountingDown = false;    public bool GloveIsOnTile = false;    public Timer timer;    void Start()    {        collider = GetComponent<Collider2D>();        init_position = transform.position;    }

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

        IsCountingDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        // check for click
        if (Input.GetMouseButtonDown(0) && collider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))        {            Controls.mode = Controls.Mode.pulling;        }

        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);        if (Controls.mode == Controls.Mode.pulling)        {            if (Input.GetMouseButton(0))            {
                // Drag
                transform.position = new Vector3(touchPos.x, touchPos.y, 0.0f);            }            else if (!IsCountingDown)            {
                GloveIsOnTile = false;

                foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
                {
                    LandTile land_tile = tile.GetComponent<LandTile>();

                    if (land_tile.collider.OverlapPoint(transform.position))
                    {
                        GloveIsOnTile = true;
                    }
                }

                if (GloveIsOnTile)
                {
                    StartCoroutine(StartCountdown());
                }
                else
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
            }        }
    }
}
