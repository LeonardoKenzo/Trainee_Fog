using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    public static MusicManager Instance => instance;
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioClip _music;
    private AudioSource _audioSource;

    void Awake()
    {
        //guarantee that it is the unique Music Manager in the Scene
        if(instance == null)
        {
            instance = this;
            _audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(_music != null)
        {
            PlayMusic(false, _music);
        }

        _slider.onValueChanged.AddListener(delegate { SetVolume(_slider.value); });
    }

    public static void SetVolume(float volume)
    {
        instance._audioSource.volume = volume;
    }

    public static void PlayMusic(bool _resetMusic, AudioClip _audioClip = null)
    {
        if (_audioClip != null)
        {
            Instance._audioSource.clip = _audioClip;
        }
        else if(Instance._audioSource.clip != null)
        {
            if (_resetMusic)
                Instance._audioSource.Stop();
            Instance._audioSource.Play();
        }
    }

    public static void PauseMusic()
    {
        Instance._audioSource.Pause();
    }
}
