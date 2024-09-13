using UnityEngine;

public class SetMusic : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField] private GameObject imageMusicOn;
    [SerializeField] private bool play;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Music()
    {
        if (play)
        {
            audioManager.StopMusic(); 
            this.imageMusicOn.SetActive(false);
            play = false;
        }
        else
        {
            audioManager.PlayMusic();
            this.imageMusicOn.SetActive(true);
            play = true;
        }
    }
}
