using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public Object seed;
    float maxChance = 1f;
    float chance;

    void Start()
    {
        chance = maxChance;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 0.5f);

        // increase chance by 0.01
        chance = Mathf.Min(chance + 0.01f, maxChance);
    }

    public void DropSeeds()
    {
        transform.localScale = Vector3.one * 1.2f;

        print(chance); 

        // random chance
        if (Random.value <= chance)
        {
            // create seed at position
            Instantiate(seed, transform.position, Quaternion.identity);

            // decrease chance
            chance = Mathf.Max(chance - 0.3f, 0f);
        }
    }
}
