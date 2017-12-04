using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPlanting : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        print("mode: " + Controls.mode);

        Touch[] touches = Controls.GetTouchesAndMouse();

        if (Controls.mode == Controls.Mode.planting)
        {
            print("touches: " + touches.Length);

            foreach (Touch touch in touches)
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    // Drag
                    transform.position = touchPos;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    LandTile tile = Controls.GetComponentAtPos<LandTile>(transform.position, "Tile");

                    if (tile != null && tile.status == LandTile.Status.tilled)
                    {
                        transform.position = (Vector2)tile.transform.position;
                    }
                }
            }
        }

        if (touches.Length <= 0)
        {
            LandTile tile = Controls.GetComponentAtPos<LandTile>(transform.position, "Tile");

            if (tile != null)
            {
                Bunny bunny = Controls.GetComponentAtPos<Bunny>(transform.position, "Bunny");

                if (tile.status == LandTile.Status.tilled && bunny == null)
                {
                    // make seed smaller until it disappears
                    transform.localScale -= new Vector3(0.1f, 0.1f, 0);

                    if (transform.localScale.x < 0.1f && transform.localScale.y < 0.1f)
                    {
                        tile.PlantTile();
                        Controls.mode = Controls.Mode.gathering_seeds;
                        Destroy(gameObject);
                    }
                }
                else
                {
                    Controls.mode = Controls.Mode.gathering_seeds;

                    // drop seed on ground
                    Seed seed = GetComponent<Seed>();
                    seed.Reset();

                    seed.enabled = true;
                    this.enabled = false;
                }
            }
        }
    }
}
