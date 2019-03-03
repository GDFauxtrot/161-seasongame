using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    CanvasGroup pauseMenu;
    CanvasGroup controlMenu;
    bool pauseMenuIsActive = false;

    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu").GetComponent<CanvasGroup>();
        controlMenu = GameObject.Find("ControlsMenu").GetComponent<CanvasGroup>();
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

}
