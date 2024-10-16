using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public Animator animator;
    private AudioSource audioSource;
    public AudioClip clickAC;
    public static UIHandler instance;

    [Header("Settings")]
    public GameObject settingsOpen;
    public GameObject settingsClose;
    public GameObject soundOn;
    public GameObject soundOf;
    public GameObject vibrationOn;
    public GameObject vibrationOf;
    public GameObject information;

    [Header("Score")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    int highScore = 0;
    int score = 0;


    public int colorIndex = 0;
    int achievement = 50;
    [HideInInspector] public Color[] standColor = new Color[] { Color.red, Color.green, Color.blue, Color.yellow, Color.black, Color.gray, Color.magenta };


    public GameObject bigJump;
    public GameObject smallJump;
    public GameObject scoreImage;
    public GameObject settingsOpenImage;
    public GameObject settingsCloseImage;
    private void Awake()
    {
        instance = this; 
        audioSource = GetComponent<AudioSource>();

    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
      
        highScore = PlayerPrefs.GetInt("Highscore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();

        if (PlayerPrefs.HasKey("Sound")==false)
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        if (!PlayerPrefs.HasKey("Vibration"))
        {
            PlayerPrefs.SetInt("Vibration", 1);
        }

        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            AudioListener.volume = 1;  // Sound is open 
        }
        else if (PlayerPrefs.GetInt("Sound") == 2)
        {
            AudioListener.volume = 0;  // Sound is close
        }
    }

 

    private void Update()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("Highscore", highScore);
            highScoreText.text = "High Score: " + highScore.ToString(); 
        }

        if (Player.instance.isDead == true)
        {
            bigJump.SetActive(false);
            smallJump.SetActive(false);
            scoreImage.SetActive(false);
            settingsOpenImage.SetActive(false);
            settingsCloseImage.SetActive(false);
        }
    }


    public void ScoreUpdate()
    {
        score = int.Parse(scoreText.text)+2;
        scoreText.text = score.ToString();

    }

    public void AchievementAchieved()
    {
        int score = int.Parse(scoreText.text);
        if (score >= achievement)
        {
            colorIndex = (colorIndex >= standColor.Length - 1) ? 0 : colorIndex + 1;
            achievement *= 2;
            BackgroundFade.instance.StartBackgroundTransition();
        }
    }

    public Color GetTileColor()
    {
        return standColor[colorIndex];
    }

    public void SettingsOpen()
    {
        audioSource.PlayOneShot(clickAC);
        settingsOpen.SetActive(false);
        settingsClose.SetActive(true);
        animator.SetTrigger("slideIn");

        if (PlayerPrefs.GetInt("Sound") == 1) // Music is open
        {
            soundOn.SetActive(true);
            soundOf.SetActive(false);
            AudioListener.volume = 1;
        }

        else if (PlayerPrefs.GetInt("Sound") == 2)
        {
            soundOn.SetActive(false);
            soundOf.SetActive(true);
            AudioListener.volume = 0;
        }

        if (PlayerPrefs.GetInt("Vibration") == 1)
        {
            vibrationOn.SetActive(true);
            vibrationOf.SetActive(false);
        }

        else if (PlayerPrefs.GetInt("Vibration") == 2)
        {
            vibrationOn.SetActive(false);
            vibrationOf.SetActive(true);
        }

    }

    public void SettingsClose()
    {
        audioSource.PlayOneShot(clickAC);
        settingsOpen.SetActive(true);
        settingsClose.SetActive(false);
        animator.SetTrigger("slideOut");
    }

    public void SoundOn()
    {
        audioSource.PlayOneShot(clickAC);
        soundOn.SetActive(false);
        soundOf.SetActive(true);
        AudioListener.volume = 0;
        PlayerPrefs.SetInt("Sound", 2);
    }

    public void SoundOff()
    {
        audioSource.PlayOneShot(clickAC);
        soundOn.SetActive(true);
        soundOf.SetActive(false);
        AudioListener.volume = 1;
        PlayerPrefs.SetInt("Sound", 1);
    }

    public void VibrationOn()
    {
        audioSource.PlayOneShot(clickAC);
        vibrationOn.SetActive(false);
        vibrationOf.SetActive(true);
        PlayerPrefs.SetInt("Vibration", 2);
    }

    public void VibrationOf()
    {
        audioSource.PlayOneShot(clickAC);
        vibrationOn.SetActive(true);
        vibrationOf.SetActive(false);
        PlayerPrefs.SetInt("Vibration", 1);

    }

    public void Information()
    {
        information.SetActive(true);
        audioSource.PlayOneShot(clickAC);
    } 

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://linktr.ee/berkayguneri");
    }
    
    public void TermOfUse()
    {
        Application.OpenURL("https://linktr.ee/berkayguneri");
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
