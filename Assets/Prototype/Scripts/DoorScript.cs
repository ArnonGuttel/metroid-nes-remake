using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorScript : MonoBehaviour
{
    [SerializeField] private float animationLength;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            GetComponent<AudioSource>().Play(0);
            GetComponent<Animator>().SetTrigger("OpenDoor");
        }
        
    }


}
