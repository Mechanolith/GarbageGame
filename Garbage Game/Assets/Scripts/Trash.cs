﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {

    public enum TrashType
    {
        None,
        RedTrash,
        OrangeTrash
    }

    public enum RecycleType
    {
        None,
        BlueRecycle,
        GreenRecycle
    }

    public bool isRecyclable;
    public TrashType trashType = TrashType.None;
    public RecycleType recType = RecycleType.None;

    float airTimer;
    bool throwing;
    Vector2 throwVec;

    Rigidbody2D rBody;

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (throwing)
        {
            rBody.velocity = throwVec * Time.deltaTime;
            airTimer -= Time.deltaTime;
            if(airTimer <= 0f)
            {
                throwing = false;
                gameObject.layer = 9;
            }
        }
    }

    public void Throw(Vector2 direction, float speed, float duration)
    {
        throwVec = direction * speed;
        airTimer = duration;
        throwing = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (throwing)
        {
            if (collision.gameObject.layer == 11)
            {
                airTimer = 0.04f;
            }
        }
    }
}
