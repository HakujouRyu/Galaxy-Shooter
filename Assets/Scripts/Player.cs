using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.05f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 4;
    private SpawnManager _spawnManager;
    private UI_Manager _UI_Manager;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private bool _tripleShotActive = false;
    [SerializeField]
    private float _tripleShotDuration = 5;
    [SerializeField]
    private float _speedUpDuration = 5;
    [SerializeField]
    private bool _speedUpActive;
    [SerializeField]
    private int _speedBoost = 2;
    [SerializeField]
    private bool _shieldActive = false;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private int _score = 0;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _UI_Manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        if (_UI_Manager == null)
        {
            Debug.LogError("UI_Manager is null");
        }
        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is null");
        }
    }

    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_speedUpActive)
        {
            transform.Translate(direction * _speed * _speedBoost * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed  * Time.deltaTime);
        }


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
        else if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_tripleShotActive == true)
        {
            Vector3 laserSpawnPosition = new Vector3(transform.position.x - 0.25f, transform.position.y + 0.5f, 0);
            Instantiate(_tripleShot, laserSpawnPosition, Quaternion.identity);
        }
        else
        {
            Vector3 laserSpawnPosition = new Vector3(transform.position.x, transform.position.y + 1.0f, 0);
            Instantiate(_laserPrefab, laserSpawnPosition, Quaternion.identity);
        }
    }

    public void Damage()
    {
        if (_shieldActive)
        {
            _shieldActive = false;
            _shield.SetActive(false);
            return;
        }

        else
        {
            _lives--;
            _UI_Manager.UpdateLives(_lives);
        }
        

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void ActivatePowerup(int powerupID)
    {
        switch (powerupID)
        {
            case 0:
                _tripleShotActive = true;
                break;
            case 1:
                _speedUpActive = true;
                break;
            case 2:
                _shieldActive = true;
                _shield.SetActive(true);
                break;
        }
        StartCoroutine(PowerDown(powerupID));

    }

    private IEnumerator PowerDown(int powerupID)
    {
        switch (powerupID)
        {
            case 0:
                while (_tripleShotActive)
                {
                    yield return new WaitForSeconds(_tripleShotDuration);

                    _tripleShotActive = false;
                }
                break;
            case 1:
                while (_speedUpActive)
                {
                    yield return new WaitForSeconds(_speedUpDuration);

                    _speedUpActive = false;
                }
                break;
            case 2: //shields have no cooldown
                break;
        }
        
    }

    public void AddToScore(int points)
    {
        _score += points;
        _UI_Manager.DisplayScore(_score);

    }


}

