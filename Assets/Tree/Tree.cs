using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public Object seed;

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 0.5f);
    }

    public void DropSeeds()
    {
        transform.localScale = Vector3.one * 1.2f;

        // create seeds at position
        int num = Random.Range(1, 1);

        for (int i = 0; i < num; i++)
        {
            Instantiate(seed, transform.position, Quaternion.identity);
        }
    }
}
