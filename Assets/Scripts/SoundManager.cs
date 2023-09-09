using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip scene1AudioClip;
    public AudioClip scene2AudioClip;

    private AudioSource audioSource;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of SoundManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Destroy duplicate instances
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Play the appropriate audio clip for the current scene
        PlayAudioForCurrentScene();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Play the appropriate audio clip when a new scene is loaded
        PlayAudioForCurrentScene();
    }

    private void PlayAudioForCurrentScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "MenuScene")
        {
            audioSource.clip = scene1AudioClip;
        }
        else if (sceneName == "MainScene")
        {
            audioSource.clip = scene2AudioClip;
        }

        // Restart the audio clip
        audioSource.Play();
    }
}


