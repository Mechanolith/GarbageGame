using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autokill : MonoBehaviour {

    public float timeToKill = 2f;

	void Update () {
        timeToKill -= Time.deltaTime;

        if(timeToKill <= 0f)
        {
            Destroy(gameObject);
        }
	}
}
