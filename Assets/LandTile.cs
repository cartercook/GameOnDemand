﻿using System.Collections;using System.Collections.Generic;using UnityEngine;public class LandTile : MonoBehaviour{    public new Collider2D collider;    // Use this for initialization    void Start ()    {        collider = GetComponent<Collider2D>();	}    public enum Status { dirt, tilled, planted, watered, sprouted };    public static Status status = Status.dirt;    // Update is called once per frame    void Update ()    {        if (Input.GetMouseButton(0))        {            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);            if (collider.OverlapPoint(touchPos) && Controls.mode == Controls.Mode.tilling)            {                TillTile();            }        }    }    public void TillTile()    {        SpriteRenderer renderer = GetComponent<SpriteRenderer>();        // Green        renderer.color = new Color(0f, 167f, 8f, 255f);        status = Status.tilled;    }    public void PlantTile()    {        SpriteRenderer renderer = GetComponent<SpriteRenderer>();        renderer.color = new Color(0f, 167f, 255f, 255f);        status = Status.planted;    }}