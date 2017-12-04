using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

    // 0 = none
    // 1 = hole
    // 2 = sprout stage 1
    // 3 = sprout stage 2
    public Sprite[] sprites;

    new SpriteRenderer renderer;
    Animator animator;
    LandTile tile;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        tile = transform.parent.GetComponent<LandTile>();

        transform.rotation = Quaternion.identity;
        transform.position = (Vector2)tile.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (tile.status == LandTile.Status.pulling)
        {
            animator.enabled = true;
            animator.Play("Glove Pull");
        }
        else if (tile.status == LandTile.Status.ready)
        {
            animator.enabled = true;
            animator.Play("Ready Sprout");
        }
        else
        {
            animator.enabled = false;

            if ((int)tile.status <= 3)
            {
                renderer.sprite = sprites[(int)tile.status];
            }
        }
	}
}
