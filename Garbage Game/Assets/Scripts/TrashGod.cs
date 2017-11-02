using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrashInfo
{
    public TrashType ID;
    public GameObject trashObject;
    public string name;
    public string type;
    public string info;
    public Sprite image;
}

public class TrashGod : MonoBehaviour {

    public List<TrashInfo> trashList = new List<TrashInfo>();
    public List<TrashInfo> recycleList = new List<TrashInfo>();

    [HideInInspector]
    public List<TrashInfo> fullList = new List<TrashInfo>();
    [HideInInspector]
    public List<TrashInfo> wrongList = new List<TrashInfo>();

    public float recycleChance;

    public float spawnDelay;
    float spawnTimer;
    public Vector3 spawnPoint;

    public List<GameObject> activeTrash = new List<GameObject>();

    public GameObject spawnParticle;

	void Start () {
        spawnTimer = spawnDelay;
        fullList.AddRange(trashList);
        fullList.AddRange(recycleList);
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

        GameManager.inst.aGod.PlaySFX(SFXType.Spawn);
    }

    void MakeTrash(List<TrashInfo> _trashList)
    {
        int rand = Random.Range(0, _trashList.Count);

        spawnPoint = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);

        GameObject trash = Instantiate(_trashList[rand].trashObject, spawnPoint, Quaternion.identity) as GameObject;
        activeTrash.Add(trash);

        Instantiate(spawnParticle, spawnPoint, spawnParticle.transform.rotation);
    }

    public void OnReset()
    {
        for(int index = 0; index < activeTrash.Count; ++index)
        {
            Destroy(activeTrash[index]);
        }

        activeTrash.Clear();
        wrongList.Clear();

        spawnTimer = spawnDelay;
    }

    public void WrongTrash(Trash _trash)
    {
        for(int index = 0; index < fullList.Count; ++index)
        {
            if (fullList[index].ID == _trash.trashType)
            {
                if (!wrongList.Contains(fullList[index]))
                {
                    wrongList.Add(fullList[index]);
                }

                break;
            }
        }
    }
}
