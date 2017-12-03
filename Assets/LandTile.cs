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
		
	}

    public void DigTile()
    {
        var tile = this.gameObject;

        var sprite = tile.GetComponent<SpriteRenderer>();

        // Green
        sprite.color = new Color(0f, 167f, 8f, 255f);
    }
}
