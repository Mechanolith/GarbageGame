using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    e_MainMenu,
    e_Game,
    e_PostGame
}

public class GameManager : MonoBehaviour
{

    public static GameManager inst;
    //[HideInInspector]
    public GameState state = GameState.e_MainMenu;

    public GameObject playerPrefab;

    public GameObject menuUI;
    public GameObject gameUI;
    public GameObject postUI;
    public GameObject creditsUI;
    public GameObject menuButtons;

    public Generator recycler;
    public Generator incinerator;
    public TrashGod tGod;
    public AudioGod aGod;
    public EnergyBar eBar;
    public TextFade sortText;
    public TextFade recText;
    public TextFade trashText;

    public EndGameUI endUI;

    public GameObject trashLandParticle;

    [HideInInspector]
    public float totalTime;
    [HideInInspector]
    public float itemsRecycled;

    [Header("Screen Flash")]
    public SpriteRenderer screenFlash;
    public float flashDuration;
    public AnimationCurve flashCurve;
    [HideInInspector]
    public float flashTimer;
    [HideInInspector]
    public bool flashing = false;

    void Awake()
    {
        inst = this;
    }

    // Use this for initialization
    void Start()
    {
        ToMenu();
        HideCredits();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == GameState.e_Game)
        {
            totalTime += Time.deltaTime;
        }

        if (flashing)
        {
            ScreenFlash();
        }
    }

    public void StartGame()
    {
        aGod.PlaySFX(SFXType.Button);

        menuUI.SetActive(false);
        postUI.SetActive(false);
        gameUI.SetActive(true);

        ResetPlayer();
        recycler.OnReset();
        incinerator.OnReset();
        tGod.OnReset();
        eBar.OnReset();
        sortText.OnReset();
        recText.OnReset();
        trashText.OnReset();
        itemsRecycled = 0;
        totalTime = 0f;

        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        state = GameState.e_Game;
    }

    public void EndGame()
    {
        aGod.PlaySFX(SFXType.GameOver);
        gameUI.SetActive(false);
        menuUI.SetActive(false);
        postUI.SetActive(true);
        endUI.ShowResults(totalTime, itemsRecycled, tGod.wrongList);
        state = GameState.e_PostGame;
    }

    public void ToMenu()
    {
        aGod.PlaySFX(SFXType.Button);

        postUI.SetActive(false);
        gameUI.SetActive(false);
        menuUI.SetActive(true);

        ResetPlayer();
        tGod.OnReset();
        itemsRecycled = 0;
        totalTime = 0f;

        state = GameState.e_MainMenu;
    }

    public void ShowCredits()
    {
        aGod.PlaySFX(SFXType.Button);
        menuButtons.SetActive(false);
        creditsUI.SetActive(true);
    }

    public void HideCredits()
    {
        aGod.PlaySFX(SFXType.Button);
        menuButtons.SetActive(true);
        creditsUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void ResetPlayer()
    {
        GameObject player = GameObject.Find("Player(Clone)");
        if (player != null)
        {
            Destroy(player);
        }
    }

    public void ScreenFlash()
    {
        flashTimer += Time.deltaTime;

        float flashProg = flashTimer / flashDuration;
        float alphaVal = flashCurve.Evaluate(flashProg);

        Color curCol = screenFlash.color;
        screenFlash.color = new Color(curCol.r, curCol.g, curCol.b, alphaVal);

        if (flashTimer > flashDuration)
        {
            flashTimer = 0f;
            flashing = false;

            screenFlash.color = new Color(curCol.r, curCol.g, curCol.b, 0f);
        }
    }
}