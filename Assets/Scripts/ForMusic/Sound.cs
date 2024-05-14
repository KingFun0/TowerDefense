using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    private List<AudioClip> tracks = new List<AudioClip>();
    private AudioSource audioSource;
    private int currentTrackIndex = 0;
    private bool isPlaying = false;

    public Button playButton;
    public Button pauseButton;
    public Button prevButton;
    public Button nextButton;
    public Slider volumeSlider;

    private bool isPlayButtonPressed;
    private bool isPauseButtonPressed;
    private bool isPrevButtonPressed;
    private bool isNextButtonPressed;
    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", volumeSlider.value);
        currentTrackIndex = PlayerPrefs.GetInt("CurrentTrackIndex", currentTrackIndex);
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.volume = savedVolume;
        volumeSlider.value = savedVolume;

        if (playButton != null)
        {
            playButton.onClick.AddListener(PlayMusic);
        }
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(PauseMusic);
        }
        if (prevButton != null)
        {
            prevButton.onClick.AddListener(PlayPreviousTrack);
        }
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(PlayNextTrack);
        }
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }

        AudioClip[] loadedTracks = Resources.LoadAll<AudioClip>("Music/Sounds");
        tracks.AddRange(loadedTracks);

        if (pauseButton != null)
        {
            pauseButton.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (audioSource.isPlaying)
        {
            isPlaying = true;
        }
        else
        {
            isPlaying = false;
        }

        if (playButton != null)
        {
            playButton.gameObject.SetActive(!isPlaying);
        }
        if (pauseButton != null)
        {
            pauseButton.gameObject.SetActive(isPlaying);
        }
    }

    public void PlayMusic()
    {
        if (currentTrackIndex < tracks.Count)
        {
            audioSource.clip = tracks[currentTrackIndex];
            audioSource.Play();
            isPlaying = true;
        }
        isPlayButtonPressed = true;
    }

    public void PauseMusic()
    {
        audioSource.Pause();
        isPlaying = false;
        PlayerPrefs.SetInt("CurrentTrackIndex", currentTrackIndex);
        isPauseButtonPressed = true;
    }

    public void PlayPreviousTrack()
    {
        currentTrackIndex = (currentTrackIndex - 1 + tracks.Count) % tracks.Count;
        if (isPlaying)
        {
            PlayMusic();
        }
        PlayerPrefs.SetInt("CurrentTrackIndex", currentTrackIndex);
        isPrevButtonPressed = true;
    }

    public void PlayNextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % tracks.Count;
        if (isPlaying)
        {
            PlayMusic();
        }
        PlayerPrefs.SetInt("CurrentTrackIndex", currentTrackIndex);
        isNextButtonPressed = true;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("IsPlayButtonPressed", isPlayButtonPressed ? 1 : 0);
        PlayerPrefs.SetInt("IsPauseButtonPressed", isPauseButtonPressed ? 1 : 0);
        PlayerPrefs.SetInt("IsPrevButtonPressed", isPrevButtonPressed ? 1 : 0);
        PlayerPrefs.SetInt("IsNextButtonPressed", isNextButtonPressed ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
