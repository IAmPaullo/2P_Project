using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalBehaviour : MonoBehaviour
{

    public bool hasScoredPoint = false;
    public UnityEvent goalEvent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Time.timeScale = .2f;
            hasScoredPoint = true;
            goalEvent.Invoke();
            ResetGoal();
        }
    }

    public void ResetGoal()
    {
        hasScoredPoint = false;

    }
}
