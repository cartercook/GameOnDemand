﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour {
    // reference to nut pile
    public Transform nutPile;

    Vector2 destination;
    float rotation;
    float destRotation;

	// Use this for initialization
	void Start () {
        // random point on screen
        destination = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(40, Screen.width - 210), Random.Range(60, Screen.height - 250f)));

        //random rotation
        rotation = Random.Range(-180f, 180f);
        // random rotation, dependent on distance travelled
        destRotation = Mathf.Sign(Random.Range(-1f, 1f)) * Vector2.Distance(transform.position, destination) * 3;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Controls.mode == Controls.Mode.gathering_seeds)
        {
            // move towards destination
            // rotate
            rotation = Mathf.Lerp(rotation, destRotation, 0.1f);
            transform.rotation = Quaternion.Euler(0, 0, rotation);

            transform.position = Vector3.Lerp(transform.position, destination, 0.1f);

            // if touching nut pile
            if (Vector2.Distance(transform.position, nutPile.position) < 50)
            {
                Destroy(gameObject);

                GameObject nut_pile_object = GameObject.FindGameObjectWithTag("NutPile");

                SeedPile seed_pile = nut_pile_object.GetComponent<SeedPile>();

                seed_pile.quantity++;
            }
        }
	}

    public void GoToNutPile()
    {
        destination = nutPile.position;
    }
}