using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGod : MonoBehaviour {

    public List<GameObject> trashList = new List<GameObject>();

    public float spawnDelay;
    float spawnTimer;
    public Vector3 spawnPoint;

	void Start () {
        spawnTimer = spawnDelay;
	}

	void Update () {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnTrash();
            spawnTimer = spawnDelay;
        }
	}

    void SpawnTrash()
    {
        int rand = Random.Range(0, trashList.Count);

        Instantiate(trashList[rand], spawnPoint, Quaternion.identity);

        spawnPoint = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);
    }
}
