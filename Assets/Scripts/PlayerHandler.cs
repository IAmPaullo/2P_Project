using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    [SerializeField]GameObject rocketPrefab;
    private bool isSpawnable = true;
    [SerializeField]private float cooldown;
    [SerializeField] Transform spawnPoint;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnMissile();
        }
    }

    private void SpawnMissile()
    {
        if (isSpawnable == false) return;
        rocketPrefab.SetActive(true);
        isSpawnable = false;
        StartCoroutine(RocketCooldown());
    }

    private IEnumerator RocketCooldown()
    {
        yield return new WaitForSeconds(cooldown);

        StartCooldown();
    }

    public void StartCooldown()
    {
        rocketPrefab.transform.SetParent(spawnPoint);
        rocketPrefab.transform.position = spawnPoint.position;
        rocketPrefab.transform.rotation = spawnPoint.rotation;
        rocketPrefab.SetActive(false);
        isSpawnable = true;
    }


}
