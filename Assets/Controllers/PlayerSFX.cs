using UnityEngine;

public class PlayerSFX : MonoBehaviour
{

    public AudioSource audioSource;

    public void PlayerSound()
    {
        audioSource.Play();
    }

}
