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

            print("Tile: " + tile);

            if (tile != null)
            {
                print("Tile: " + tile.status);

                if (tile.status == LandTile.Status.tilled)
                {
                    // make seed smaller until it disappears
                    transform.localScale -= new Vector3(0.07f, 0.07f, 0);

                    print("Tile: " + tile.status);

                    print("Tile: " + transform.position);

                    print("Tile x: " + transform.localScale.x);
                    print("Tile y: " + transform.localScale.y);

                    if (transform.localScale.x < 0.1f && transform.localScale.y < 0.1f)
                    {
                        print("destroy1");
                        tile.PlantTile();
                        Controls.mode = Controls.Mode.gathering_seeds;
                        Destroy(gameObject);
                    }
                }
                else
                {
                    print("destroy2");
                    Controls.mode = Controls.Mode.gathering_seeds;
                    Destroy(gameObject);
                }
            }
        }
    }
}
