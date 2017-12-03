using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandTile : MonoBehaviour
{
    new Collider2D collider;

    // Use this for initialization
    void Start () {
        collider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (collider.OverlapPoint(touchPos))
            {
                TillTile();
            }
        }
    }

    public void TillTile()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        // Green
        renderer.color = new Color(0f, 167f, 8f, 255f);
    }
}
