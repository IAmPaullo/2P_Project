using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    Vector2 rocketDir;
    bool isSpawnable = true;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject rocket;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float cooldown;
    [SerializeField] Rigidbody2D ballRb;




    private void Start()
    {
        //rocketDir = transform.up;
        rb = rocket.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnRocket();

        }
    }

    public void SpawnRocket()
    {
        if (!isSpawnable) return;
        rocket.SetActive(true);
        rocket.transform.parent = null;
        rb.velocity = rocketDir * speed;
        isSpawnable = false;

        StartCoroutine(RocketCooldown());
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 _wallNormal = collision.contacts[0].normal;
            rocketDir = Vector2.Reflect(rb.velocity, _wallNormal).normalized;

            //speed *= 0.5f;

            rb.velocity = rocketDir * speed;

        }
    }

    private IEnumerator RocketCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        rocket.SetActive(false);
        rocket.transform.SetParent(spawnPoint);
        rocket.transform.position = spawnPoint.position;
        isSpawnable = true;
    }

}
