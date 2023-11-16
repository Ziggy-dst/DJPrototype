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
    }

    private void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            FindObjectOfType<RespawnPlayer>().RespawnReset();
        }
    }
}
