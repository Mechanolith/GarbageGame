using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public bool isIncinerator;

    public float startingFuel = 60f;
    public float initialDecay = 1f;
    public float decayIncrease = 0.01f;
    public float maxFuel = 100f;
    public float fuelPerTrash = 4f;

    public float lockDuration;
    float lockTimer;
    bool isLocked;

    List<GameObject> monitoredTrash = new List<GameObject>();
    GameObject deadTrash;

    [HideInInspector]
    public float fuel;
    float decayRate;

    SpriteRenderer sRend;
    Color defColor;

    TrashGod tGod;

    void Start() {
        fuel = startingFuel;
        decayRate = initialDecay;
        //sRend = GetComponent<SpriteRenderer>();
        //defColor = sRend.color;

        deadTrash = new GameObject();

        tGod = GameObject.Find("Trash God").GetComponent<TrashGod>();
    }

    void Update() {
        if (!isIncinerator)
        {
            if (GameManager.inst.state == GameState.e_Game)
            {
                fuel -= decayRate * Time.deltaTime;
                decayRate += decayIncrease * Time.deltaTime;


                if (fuel <= 0f)
                {
                    fuel = 0f;
                    GameManager.inst.EndGame();
                }
            }
        }

        if (isLocked)
        {
            lockTimer -= Time.deltaTime;
            if(lockTimer <= 0f)
            {
                Unlock();
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

            //Remove any we've deleted.
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
                Lock(_trash.GetComponent<Trash>());
            }
        }

        if (_trash.tag == "Recycling")
        {
            if (isIncinerator)
            {
                Lock(_trash.GetComponent<Trash>());
            }
            else
            {
                GetFuel();
            }
        }

        tGod.activeTrash.Remove(_trash);
        Destroy(_trash);
    }

    void Lock(Trash _trash)
    {
        isLocked = true;
        lockTimer = lockDuration;
        GetComponent<Collider2D>().isTrigger = false;
        tGod.WrongTrash(_trash);
        //sRend.color = new Color(defColor.r, defColor.g, defColor.b, 0.5f);
    }

    void Unlock()
    {
        GetComponent<Collider2D>().isTrigger = true;
        //sRend.color = defColor;
        isLocked = false;
    }

    void GetFuel()
    {
        fuel += fuelPerTrash;
        if(fuel > maxFuel)
        {
            fuel = maxFuel;
        }

        ++GameManager.inst.itemsRecycled;
    }

    public void OnReset()
    {
        fuel = startingFuel;
        decayRate = initialDecay;
        Unlock();
    }
}
