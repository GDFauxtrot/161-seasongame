using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float curTime;
    private float originalTime;
    public TextMeshProUGUI timerUI;
    public GameObject gameOverWindow;
    public TMP_ColorGradient mid;
    public TMP_ColorGradient low;

    // Start is called before the first frame update
    void Start()
    {
        originalTime = curTime;
        gameOverWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        //CheckEndGame();
    }

    /*
    void CheckEndGame()
    {
        if(curTime < 0)
        {
            Debug.Log("Game should end");
        }
    }*/

    void UpdateTime()
    {
        curTime -= Time.deltaTime;
        timerUI.text = curTime.ToString("F2");

        if (curTime / originalTime < .5f && curTime / originalTime >= .25f)
        {
            timerUI.color = Color.yellow;
            timerUI.colorGradientPreset = mid;
        }

        else if(curTime / originalTime < .25f)
        {
            timerUI.color = Color.red;
            timerUI.colorGradientPreset = low;
        }

        if(curTime < 0 && !gameOverWindow.activeSelf)
        {
            Time.timeScale = 0;
            gameOverWindow.SetActive(true);
        }
    }
}
