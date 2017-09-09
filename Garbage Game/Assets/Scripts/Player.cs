using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 150f;
    public float grabRadius = 1.5f;
    public float grabRange = 3f;

    Rigidbody2D rBody = null;
    Vector2 moveVector = Vector2.zero;
    Vector2 facing = Vector2.right;
    bool isHolding = false;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        moveVector = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveVector.y = 1f;
            facing = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveVector.y = -1f;
            facing = -Vector2.up;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveVector.x = 1f;
            facing = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveVector.x = -1f;
            facing = -Vector2.right;
        }

        rBody.velocity = moveVector.normalized * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isHolding)
            {
                Pickup();
            }
            else
            {
                Throw();
            }
        }

        GrabDebug();
    }

    void Pickup()
    {
        RaycastHit2D rayHit = new RaycastHit2D();
        LayerMask mask = 1 << 9; //Only hit the trash layer.
        rayHit = Physics2D.CircleCast(transform.position, grabRadius, facing, grabRange, mask);

        Debug.Log("RAY HIT " + rayHit.point);
    }

    void GrabDebug()
    {
        Vector3 debugDir = new Vector3(facing.x, facing.y, 0f);
        Vector3 debugSide = new Vector3(facing.y, facing.x, 0f);
        Debug.DrawLine(transform.position - (debugDir * grabRadius), transform.position + (debugDir * (grabRadius + grabRange)));
        Debug.DrawLine(transform.position + (debugSide * grabRadius), transform.position + (debugSide * grabRadius) + (debugDir * (grabRange)));
        Debug.DrawLine(transform.position + (-debugSide * grabRadius), transform.position + (-debugSide * grabRadius) + (debugDir * (grabRange)));
    }

    void Throw()
    {

    }
}
