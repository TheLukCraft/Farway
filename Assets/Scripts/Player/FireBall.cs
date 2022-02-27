using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject dameEffect;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Instantiate(dameEffect, transform.position, dameEffect.transform.rotation);
            Destroy(gameObject);
        }
    }
}
