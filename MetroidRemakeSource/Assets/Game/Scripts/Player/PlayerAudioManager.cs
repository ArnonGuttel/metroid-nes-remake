using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    #region Inspector

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource playerHitSound;
    [SerializeField] private AudioSource playerDeadSound;

    #endregion

    public void playJump()
    {
        jumpSound.Play(0);
    }


    public void playPlayerHit()
    {
        playerHitSound.Play(0);
    }

    public void playPlayerDead()
    {
        playerDeadSound.Play(0);
    }
}