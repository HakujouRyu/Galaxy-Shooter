using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            StartCoroutine(ReloadScene());
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
    IEnumerator ReloadScene()
    {
        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(1); //BaseScene

        while (!sceneLoading.isDone)
        {
            yield return null;
        }
    }
}
