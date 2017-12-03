using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandTile : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0) && Controls.Instance.mode == Controls.Mode.tilling)        {            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);            GameObject tile = this.gameObject;
            Collider2D collider = Controls.GetComponentAtPos<Collider2D>(touchPos, "Tile");

            if (collider != null)            {
                tile.GetComponent<LandTile>().TillTile();            }        }
    }

    public void TillTile()
    {
        var tile = this.gameObject;

        var sprite = tile.GetComponent<SpriteRenderer>();

        // Green
        sprite.color = new Color(0f, 167f, 8f, 255f);
    }
}
