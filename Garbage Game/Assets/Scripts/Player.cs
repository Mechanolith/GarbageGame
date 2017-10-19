using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 150f;
    public float grabRadius = 1.5f;
    public float grabRange = 3f;
    public GameObject holdPoint = null;
    public GameObject spriteObject = null;
    public float throwForce = 25f;
    public float airTime = 2f;

    Rigidbody2D rBody = null;
    Vector2 moveVector = Vector2.zero;
    Vector2 facing = Vector2.right;
    bool isHolding = false;
    Transform heldObject = null;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.inst.state == GameState.e_Game)
        {
            moveVector = Vector2.zero;
            Quaternion sRot = Quaternion.identity;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                moveVector.y = 1f;
                facing = Vector2.up;
                sRot.eulerAngles = new Vector3(0f, 0f, 180f);
                spriteObject.transform.rotation = sRot;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                moveVector.y = -1f;
                facing = -Vector2.up;
                spriteObject.transform.rotation = Quaternion.identity;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                moveVector.x = 1f;
                facing = Vector2.right;
                sRot.eulerAngles = new Vector3(0f, 0f, 90f);
                spriteObject.transform.rotation = sRot;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                moveVector.x = -1f;
                facing = -Vector2.right;
                sRot.eulerAngles = new Vector3(0f, 0f, 270f);
                spriteObject.transform.rotation = sRot;
            }

            rBody.velocity = moveVector.normalized * moveSpeed * Time.deltaTime;

            if (isHolding)
            {
                heldObject.position = holdPoint.transform.position;
            }

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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.inst.ToMenu();
            }

            GrabDebug();
        }
        else
        {
            moveVector = Vector2.zero;
            rBody.velocity = Vector2.zero;
        }
    }

    void Pickup()
    {
        RaycastHit2D rayHit = new RaycastHit2D();
        LayerMask mask = 1 << 9; //Only hit the trash layer.
        rayHit = Physics2D.CircleCast(transform.position, grabRadius, facing, grabRange, mask);

        if(rayHit.transform != null)
        {
            rayHit.collider.enabled = false;
            rayHit.transform.position = holdPoint.transform.position;
            heldObject = rayHit.transform;
            isHolding = true;
        }

        //Debug.Log("RAY HIT " + rayHit.point);
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
        heldObject.gameObject.layer = 12;
        heldObject.GetComponent<Collider2D>().enabled = true;
        heldObject.GetComponent<Trash>().Throw(facing, throwForce, airTime);
        isHolding = false;
        heldObject = null;
    }
}
