using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
            GameReset();
    }

    void GameReset()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
