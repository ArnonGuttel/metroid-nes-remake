using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public void playAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Metroid Prototype");
    }
}
