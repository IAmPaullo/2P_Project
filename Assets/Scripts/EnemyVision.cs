using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    
    [SerializeField] int states = 3;
    [SerializeField] float cooldown;
    [SerializeField] GameObject rocketPrefab;
    [SerializeField] Transform spawnPoint;
    Transform originalTransform;
    Ray2D ray;
    RaycastHit2D hit;
    Vector2 refDir;



    int randValue;
    bool canRoll = true;
    bool canShoot = true;
    enum EnemyStanceEnum { Idle, Reset, ShootWall, ShootStraight };

    [SerializeField]
#pragma warning disable IDE0052 // Remove unread private members
    EnemyStanceEnum stance;
#pragma warning restore IDE0052 // Remove unread private members




    private void Awake()
    {
        originalTransform = this.gameObject.transform;
    }

    private void Update()
    {

        RollUpdate();

        EnemyState();

        Debug.DrawRay(transform.position, this.gameObject.transform.up * 100, Color.red);

    }


    private void RollUpdate()
    {
        if (canRoll)
        {
            int roll = RollForChance();
            //StartCoroutine(ChanceCountDown());
            canRoll = false;
            //StartCoroutine(ChanceCountDown());
            Debug.LogWarning("Rolando");
        }
    }
    private int RollForChance()
    {
        if (canRoll)
            randValue = Random.Range(1, states);

        return randValue;
    }

    


    private IEnumerator ChanceCountDown()
    {
        yield return new WaitForSeconds(cooldown + 0.3f);
        canRoll = true;
        if (canShoot == false)
            RocketReset();


    }


    private void SpawnMissile()
    {
        canRoll = false;
        if (canShoot == false) return;
        rocketPrefab.SetActive(true);
        canShoot = false;
        StartCoroutine(ChanceCountDown());
    }

    public void RocketReset()
    {
        rocketPrefab.transform.SetParent(spawnPoint);
        rocketPrefab.transform.position = spawnPoint.position;
        rocketPrefab.transform.rotation = spawnPoint.rotation;
        rocketPrefab.SetActive(false);
        canShoot = true;
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
        ray = new Ray2D(this.transform.position, transform.up);
        hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit && hit.collider.CompareTag("Wall"))
        {
            refDir = Vector2.Reflect(ray.direction, hit.normal);
            RaycastHit2D reflectedHit = Physics2D.Raycast(hit.point, refDir);
            Debug.DrawRay(hit.point, refDir * 100, Color.green);

            if (reflectedHit)
            {
                if (reflectedHit.collider.CompareTag("Ball"))
                {
                    Debug.LogWarning("Collider verde na bola");
                    SpawnMissile();
                }
            }
        }

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
                SpawnMissile();
            }

        }

    }

    private void RandomShot()
    {
        ray = new Ray2D(this.transform.position, transform.up);
        hit = Physics2D.Raycast(ray.origin, ray.direction);
        Debug.DrawRay(transform.position, this.gameObject.transform.up * 100, Color.blue);

        if (hit && canShoot)
        {
            if (this.transform.rotation.z > -35 && this.transform.rotation.z < 40)
                SpawnMissile();
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
                //ray = new Ray2D(this.transform.position, transform.up);
                //hit = Physics2D.Raycast(ray.origin, ray.direction);
                //StartCoroutine(ChanceCountDown());
                RandomShot();

                break;


            case 2:
                stance = EnemyStanceEnum.ShootStraight;
                ShootStraight();
                break;


            case 3:
                stance = EnemyStanceEnum.ShootWall;
                ShootWall();
                break;

        }



    }



}
