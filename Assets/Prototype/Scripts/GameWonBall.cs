using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWonBall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerManager>().GameWon = true;
            Destroy(gameObject);
        }
    }
    
}
