using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour {

    public Generator gen;
    float fullSize;

    public float flashTime;
    public float flashLimit;
    public int cyclesPerSound = 3;
    int curCycles = 0;
    float counter;
    bool isRed;
    Color startCol;
    public Color flashCol;
    Image bar;

	// Use this for initialization
	void Start () {
        fullSize = transform.localScale.y;
        counter = 0f;
        bar = GetComponent<Image>();
        startCol = bar.color;
    }
	
	// Update is called once per frame
	void Update () {
        float normalisedSize = (gen.fuel / gen.maxFuel) * fullSize;
        if(normalisedSize < 0f)
        {
            normalisedSize = 0f;
        }
        transform.localScale = new Vector3(transform.localScale.x, normalisedSize, transform.localScale.z);

        if (gen.fuel < flashLimit * (gen.maxFuel * (1f/100f)) && GameManager.inst.state == GameState.e_Game)   //If under X% max fuel.
        {
            counter -= Time.deltaTime;
            if (counter <= 0)
            {
                if (isRed)
                {
                    bar.color = startCol;
                    isRed = false;
                }
                else
                {
                    bar.color = flashCol;
                    isRed = true;

                    if (curCycles == 0)
                    {
                        GameManager.inst.aGod.PlaySFX(SFXType.Warning);
                    }

                    ++curCycles;
                    curCycles %= cyclesPerSound;
                }

                counter = flashTime;
            }
        }
        else if (counter != flashTime)
        {
            OnReset();
        }
	}

    public void OnReset()
    {
        counter = flashTime;
        bar.color = startCol;
        isRed = false;
    }
}
