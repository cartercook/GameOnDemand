using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
    public static Controls Instance;

    public Collider2D hoe;
    public Collider2D seedPile;
    public Collider2D wateringCan;
    public Collider2D glove;

    public bool IsDigging = false;

    public enum Mode { gathering_seeds, tilling, planting, watering, pulling };
    public Mode mode = Mode.gathering_seeds;

    void Start()
    {
        // singleton
        Instance = this;
    }
    	
	// Update is called once per frame
	void Update ()
    {
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // detect tree at touch point
                Tree tree = GetComponentAtPos<Tree>(touchPos, "Tree");
                hoe = GetComponentAtPos<Collider2D>(touchPos, "Hoe");

                if (tree != null && mode == Mode.gathering_seeds)
                {
                    tree.DropSeeds();
                }
                else if (hoe != null && hoe.OverlapPoint(touchPos))
                {
                    if (mode == Mode.gathering_seeds)
                        mode = Mode.tilling;
                    else
                        mode = Mode.gathering_seeds;
                }
                else
                {
                    CollectNutsAtPosition(touchPos);
                }
            }
        }
	}

    void CollectNutsAtPosition(Vector2 position)
    {
        foreach (GameObject nut in GameObject.FindGameObjectsWithTag("Nut"))
        {
            if (Vector2.Distance(position, nut.transform.position) <= 150)
            {
                nut.GetComponent<Nut>().GoToNutPile();
            }
        }
    }

    public static T GetComponentAtPos<T>(Vector2 position, string layerName) where T:Component
    {
        T component = null;

        // detect object at touch point
        Collider2D collider = Physics2D.OverlapPoint(position, ~LayerMask.NameToLayer(layerName));

        if (collider != null)
        {
            component = collider.GetComponent<T>();
        }

        return component;
    }
}
