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

    [SerializeField]
    enum EnemyStanceEnum { Idle, Reset, ShootWall, ShootStraight };

    [SerializeField]
    EnemyStanceEnum stance;


    private void Awake()
    {
        originalTransform = this.gameObject.transform;
    }

    private void Update()
    {
        Signal();

    }


    void Signal()
    {
        maxBounce = Mathf.Clamp(maxBounce, 1, maxBounce);
        ray = new Ray2D(this.transform.position, transform.up);
        hit = Physics2D.Raycast(ray.origin, ray.direction);
        Debug.DrawRay(transform.position, this.gameObject.transform.up, Color.red);

        if (canRoll)
        {
            int roll = RollForChance();
            EnemyState(roll);

            StartCoroutine(ChanceCountDown());
            canRoll = false;
        }

        if (randValue != 0)
        {


            if (hit && hit.collider.CompareTag("Wall"))
            {
                refDir = Vector2.Reflect(ray.direction, hit.normal);
                RaycastHit2D reflectedHit = Physics2D.Raycast(hit.point, refDir);


                Debug.Log("parede");

                Debug.DrawRay(hit.point, refDir * 100, Color.green);

                if (reflectedHit)
                {
                    if (reflectedHit.collider.CompareTag("Ball"))
                    {
                        Debug.Log("Collider verde na bola");
                    }
                    else
                    {

                        Debug.Log(hit.transform.name);
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
    }

    private int RollForChance()
    {
        if (canRoll)
        {
            randValue = Random.Range(0, states);
            Debug.Log(randValue);
        }

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
    }

    private void EnemyState(int rolledState)
    {


        switch (rolledState)
        {
            case 0:
                //Idle
                stance = EnemyStanceEnum.Idle;
                ray = new Ray2D(this.transform.position, transform.up);
                hit = Physics2D.Raycast(ray.origin, ray.direction);
                break;


            case 1:
                stance = EnemyStanceEnum.Reset;
                ResetTransform();

                break;

               

            case 2:
                stance = EnemyStanceEnum.ShootStraight;

                break;


            case 3:
                stance = EnemyStanceEnum.ShootWall;

                break;



        }



    }



}
