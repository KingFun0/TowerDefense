using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.N.Fridman.SoundVolumeController.Scripts
{
    public class SoundVolumeControllerComponent : MonoBehaviour
    {
        private List<AudioClip> _tracks = new List<AudioClip>();
        private bool _isPlaying = true;
        private int _currentTrackIndex = 0;
        [Header("Components")]
        [Tooltip("Audio Source Does Тot Connect Automatically")]
        [SerializeField] private AudioSource _audio;
        [Tooltip("Slider Search Using A Tag")]
        [SerializeField] private Slider _slider;
        [Tooltip("Text Search Using A Tag")]
        [SerializeField] private Text _text;
        [Tooltip("Button Search Using A Tag")]
        [SerializeField] private Button _playButton;
        [Tooltip("Button Search Using A Tag")]
        [SerializeField] private Button _pauseButton;
        [Tooltip("Button Search Using A Tag")]
        [SerializeField] private Button _prevButton;
        [Tooltip("Button Search Using A Tag")]
        [SerializeField] private Button _nextButton;

        [Header("Keys")]
        [Tooltip("Save Data PlayerPrefs Key")]
        [SerializeField] private string _saveVolumeKey;
        [Tooltip("Save Data PlayerPrefs Key")]
        [SerializeField] private string _savePlayKey;
        [Tooltip("Save Data PlayerPrefs Key")]
        [SerializeField] private string _savePauseKey;
        [Tooltip("Save Data PlayerPrefs Key")]
        [SerializeField] private string _savePrevKey;
        [Tooltip("Save Data PlayerPrefs Key")]
        [SerializeField] private string _saveNextKey;


        [Header("Tags")]
        [Tooltip("Volume Control Slider Tag")]
        [SerializeField] private string _sliderTag;
        [Tooltip("Volume Control Text Tag")]
        [SerializeField] private string _textVolumeTag;
        [Tooltip("Play Control Button Tag")]
        [SerializeField] private string _playTag;
        [Tooltip("Pause Control Button Tag")]
        [SerializeField] private string _pauseTag;
        [Tooltip("Prev Control Button Tag")]
        [SerializeField] private string _prevTag;
        [Tooltip("Next Control Button Tag")]
        [SerializeField] private string _nextTag;

        [Header("Parameters")]
        [Tooltip("Sound Volume Value")]
        [SerializeField][Range(0.0f, 1.0f)] private float _volume;
        [Header("savePlayMusic")]
        [SerializeField] private int _savePlayMusic;
        [Header("savePauseMusic")]
        [SerializeField] private int _savePauseMusic;
        [Header("savePrevMusic")]
        [SerializeField] private int _savePrevMusic;
        [Header("saveNextMusic")]
        [SerializeField] private int _saveNextMusic;
        private void Start()
        {
            _audio = GetComponent<AudioSource>();
            AudioClip[] loadedTracks = Resources.LoadAll<AudioClip>("Music/Sounds");
            _currentTrackIndex = PlayerPrefs.GetInt("CurrentTrackIndex", _currentTrackIndex);
            _tracks.AddRange(loadedTracks);
            if (_pauseButton != null)
            {
                _pauseButton.gameObject.SetActive(false);
            }
            
        }
        private void NextAudio()
        {
            _currentTrackIndex = (_currentTrackIndex + 1) % _tracks.Count;
            if (_isPlaying)
            {
                PlayAudio();
            }
        }
        private void PrevAudio()
        {
            _currentTrackIndex = (_currentTrackIndex - 1 + _tracks.Count) % _tracks.Count;
            if (_isPlaying)
            {
                PlayAudio();
            }
        }
        private void PlayAudio()
        {
            if (_currentTrackIndex < _tracks.Count)
            {
                _audio.clip = _tracks[_currentTrackIndex];
                _audio.Play();
                _isPlaying = true;
                this._savePlayMusic = 1;
                this._savePauseMusic = 0;
            }
        }
        private void PauseAudio()
        {

            _audio.Stop();
            _isPlaying = false;
            this._savePlayMusic = 0;
            this._savePauseMusic = 1;

        }

        private void Awake()
        {
            //if (!_audio.isPlaying && !_isPlaying)
            //{
            //    PlayAudio();
            //}
            if (PlayerPrefs.HasKey(this._saveVolumeKey))
            {
                this._volume = PlayerPrefs.GetFloat(this._saveVolumeKey);
                this._audio.volume = this._volume;

                GameObject sliderObj = GameObject.FindWithTag(this._sliderTag);
                if (sliderObj != null)
                {
                    this._slider = sliderObj.GetComponent<Slider>();
                    this._slider.value = this._volume;
                }
            }
            else
            {
                this._volume = 0.5f;
                PlayerPrefs.SetFloat(this._saveVolumeKey, this._volume);
                this._audio.volume = this._volume;
            }
            if (PlayerPrefs.HasKey(this._savePlayKey))
            {
                this._savePlayMusic = PlayerPrefs.GetInt(this._savePlayKey);
                if (this._savePlayMusic == 0)
                {

                    GameObject playObj = GameObject.FindWithTag(this._playTag);
                    if (playObj != null)
                    {
                        this._playButton = playObj.GetComponent<Button>();
                       // _playButton.onClick.AddListener(PlayAudio);
                    }
                }
            }
            if (PlayerPrefs.HasKey(this._savePauseKey))
            {
                this._savePauseMusic = PlayerPrefs.GetInt(this._savePauseKey);
                if (this._savePauseMusic == 0)
                {
                    GameObject pauseObj = GameObject.FindWithTag(this._pauseTag);
                    if (pauseObj != null)
                    {
                        this._pauseButton = pauseObj.GetComponent<Button>();
                        _pauseButton.onClick.AddListener(PauseAudio);
                    }
                }
            }
            if (PlayerPrefs.HasKey(this._savePrevKey))
            {
                this._savePrevMusic = PlayerPrefs.GetInt(this._savePrevKey);
                if (this._savePrevMusic == 0)
                {
                    GameObject prevObj = GameObject.FindWithTag(this._prevTag);
                    if (prevObj != null)
                    {
                        this._prevButton = prevObj.GetComponent<Button>();
                        _prevButton.onClick.AddListener(PrevAudio);
                    }
                }
            }
            if (PlayerPrefs.HasKey(this._saveNextKey))
            {
                this._saveNextMusic = PlayerPrefs.GetInt(this._saveNextKey);
                if (this._saveNextMusic == 0)
                {
                    GameObject nextObj = GameObject.FindWithTag(this._nextTag);
                    if (nextObj != null)
                    {
                        this._nextButton = nextObj.GetComponent<Button>();
                        _nextButton.onClick.AddListener(NextAudio);
                    }
                }
            }
        }
        private void Update()
        {
            if (_audio.isPlaying)
            {
                _isPlaying = true;
            }
            else
            {
                _isPlaying = false;
            }
            if (_playButton != null)
            {
                _playButton.gameObject.SetActive(!_isPlaying);
            }
            if (_pauseButton != null)
            {
                _pauseButton.gameObject.SetActive(_isPlaying);
            }
        }


        private void LateUpdate()
        {
            GameObject sliderObj = GameObject.FindWithTag(this._sliderTag);
            if (sliderObj != null)
            {
                this._slider = sliderObj.GetComponent<Slider>();
                this._volume = _slider.value;

                if (this._audio.volume != this._volume)
                {
                    PlayerPrefs.SetFloat(this._saveVolumeKey, this._volume);
                }

                GameObject textObj = GameObject.FindWithTag(this._textVolumeTag);
                if (textObj != null)
                {
                    this._text = textObj.GetComponent<Text>();

                    this._text.text = Mathf.Round(this._volume * 100) + "%";
                }
            }

            this._audio.volume = this._volume;
            GameObject playButtonObj = GameObject.FindWithTag(this._playTag);
            if (playButtonObj != null)
            {
                this._playButton = playButtonObj.GetComponent<Button>();

                
                    _playButton.onClick.AddListener(PlayAudio);
            
                if (_audio.isPlaying && _isPlaying)
                {
                    _audio.Play();
                }
            }
            GameObject pauseButtonObj = GameObject.FindWithTag(this._pauseTag);
            if (pauseButtonObj != null)
            {
                this._pauseButton = pauseButtonObj.GetComponent<Button>();

           
                    _pauseButton.onClick.AddListener(PauseAudio);
             
                if (!_audio.isPlaying && !_isPlaying)
                {
                    _audio.Stop();
                }
            }
            GameObject prevButtonObj = GameObject.FindWithTag(this._prevTag);
            if (prevButtonObj != null)
            {
                this._prevButton = prevButtonObj.GetComponent<Button>();
            
                    _prevButton.onClick.AddListener(PrevAudio);
            
            }
            GameObject nextButtonObj = GameObject.FindWithTag(this._nextTag);
            if (nextButtonObj != null)
            {
                this._nextButton = nextButtonObj.GetComponent<Button>();
            
                    _nextButton.onClick.AddListener(NextAudio);
            }
        }
    }
}
