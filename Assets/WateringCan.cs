using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public Vector3 init_position;
    new Collider2D collider;        
    // Use this for initialization
    void Start()
    {
        init_position = transform.position;
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // check for click
        if (Input.GetMouseButtonDown(0) && collider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))        {            Controls.mode = Controls.Mode.watering;        }

        if (Controls.mode == Controls.Mode.watering)        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);            if (Input.GetMouseButton(0))            {
                // Drag
                transform.position = new Vector3(touchPos.x, touchPos.y, 0.0f);            }            else            {
                // Drop with Lerp
                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(init_position.x, init_position.y)) < 2f)                {                    transform.position = init_position;                    Controls.mode = Controls.Mode.gathering_seeds;                }                else                {                    transform.position = Vector2.Lerp(transform.position, init_position, 0.3f);                }            }        }
    }
}
