using System.Collections;using System.Collections.Generic;using UnityEngine;public class LandTile : MonoBehaviour{    public new Collider2D collider;    // Use this for initialization    void Start ()    {        collider = GetComponent<Collider2D>();        status = Status.dirt;
    }    public enum Status { dirt, tilled, planted, watered, sprouted };    public Status status;    // Update is called once per frame    void Update ()    {        if (Input.GetMouseButton(0))        {            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);            if (collider.OverlapPoint(touchPos) && Controls.mode == Controls.Mode.tilling)            {                TillTile();            }        }    }    public bool TillTile()    {        bool result = false;        if (status == Status.dirt)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();

            // Green
            renderer.color = new Color(0f, 167f, 8f, 255f);

            status = Status.tilled;

            result = true;
        }        return result;    }    public bool PlantTile()    {
        bool result = false;        if (status == Status.tilled)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();

            renderer.color = new Color(255f, 167f, 8f, 255f);

            status = Status.planted;

            result = true;
        }        return result;    }

    public bool WaterTile()    {
        bool result = false;        if (status == Status.planted)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();

            renderer.color = new Color(0f, 167f, 255f, 255f);

            status = Status.watered;

            result = true;
        }        return result;    }}