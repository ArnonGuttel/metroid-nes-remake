using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
    
    public void playAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Metroid Prototype");
    }
}
