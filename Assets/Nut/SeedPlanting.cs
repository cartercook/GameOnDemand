using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPlanting : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Controls.mode == Controls.Mode.planting)
        {
            foreach (Touch touch in Controls.GetTouchesAndMouse())
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    // Drag
                    transform.position = touchPos;
                }
                else
                {
                    LandTile tile = Controls.GetComponentAtPos<LandTile>(transform.position, "Tile");

                    print("Tile: " + tile);

                    if (tile != null)
                    {
                        if (tile.status == LandTile.Status.tilled)
                        {
                            transform.position = (Vector2)tile.transform.position;

                            // make seed smaller until it disappears
                            transform.localScale -= new Vector3(0.07f, 0.07f, 0);

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
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
	}
}
