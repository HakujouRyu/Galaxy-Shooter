using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player _player;
    [SerializeField]
    private int _pointValue;
    private Animator _animator;


    void Start()
    {
        GoToTop();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = gameObject.GetComponent<Animator>();

        if (_player == null)
        {
            Debug.LogError("Enemy::Player is null!?");
        }
        if (_animator == null)
        {
            Debug.LogError("Enemy::Animator is null!");
        }
        
    }

  
    void Update()
    {
        Movement();

        if (transform.position.y <= -6)
        {
            GoToTop();
        }
    
        
    }

    void GoToTop()
    {
        transform.position = new Vector3(UnityEngine.Random.Range(-9.7f, 9.7f), 9, 0);
    }

    void Movement()
    {
        /*int score = _player.GetScore();
        int scoreMultiplyer = (score / 100);
        transform.Translate(Vector3.down * (_speed + scoreMultiplyer)  * Time.deltaTime);*/
        
        // ^ Logic to speed enemies up every 100 points. ^ //

        transform.Translate(Vector3.down * (_speed)  * Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player =  other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            DeathSequence();

        }
        else if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            _player.AddToScore(_pointValue);
            DeathSequence();
        }

    }
    private void DeathSequence()
    {
        _animator.SetTrigger("OnEnemyDeath");
        gameObject.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        _speed = 1.5f;

        Destroy(gameObject, 2.4f);
    }
    public void SpeedUp()
    {
        _speed++;
    }
}
