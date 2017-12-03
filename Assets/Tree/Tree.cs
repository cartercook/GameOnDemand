using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public Object seed;

    public void DropSeeds() {
        // create seeds at position
        int num = Random.Range(1, 1);
        for (int i = 0; i < num; i++)
        {
            Instantiate(seed, transform.position, Quaternion.identity);
        }
    }
}
