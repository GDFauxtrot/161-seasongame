using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    CanvasGroup pauseMenu;
    CanvasGroup controlMenu;
    bool pauseMenuIsActive = false;
    public TextMeshProUGUI scoreTextUI;
    private int score;

    void Start()
    {
        Time.timeScale = 1.0f;
        pauseMenu = GameObject.Find("PauseMenu").GetComponent<CanvasGroup>();
        controlMenu = GameObject.Find("ControlsMenu").GetComponent<CanvasGroup>();
        score = 0;
    }

    void Update()
    {
        PauseMenuToggle();
    }

    void PauseMenuToggle()
    {
        if (Input.GetKeyDown("escape") && !pauseMenuIsActive)
        {
            Time.timeScale = 0;
            ShowCanvas(pauseMenu);
            pauseMenuIsActive = true;
        }

        else if (Input.GetKeyDown("escape") && pauseMenuIsActive)
        {
            Time.timeScale = 1;
            HideCanvas(pauseMenu);
            HideCanvas(controlMenu);
            pauseMenuIsActive = false;
        }
    }

    public void ShowCanvas(CanvasGroup canvas)
    {
        canvas.alpha = 1f;
        canvas.blocksRaycasts = true;
    }

    public void HideCanvas(CanvasGroup canvas)
    {
        canvas.alpha = 0f;
        canvas.blocksRaycasts = false;
    }

    public void StartTime()
    {
        Time.timeScale = 1;
    }

    public void ChangePauseMenuIsActive()
    {
        pauseMenuIsActive = !pauseMenuIsActive;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        StartCoroutine(ScoreJuice());
        scoreTextUI.text = score.ToString();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ScoreJuice()
    {
        scoreTextUI.fontSize = 30;
        yield return new WaitForSeconds(.3f);
        scoreTextUI.fontSize = 20;
    }
    
}
