using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : MonoBehaviour {

    bool jumping = false;
    int nonVisibleCount = 0;
    float vel;
    float accel;
    float originalY;

    Animator animator;
    new SpriteRenderer renderer;
    new Collider2D collider;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();

        //random orientation
        renderer.flipX = Random.value > 0.5;

        StartJump();
	}
	
	// Update is called once per frame
	void Update () {
        if (jumping)
        {
            // simulate gravity
            vel += accel;
            Vector3 newPos = transform.position;

            newPos.x += renderer.flipX ? 10 : -10; // horizontal speed

            if (newPos.y + vel < originalY)
            {
                jumping = false;
                newPos.y = originalY;
            }
            else
            {
                newPos.y += vel;
            }

            transform.position = newPos;
        }
        else
        {
            // detect if rabbit is offscreen

            if (renderer.isVisible)
            {
                nonVisibleCount = 0;
            }
            else
            {
                nonVisibleCount++;
            }

            if (nonVisibleCount > 60)
            {
                Destroy(gameObject);
            }

            // around once every 10 seconds
            if (Random.value < Time.deltaTime / 10)
            {
                // face other direction
                renderer.flipX = !renderer.flipX;
            }
            else if (Random.value < Time.deltaTime / 10)
            {
                StartJump();
            }

            foreach (Touch touch in Controls.GetTouchesAndMouse())
            {
                if (touch.phase == TouchPhase.Began && collider.OverlapPoint(Camera.main.ScreenToWorldPoint(touch.position)))
                {
                    StartJump();
                }
            }
        }
	}

    public void StartJump()
    {
        jumping = true;
        //animator.enabled = false;

        vel = 50;
        accel = -5;
        originalY = transform.position.y;
    }


}
