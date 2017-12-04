using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public GameObject seed;
    float maxChance = 1f;
    float chance;

    void Start()
    {
        chance = maxChance;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 15 * Time.deltaTime);

        // increase by one every 10 seconds
        chance = Mathf.Min(chance + Time.deltaTime/100, maxChance);
    }

    public void DropSeeds()
    {
        transform.localScale = Vector3.one * 1.2f;

        print(chance);

        // random chance
        if (Random.value <= chance)
        {
            // create seed at position
            Instantiate(seed, transform.position, Quaternion.identity).GetComponent<Seed>().enabled = true;

            // decrease chance
            chance /= 2;
        }
    }
}
