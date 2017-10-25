using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour {

    public float fadeRate;
    public float fadeDelay;
    float fadeTimer;
    public Text fadeText;
    Color curColor;
    public Color startColor;
	
	void Start () {
        fadeText = GetComponent<Text>();
        startColor = fadeText.color;
        curColor = startColor;
        OnReset();
	}
	
	void Update () {
        if (GameManager.inst.state == GameState.e_Game)
        {
            if (fadeTimer > 0)
            {
                fadeTimer -= Time.deltaTime;
            }
            else
            {
                if (curColor.a > 0f)
                {
                    curColor.a -= fadeRate * Time.deltaTime;
                    fadeText.color = curColor;
                }
            }
        }
	}

    public void OnReset()
    {
        fadeTimer = fadeDelay;
        curColor = startColor;
        fadeText.color = startColor;
    }
}
