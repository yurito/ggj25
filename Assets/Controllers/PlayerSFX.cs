using UnityEngine;

public class PlayerSFX : MonoBehaviour
{

    public AudioSource audioSource;

    public void PlayerSound()
    {
        Debug.Log("Player Sound");
        audioSource.Play();
    }

}
