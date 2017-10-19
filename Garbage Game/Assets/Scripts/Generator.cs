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

    List<GameObject> monitoredTrash = new List<GameObject>();
    GameObject deadTrash;

    [HideInInspector]
    public float fuel;

    SpriteRenderer sRend;
    Color defColor;

    void Start() {
        fuel = startingFuel;
        //sRend = GetComponent<SpriteRenderer>();
        //defColor = sRend.color;

        deadTrash = new GameObject();
    }

    void Update() {
        if (!isIncinerator)
        {
            fuel -= decayRate * Time.deltaTime;

            if (fuel <= 0f)
            {
                Debug.Log("OUT OF FUEL! GAME OVER!");
                fuel = 0f;
            }
        }

        if (isLocked)
        {
            lockTimer -= Time.deltaTime;
            if(lockTimer <= 0f)
            {
                GetComponent<Collider2D>().isTrigger = true;
                //sRend.color = defColor;
                isLocked = false;
            }
        }

        if(monitoredTrash.Count > 0)
        {
            for(int index = 0; index < monitoredTrash.Count; ++index)
            {
                if(monitoredTrash[index].layer != 12)
                {
                    TakeTrash(monitoredTrash[index]);
                    monitoredTrash[index] = deadTrash;
                }
            }

            //Will this work?
            monitoredTrash.RemoveAll(IsDead);
        }
    }

    bool IsDead(GameObject obj)
    {
        return obj == deadTrash;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!isLocked && (col.tag == "Trash" || col.tag == "Recycling"))
        {
            if (col.gameObject.layer != 12)
            {
                TakeTrash(col.gameObject);
            }
            else
            {
                MonitorTrash(col.gameObject);
            }
        }
    }

    void MonitorTrash(GameObject _trash)
    {
        monitoredTrash.Add(_trash);
    }

    void TakeTrash(GameObject _trash)
    {
        if (_trash.tag == "Trash")
        {
            if (!isIncinerator)
            {
                Lock();
            }
        }

        if (_trash.tag == "Recycling")
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

        Destroy(_trash.gameObject);
    }

    void Lock()
    {
        isLocked = true;
        lockTimer = lockDuration;
        GetComponent<Collider2D>().isTrigger = false;
        //sRend.color = new Color(defColor.r, defColor.g, defColor.b, 0.5f);
    }

    void GetFuel()
    {
        fuel += fuelPerTrash;
    }
}
