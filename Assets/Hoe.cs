using UnityEngine;
using UnityEditor;

public class Hoe : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Controls.Instance.IsDigging)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
            {
                if (Vector2.Distance(touchPos, tile.transform.position) <= 100)
                {
                    tile.GetComponent<LandTile>().DigTile();
                }
            }
        }
    }

    public GameObject Spade { get; set; }
}