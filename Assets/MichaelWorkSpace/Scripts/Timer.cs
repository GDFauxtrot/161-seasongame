using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float curTime;
    public TextMeshProUGUI timerUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        CheckEndGame();
    }

    void CheckEndGame()
    {
        if(curTime < 0)
        {
            Debug.Log("Game should end");
        }
    }

    void UpdateTime()
    {
        curTime -= Time.deltaTime;
        timerUI.text = curTime.ToString("F2");
    }
}
