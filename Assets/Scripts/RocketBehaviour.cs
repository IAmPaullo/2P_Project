using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    Vector2 rocketDir;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    Transform spawnPoint;
    Vector2 lateSpeed;

    private void OnEnable()
    {
        spawnPoint = transform.parent;
        rocketDir = transform.up;
        transform.parent = null;
        rb.velocity = rocketDir * speed;
    }

    private void Update()
    {
        lateSpeed = rb.velocity;
        transform.up = lateSpeed.normalized;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log(lateSpeed.normalized);
            Vector2 _wallNormal = collision.contacts[0].normal;
            rocketDir = Vector2.Reflect(lateSpeed, _wallNormal).normalized;
            rb.velocity = rocketDir * speed;

        }
        else if (collision.gameObject.CompareTag("Ball"))
        {
            this.gameObject.SetActive(false);
        }
    }




}
