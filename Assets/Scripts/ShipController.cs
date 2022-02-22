using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShipController : MonoBehaviour
{
    
    [SerializeField] float speed;
    [SerializeField] float multiplier;

    private void Start()
    {
        
        multiplier = Random.Range(0.25f, 0.3f);
    }

    private void Update()
    {
        RotateShip();
    }

    private void RotateShip()
    {
        this.transform.Rotate(new Vector3(this.transform.rotation.x, this.transform.rotation.y, (speed * Time.deltaTime) * multiplier));
    }
}
