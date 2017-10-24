using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFXType
{
    Button,
    Drop,
    GameOver,
    Generate,
    Incinerate,
    Pickup,
    Spawn,
    StepL, 
    StepR,
    Throw,
    Unlock,
    Warning,
    Wrong
}

[System.Serializable]
public class SFX
{
    public SFXType type;
    public AudioClip sound;
}

public class AudioGod : MonoBehaviour {

    public List<SFX> sounds = new List<SFX>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySFX(SFXType _type)
    {
        for(int index = 0; index < sounds.Count; ++index)
        {
            if(sounds[index].type == _type)
            {
                AudioSource.PlayClipAtPoint(sounds[index].sound, Camera.main.transform.position);
                break;
            }
        }
    }
}
