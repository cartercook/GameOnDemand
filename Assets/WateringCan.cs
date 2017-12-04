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
        Touch[] touches = Controls.GetTouchesAndMouse();        foreach (Touch touch in touches)        {            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            // check for click
            if (touch.phase == TouchPhase.Began && collider.OverlapPoint(touchPos))
            {
                Controls.mode = Controls.Mode.watering;
            }

            print("Mode: " + Controls.mode);

            if (Controls.mode == Controls.Mode.watering)            {
                print("watering");

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {                    print("dragging");
                    // Drag
                    transform.position = touchPos;
                }
            }
        }

        if (touches.Length <= 0)
        {
            print("dropping");
            // Drop with Lerp
            if (Vector2.Distance(transform.position, init_position) < 2f)
            {
                transform.position = init_position;
                Controls.mode = Controls.Mode.gathering_seeds;
            }
            else
            {
                transform.position = Vector2.Lerp(transform.position, init_position, 0.3f);
            }
        }
    }
}
