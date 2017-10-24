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

    public Generator recycler;
    public Generator incinerator;
    public TrashGod tGod;
    public AudioGod aGod;
    public EnergyBar eBar;

    public EndGameUI endUI;

    [HideInInspector]
    public float totalTime;
    [HideInInspector]
    public float itemsRecycled;

    void Awake()
    {
        inst = this;
    }

    // Use this for initialization
    void Start()
    {
        ToMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == GameState.e_Game)
        {
            totalTime += Time.deltaTime;
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
}