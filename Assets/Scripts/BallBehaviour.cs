using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform originalSpawn;
    [SerializeField] float slowdownSpeed;
    [SerializeField] float resetCooldown;
    [SerializeField] float scaleSpeed;
    bool isHit;
    public Ease animEase;
    private void Update()
    {
        SlowDownByTime();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rocket"))
            isHit = true;
    }

    private void SlowDownByTime()
    {
        if (!isHit) return;
        rb.velocity -= (rb.velocity * slowdownSpeed) * Time.deltaTime;
        rb.angularVelocity -= rb.angularVelocity * slowdownSpeed * Time.deltaTime;

        if (rb.velocity.magnitude <= 0)
        {
            isHit = false;
        }
    }


    public void ResetToSpawn()
    {
        isHit = false;
        rb.velocity *= 0;
        rb.angularVelocity *= 0;
        BallAnim();
        //StartCoroutine(ResetPositionAndTimeScale());
    }

    IEnumerator ResetPositionAndTimeScale()
    {
        yield return new WaitForSecondsRealtime(resetCooldown);
        this.transform.position = originalSpawn.position;
        Time.timeScale = 1f;
        this.transform.DOScale(Vector3.one, scaleSpeed).SetEase(animEase);
    }

    public void BallAnim()
    {
        this.transform.DOScale(Vector3.zero, scaleSpeed).SetEase(animEase);
        StartCoroutine(ResetPositionAndTimeScale());
    }
}
