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

    public TextMeshProUGUI energyFrame;
    
    #endregion

    #region Fields
    
    private bool _canRoll;
    private int _playerEnergy = InitialEnergy;
    [SerializeField] float invulnerableLength;
    private float invulnerableCounter; 


    #endregion
    
    #region Events
    
    public static event Action playerDead;

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
            GetComponent<SpriteRenderer>().color = Color.red;
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
                
    }
    #endregion
    
    #region Methods

    private void enemyHit(GameObject other)
    {
        _playerEnergy -= HitDamage;
        if (_playerEnergy <= 0)
        {
            _playerEnergy = 0;
            playerDead?.Invoke();
        }
        energyFrame.text = _playerEnergy.ToString();
        
        var player = GetComponent<PlayerMovement>();
        if (other.transform.position.x > transform.position.x)
            player.knockFromRight = true;
        else
            player.knockFromRight = false;
        player.knockBackCount = player.knockBackLength;
        
        invulnerableCounter = invulnerableLength;
        GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public void addEnergy()
    {
        _playerEnergy += EnergyRestore;
        energyFrame.text = _playerEnergy.ToString();
    }
    
    #endregion
}
