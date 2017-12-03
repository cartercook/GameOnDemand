using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nut : MonoBehaviour {
    // reference to nut pile
    public Transform nutPile;

    Vector2 destination;

	// Use this for initialization
	void Start () {
        // random point on screen
        destination = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(40, Screen.width - 210), Random.Range(230, Screen.height - 250f)));
	}
	
	// Update is called once per frame
	void Update () {
        // move towards destination
        transform.position = Vector3.Lerp(transform.position, destination, 0.1f);

        // if touching nut pile
        if (Vector2.Distance(transform.position, nutPile.position) < 50)
        {
            Destroy(gameObject);
        }
	}

    public void GoToNutPile() {
        destination = nutPile.position;
    }
}
