using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnManager;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
       float randX = Random.Range(-9f, 9f);
       float randY = Random.Range(1f, 6f);

        transform.position = new Vector3(randX, randY, 0f);
    }

    void Update()
    {
        Rotate();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Explode();
        }
    }
    private void Rotate()
    {
        transform.Rotate(0, 0, (1 * _speed * Time.deltaTime));
    }

    private void Explode()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        _spawnManager.StartSpawning();
        GameObject newExplosion = Instantiate(_explosion, gameObject.transform);
        newExplosion.GetComponent<AudioSource>().Play();
        Destroy(gameObject, 3);
    }
}
