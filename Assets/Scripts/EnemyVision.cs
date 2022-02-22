using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] int maxBounce = 1;
    [SerializeField] int states = 3;
    [SerializeField] float cooldown;
    [SerializeField] GameObject rocketPrefab;
    Transform originalTransform;
    Ray2D ray;
    RaycastHit2D hit;
    Vector2 refDir;



    int randValue;
    bool canRoll = true;
    bool isSpawnable = true;
    bool canShoot = true;
    enum EnemyStanceEnum { Idle, Reset, ShootWall, ShootStraight };

    [SerializeField]
    EnemyStanceEnum stance;


    private void Awake()
    {
        originalTransform = this.gameObject.transform;
    }

    private void Update()
    {
        //Signal();
        if (canRoll)
        {
            int roll = RollForChance();
            StartCoroutine(ChanceCountDown());
            canRoll = false;
        }

        EnemyState();

        Debug.DrawRay(transform.position, this.gameObject.transform.up * 100, Color.red);

    }


    void Signal()
    {
        maxBounce = Mathf.Clamp(maxBounce, 1, maxBounce);
        ray = new Ray2D(this.transform.position, transform.up);
        hit = Physics2D.Raycast(ray.origin, ray.direction);
        Debug.DrawRay(transform.position, this.gameObject.transform.up, Color.blue);





        if (randValue == 4)
        {


            if (hit && hit.collider.CompareTag("Wall"))
            {
                refDir = Vector2.Reflect(ray.direction, hit.normal);
                RaycastHit2D reflectedHit = Physics2D.Raycast(hit.point, refDir);


                Debug.DrawRay(hit.point, refDir * 100, Color.green);

                if (reflectedHit)
                {
                    if (reflectedHit.collider.CompareTag("Ball"))
                    {
                        Debug.Log("Collider verde na bola");
                    }
                    else
                    {

                        //Debug.Log(hit.transform.name);
                    }

                }
            }

            else
            {
                if (hit)
                {
                    refDir = Vector2.Reflect(refDir, hit.normal);
                    ray = new Ray2D(hit.point, refDir);
                    Debug.DrawRay(hit.point, refDir * 100, Color.yellow);
                }
            }
        }

    }


    private IEnumerator ChanceCountDown()
    {
        yield return new WaitForSeconds(cooldown);
        canRoll = true;
        canShoot = true;
    }

    private int RollForChance()
    {
        if (canRoll)
            randValue = Random.Range(1, states);

        return randValue;
    }


    private void SpawnMissile()
    {

        rocketPrefab.SetActive(true);
        canRoll = false;
        StartCoroutine(ChanceCountDown());
    }

    private void ResetTransform()
    {
        transform.position = originalTransform.position;
        transform.rotation = originalTransform.rotation;
        ray = new Ray2D(this.transform.position, transform.up);
        hit = Physics2D.Raycast(ray.origin, ray.direction);

        Debug.Log("resetei");
    }


    private void ShootWall()
    {

    }

    private void ShootStraight()
    {
        ray = new Ray2D(this.transform.position, transform.up);
        hit = Physics2D.Raycast(ray.origin, ray.direction);
        Debug.DrawRay(transform.position, this.gameObject.transform.up * 100, Color.blue);

        if (hit && canShoot)
        {
            if (hit.collider.CompareTag("Ball"))
            {
                Debug.LogWarning("Atirou na bola");
                canShoot = false;
                StartCoroutine(ChanceCountDown());
            }

        }
    }

    private void EnemyState()
    {
        int rolledState = RollForChance();

        switch (rolledState)
        {
            case 0:
                stance = EnemyStanceEnum.Reset;
                ResetTransform();

                break;


            case 1:
                //Idle
                stance = EnemyStanceEnum.Idle;
                ray = new Ray2D(this.transform.position, transform.up);
                hit = Physics2D.Raycast(ray.origin, ray.direction);
                break;





            case 2:
                stance = EnemyStanceEnum.ShootStraight;

                ShootStraight();
                cooldown = 500;


                break;


            case 3:
                stance = EnemyStanceEnum.ShootWall;
                ShootWall();
                break;



        }



    }



}
