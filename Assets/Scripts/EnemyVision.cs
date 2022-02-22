using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{

    Ray2D ray;
    RaycastHit2D hit;

    Vector2 refDir;
    LineRenderer lineRenderer;
    public int maxBounce = 2;

    private void Awake()
    {
        //lineRenderer = GetComponent<LineRenderer>();
        
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

        

        for (int i = 0; i < maxBounce; i++)
        {
            if (i == 0)
            {
                if (hit)
                {
                    refDir = Vector2.Reflect(ray.direction, hit.normal);
                    //ray = new Ray2D(hit.point, refDir);

                    RaycastHit2D reflectedHit = Physics2D.Raycast(hit.point, refDir);

                    
                    
                    
                    Debug.DrawRay(hit.point, refDir * 100, Color.green);
                    if (reflectedHit)
                    {
                        if (reflectedHit.collider.tag == "Ball")
                        {
                            //Debug.Log("bola");
                        }

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

                    

                    //Debug.Log(hit.transform.name);

                }
            }
        }
    }
}
