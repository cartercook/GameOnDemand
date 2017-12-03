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

                if (touch.phase == TouchPhase.Moved)
                {
                    // Drag
                    transform.position = touchPos;
                }
                else
                {
                    LandTile tile = Controls.GetComponentAtPos<LandTile>(transform.position, "Tile");

                    if (tile != null)
                    {
                        if (tile.status == LandTile.Status.tilled)
                        {
                            transform.position = tile.transform.position;

                            // make seed smaller until it disappears
                            transform.localScale -= new Vector3(0.07f, 0.07f, 0);

                            if (transform.localScale.x < 0.1f && transform.localScale.y < 0.1f)
                            {
                                tile.PlantTile();
                                Controls.mode = Controls.Mode.gathering_seeds;
                                Destroy(gameObject);
                            }
                        }
                        else
                        {
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
	}
}
