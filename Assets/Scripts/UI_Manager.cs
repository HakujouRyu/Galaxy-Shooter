using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _GameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Sprite[] _lifesprites;
    [SerializeField]
    private Image _livesImg;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("GameManager is null!");
        }
        _scoreText.text = "Score: " + 0;
        _GameOverText.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }

    public void DisplayScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _lifesprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }
    void GameOverSequence()
    {
        _gameManager.GameOver();
        _restartText.gameObject.SetActive(true);
        StartCoroutine(DisplayGameover());
    }

    IEnumerator DisplayGameover()
    {
        
        while (true)
        {
            _GameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _GameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.3f);

        }

    }
    
}
