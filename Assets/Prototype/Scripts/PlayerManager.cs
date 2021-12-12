using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Constants

    private const int HitDamage = 8;
    private const int EnergyRestore = 5;
    private const int InitialEnergy = 30;

    #endregion
    
    #region Inspector

    [SerializeField] private  TextMeshProUGUI energyFrame;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWonUI;
    [SerializeField] float invulnerableLength;

    #endregion

    #region Fields

    [HideInInspector] public bool GameWon;
    private bool _canRoll;
    private int _playerEnergy = InitialEnergy;
    private float invulnerableCounter; 


    #endregion
    


    #region MonoBehaviour
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (invulnerableCounter > 0)
            {
                return;
            }
            enemyHit(other.gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            if (invulnerableCounter > 0)
                return;
            enemyHit(other.gameObject);
        }
    }

    private void Update()
    {
        if (invulnerableCounter > 0)
        {
            invulnerableCounter -= Time.deltaTime;
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        }
        if (invulnerableCounter <= 0)
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }

        if (GameWon)
        {
            GetComponent<PlayerMovement>().PlayerDead = true;
            gameWonUI.gameObject.SetActive(true);
        }
                
    }
    #endregion
    
    #region Methods

    private void enemyHit(GameObject other)
    {
        gameObject.GetComponent<Animator>().SetTrigger("PlayerHit");
        _playerEnergy -= HitDamage;
        if (_playerEnergy <= 0)
        {
            _playerEnergy = 0;
            gameObject.GetComponent<Animator>().SetTrigger("PlayerDead");
            GetComponent<PlayerMovement>().PlayerDead = true;
            gameOverUI.SetActive(true);
            
        }
        energyFrame.text = _playerEnergy.ToString();
        
        var player = GetComponent<PlayerMovement>();
        if (other.transform.position.x > transform.position.x)
            player.knockFromRight = true;
        else
            player.knockFromRight = false;
        player.knockBackCount = player.knockBackLength;
        
        invulnerableCounter = invulnerableLength;
    }

    public void addEnergy()
    {
        _playerEnergy += EnergyRestore;
        energyFrame.text = _playerEnergy.ToString();
    }
    
    #endregion
}
