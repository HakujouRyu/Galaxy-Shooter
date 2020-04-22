using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    [SerializeField]
    private int _powerupID;

    void Start()
    {
        transform.position = new Vector3(Random.Range(-9.7f, 9.7f), 9);
    }

    void Update()
    {
        CalculateMovement();
    }
    void CalculateMovement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -4.5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.ActivatePowerup(_powerupID);

            }

            Destroy(gameObject);
        }
    }
}
