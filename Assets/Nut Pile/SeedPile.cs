using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPile : MonoBehaviour
{
    public Object seed;

    public int quantity;

    new Collider2D collider;
    // Use this for initialization
    void Start ()
    {
        collider = GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0) && collider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))        {            if (quantity > 0)
            {
                Controls.mode = Controls.Mode.planting;

                Instantiate(seed, transform.position, Quaternion.identity);

                quantity--;

            }
        }
    }
}
