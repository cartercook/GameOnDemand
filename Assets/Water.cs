using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    new SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Controls.mode == Controls.Mode.watering)
        {
            renderer.enabled = true;
            LandTile tile = Controls.GetComponentAtPos<LandTile>(transform.position, "Tile");

            if (tile != null)
            {
                tile.WaterTile();
            }
        }
        else
        {
            renderer.enabled = false;
        }
	}
}
