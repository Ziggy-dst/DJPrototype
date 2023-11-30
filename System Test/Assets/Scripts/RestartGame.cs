using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Restart();
        SelectLevel();
        PauseGame();
    }

    private void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            FindObjectOfType<RespawnPlayer>().RespawnReset();
        }
    }

    private void SelectLevel()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(0);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(1);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(2);
        }
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!GameManager.Instance.Paused)
            {
                GameManager.Instance.Pause();
            }
            else
            {
                GameManager.Instance.UnPause();
            }
        }
        
    }
}
