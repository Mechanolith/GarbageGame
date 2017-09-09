using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public bool isIncinerator;

    public float startingFuel = 60f;
    public float decayRate = 1f;
    public float maxFuel = 100f;
    public float fuelPerTrash = 4f;
    
    public float lockDuration;
    float lockTimer;
    bool isLocked;

    float fuel;

    SpriteRenderer sRend;
    Color defColor;

    void Start() {
        fuel = startingFuel;
        sRend = GetComponent<SpriteRenderer>();
        defColor = sRend.color;
    }

    void Update() {
        if (!isIncinerator)
        {
            fuel -= decayRate * Time.deltaTime;

            if (fuel <= 0f)
            {
                Debug.Log("OUT OF FUEL! GAME OVER!");
            }
        }

        if (isLocked)
        {
            lockTimer -= Time.deltaTime;
            if(lockTimer <= 0f)
            {
                GetComponent<Collider2D>().isTrigger = true;
                sRend.color = defColor;
                isLocked = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!isLocked)
        {
            if (col.tag == "Trash")
            {
                if (!isIncinerator)
                {
                    Lock();
                }
            }

            if (col.tag == "Recycling")
            {
                if (isIncinerator)
                {
                    Lock();
                }
                else
                {
                    GetFuel();
                }
            }

            Destroy(col.gameObject);
        }
    }

    void Lock()
    {
        isLocked = true;
        lockTimer = lockDuration;
        GetComponent<Collider2D>().isTrigger = false;
        sRend.color = new Color(defColor.r, defColor.g, defColor.b, 0.5f);
    }

    void GetFuel()
    {
        fuel += fuelPerTrash;
    }
}
