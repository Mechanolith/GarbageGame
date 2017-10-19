using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGod : MonoBehaviour {

    public List<GameObject> trashList = new List<GameObject>();
    public List<GameObject> recycleList = new List<GameObject>();

    public float recycleChance;

    public float spawnDelay;
    float spawnTimer;
    public Vector3 spawnPoint;

    public List<GameObject> activeTrash = new List<GameObject>();

	void Start () {
        spawnTimer = spawnDelay;
	}

	void Update () {
        if (GameManager.inst.state == GameState.e_Game)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                SpawnTrash();
                spawnTimer = spawnDelay;
            }
        }
	}

    void SpawnTrash()
    {
        float type = Random.Range(0f, 100f);
        if (type < recycleChance)
        {
            MakeTrash(recycleList);
        }
        else
        {
            MakeTrash(trashList);
        }
    }

    void MakeTrash(List<GameObject> _trashList)
    {
        int rand = Random.Range(0, _trashList.Count);

        spawnPoint = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);

        GameObject trash = Instantiate(_trashList[rand], spawnPoint, Quaternion.identity) as GameObject;

        activeTrash.Add(trash);
    }

    public void OnReset()
    {
        for(int index = 0; index < activeTrash.Count; ++index)
        {
            Destroy(activeTrash[index]);
        }

        activeTrash.Clear();
    }
}
