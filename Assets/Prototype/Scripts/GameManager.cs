using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager _shared;
    private bool _resetFlag;
    [SerializeField] private  TextMeshProUGUI TimerFrame;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWonUI;

    public List<GameObject> DeadEnemies;
    [SerializeField] private float Timer;
    private bool _startTimer;

    public static event Action OpenHiddenDoor;
    public static event Action PlayerDead;
    public static event Action GameWon;

    private void Start()
    {
        _shared = this;
    }

    public static void addToDeadEnemies(GameObject enemy)
    {
        _shared.DeadEnemies.Add(enemy);
    }

    public static void resetEnemies()
    {
        if (_shared.DeadEnemies.Count == 0 || _shared._resetFlag == false)
            return;
        foreach (var enemy in _shared.DeadEnemies.ToList())
        {
            enemy.gameObject.SetActive(true);
            _shared.DeadEnemies.Remove(enemy);
        }

        _shared._resetFlag = false;
    }

    public static void boundrayChange()
    {
        _shared._resetFlag = true;
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
    
    public void playAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Metroid Prototype");
    }

    public static void KeyTaken()
    {
        OpenHiddenDoor?.Invoke();
    }

    public static void InvokePlayerDead()
    {
        PlayerDead?.Invoke();
    }

    public static void InvokeGameWon()
    {
        GameWon?.Invoke();
    }

    private void startTimer()
    {
        TimerFrame.gameObject.SetActive(true);
        _startTimer = true;
    }


    private void Awake()
    {
        OpenHiddenDoor += startTimer;
        PlayerDead += activeGameOverUI;
        GameWon += hideTimer;
        GameWon += activeGameWonUI;

    }

    private void OnDestroy()
    {
        OpenHiddenDoor -= startTimer;
        PlayerDead -= activeGameOverUI;
        GameWon -= hideTimer;
        GameWon -= activeGameWonUI;
    }

    private void hideTimer()
    {
        _startTimer = false;
        TimerFrame.gameObject.SetActive(false);
    }

    private void activeGameWonUI()
    {
        gameWonUI.SetActive(true);
        GetComponent<AudioSource>().Stop();
    }
    
    private void activeGameOverUI()
    {
        gameOverUI.SetActive(true);
    }

    private void Update()
    {
        if (_startTimer)
        {
            Timer = Timer-Time.deltaTime;
            double timeLeft = Math.Round(Timer, 2);
            TimerFrame.text = timeLeft.ToString();
            if (Timer <= 0)
            {
                TimerFrame.text = "0";
                _startTimer = false;
                InvokePlayerDead();
            }
        }

    }
}
