using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour {

    public float energyPerItem = 5f;
    public Text resultText;
    public Text flawlessText;
    public Text wrongTitle;
    public Text wrongType;
    public Text wrongText;
    public Image wrongImage;

    public Button nextButton;
    public Button prevButton;

    public Sprite transparentImage;

    int currentWrong = 0;
    List<TrashInfo> wrongList;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void ShowResults(float _time, float _items, List<TrashInfo> _wrongList)
    {
        wrongList = _wrongList;

        resultText.text = "You kept the generator running for " + Mathf.Round(_time) + " seconds, and made " + (_items * energyPerItem) + " kilowatts of power!";

        if (wrongList.Count > 0)
        {
            flawlessText.text = "";
            DisplayWrong(0);
        }
        else
        {
            wrongTitle.text = "Flawless!";
            wrongType.text = "";
            wrongText.text = "";
            wrongImage.sprite = transparentImage;

            string tempText = "You didn't sort anything wrong! <br> <br> Great Recycling!";
            tempText = tempText.Replace("<br>", "\n");
            flawlessText.text = tempText;
        }

        if(wrongList.Count < 1)
        {
            nextButton.gameObject.SetActive(false);
            prevButton.gameObject.SetActive(false);
        }
        else
        {
            nextButton.gameObject.SetActive(true);
            prevButton.gameObject.SetActive(true);
        }
    }

    public void NextWrong()
    {
        if(currentWrong < wrongList.Count - 1)
        {
            ++currentWrong;
        }
        else
        {
            currentWrong = 0;
        }

        DisplayWrong(currentWrong);
    }

    public void PrevWrong()
    {
        if (currentWrong == 0)
        {
            currentWrong = wrongList.Count - 1;
        }
        else
        {
            --currentWrong;
        }

        DisplayWrong(currentWrong);
    }

    void DisplayWrong(int ID)
    {
        wrongTitle.text = wrongList[ID].name;
        wrongType.text = wrongList[ID].type;
        wrongText.text = wrongList[ID].info;
        wrongImage.sprite = wrongList[ID].image;
    }
}
