using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager _shared;
    private bool _resetFlag;
    
    public List<GameObject> DeadEnemies;

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
}
