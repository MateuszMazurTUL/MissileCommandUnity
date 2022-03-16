using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject pointsObject;
    [SerializeField] GameObject gameStateObject;
    [SerializeField] GameObject UI_bg;

    // Start is called before the first frame update
    void Start()
    {
        //initial UI components
        Canvas.SetActive(true);
        if (!pointsObject) pointsObject = Canvas.transform.Find("Points").gameObject;
        if(!gameStateObject) gameStateObject = Canvas.transform.Find("Status").gameObject;
        if(!gameStateObject) UI_bg = Canvas.transform.Find("UI_bg").gameObject;
    }

    //update points in UI
    internal void UpdatePoints(int points)
    {
        //manipulate string to make const lenght of 7-digit string
        string tmp = "Score: 0000000";
        int lenP = points.ToString().Length;
        int lenS = tmp.Length;
        string newText = tmp.Substring(0, lenS - lenP) + points.ToString();

        this.pointsObject.GetComponent<Text>().text = newText;
    }

    //update UI, set string and alpha background
    internal void UI_Pause()
    {
        this.gameStateObject.SetActive(true);
        this.UI_bg.SetActive(true);
        this.gameStateObject.GetComponent<Text>().text = "Pause";
    }

    //update UI, remove string and alpha background
    internal void UI_Play()
    {
        this.gameStateObject.SetActive(false);
        this.UI_bg.SetActive(false);
    }

    //update UI, like pause, other string
    internal void UI_Gameover()
    {
        this.UI_Pause();
        this.gameStateObject.GetComponent<Text>().text = "Game Over\n[Press ESC to PLAY]";
    }

    //update UI, like pause, other string
    internal void UI_PreGame()
    {
        this.UI_Pause();
        this.gameStateObject.GetComponent<Text>().text = "[Press ESC to PLAY]";
    }


}
