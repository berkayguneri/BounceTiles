using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject playButton;
    public GameObject exitButton;
    public GameObject infoButton;
    public GameObject gameTitle;

    private AudioSource audioSource;
    public AudioClip buttonClickSound;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayGame()
    {
        PlayButtonSound();
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        PlayButtonSound();
        Application.Quit();
    }
    
    public void InfoPanelOpen()
    {
        PlayButtonSound();
        infoPanel.SetActive(true);
        playButton.SetActive(false);
        exitButton.SetActive(false);
        infoButton.SetActive(false);
        gameTitle.SetActive(false);
    }
    public void InfoPanelClose()
    {
        PlayButtonSound();
        infoPanel.SetActive(false);
        playButton.SetActive(true);
        exitButton.SetActive(true);
        infoButton.SetActive(true);
        gameTitle.SetActive(true);
    }


    private void PlayButtonSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    public void LikeButton()
    {
        PlayButtonSound();
    }
}
