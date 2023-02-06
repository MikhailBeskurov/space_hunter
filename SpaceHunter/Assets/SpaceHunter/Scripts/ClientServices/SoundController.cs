using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace ClientServices
{
    public enum SfxClip
    {
        Btn_click,
        Opponent_Found,
        Round_,
        YouTurn,
        ExtraMove,
        Victory,
        Draw,
        Lose,
        Joy_reaction,
        Sad_reaction,
        Swap,
        Fruit_Splash,
        booster_match,
        Missle_launch,
        Bomb_launch,
        Building_obj,
        Completed_building_apiary,
        Completed_building_tree,
        Completed_building_decoration,
        Placement_bee,
        finish_settle_bee,
        Apiary_screen,
        Honey_start,
        Honey_end,
        Replenishmen_Honey,
        farm_improve,
        improver_honey_oxygen,
        Notification,
        Successful_purchase,
        Lack_Honey,
        Error_audio,
        stake,
        unstake,
        collect_reward
    }

    public enum MusicClip
    {
        Match3_music,
        Search_opponent_sound,
        Main_music_farm
    }

    public class SoundController : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _sfxMixer;
        [SerializeField] private AudioMixerGroup _musicMixer;
        [SerializeField] private AudioMixer _sfx;
        [SerializeField] private AudioMixer _musicMaster;

        private const int _sfxPoolStartSize = 10;

        private static SoundController _instance;
        private static AudioSource _music;

        private const string _musicPrefs = "Music";
        private const string _sfxPrefs = "Sfx";
        
        private Dictionary<string, AudioClip> _sfxClips = new Dictionary<string, AudioClip>();
        private Dictionary<MusicClip, AudioClip> _musicClips = new Dictionary<MusicClip, AudioClip>();

        private Queue<AudioSource> _sfxPool = new Queue<AudioSource>();

        private void Awake()
        {
            _instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (PlayerPrefs.HasKey(_sfxPrefs))
            {
                _sfx.SetFloat("volume", PlayerPrefs.GetInt(_sfxPrefs));
            }

            if (PlayerPrefs.HasKey(_musicPrefs))
            {
                _musicMaster.SetFloat("volume", PlayerPrefs.GetInt(_musicPrefs));
            }
        }
        
        private static void Init()
        {
            CreateMusic();
            _instance.PrewarmPool();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                _sfx.SetFloat("volume", PlayerPrefs.HasKey(_sfxPrefs) ? PlayerPrefs.GetInt(_sfxPrefs) : 1); 
                _musicMaster.SetFloat("volume", PlayerPrefs.HasKey(_musicPrefs) ? PlayerPrefs.GetInt(_musicPrefs) : 1); 
            }
            else
            {
                _sfx.SetFloat("volume", -80); 
                _musicMaster.SetFloat("volume", -80); 
            }
        }

        public static void PlayMusic(MusicClip musicClip)
        {
            if (!_instance)
            {
                Init();
            }

            _instance.LoadMusic(musicClip);
        }

        public static void PlaySfx(SfxClip sfxClip)
        {
            if (!_instance)
                Init();
            _instance.PlaySfxById(sfxClip.ToString());
        }
        
        public static void PlaySfx(string sfxClip)
        {
            if (!_instance)
                Init();
            _instance.PlaySfxById(sfxClip);
        }
        
        public static float GetSfxlength(string sfxClip)
        {   
            if (!_instance)
                Init();
            
            var clip = _instance.GetSfxClip(sfxClip);
            return clip.length;
        }
        
        private void LoadMusic(MusicClip musicClip)
        {
            var clip = GetMusicClip(musicClip);
            if (clip != null)
            {
                if (_music.isPlaying && _music.clip != clip)
                {
                    _music.Stop();
                }

                _music.clip = clip;
                _music.Play();
            }
        }

        private void PlaySfxById(string sfxClip)
        {
            if (!_instance)
                Init();
            var clip = GetSfxClip(sfxClip);
            if (clip != null)
            {
                var source = GetOrCreateSfxSource();
                source.clip = clip;
                source.Play();
                StartCoroutine(StopPlaying(source, clip.length));
            }
        }

        private IEnumerator StopPlaying(AudioSource source, float clipLength)
        {
            yield return new WaitForSeconds(clipLength);
            source.Stop();
            source.clip = null;
            _sfxPool.Enqueue(source);
        }

        private void PrewarmPool()
        {
            for (int i = 0; i < _sfxPoolStartSize; i++)
            {
                _sfxPool.Enqueue(CreateNewSfx());
            }
        }

        private AudioSource GetOrCreateSfxSource()
        {
            if (_sfxPool.Count > 0)
            {
                return _sfxPool.Dequeue();
            }

            return CreateNewSfx();
        }

        private AudioSource CreateNewSfx()
        {
            var go = new GameObject("SfxSource");
            go.transform.SetParent(transform);
            var source = go.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = _sfxMixer;
            source.loop = false;
            source.playOnAwake = false;
            source.panStereo = 0;
            return source;
        }

        private static void CreateMusic()
        {
            if (!_instance)
                Init();
            _instance.CreateSource();
        }

        private AudioSource CreateSource()
        {
            var go = new GameObject("Music");
            go.transform.SetParent(transform);
            _music = go.AddComponent<AudioSource>();
            _music.outputAudioMixerGroup = _musicMixer;
            _music.playOnAwake = false;
            _music.panStereo = 0;
            _music.loop = true;
            return _music;
        }

        private AudioClip GetSfxClip(string sfxClip)
        {
            if (_sfxClips.TryGetValue(sfxClip, out var clip))
            {
                return clip;
            }

            clip = Resources.Load<AudioClip>($"Sfx/{sfxClip}");
            _sfxClips.Add(sfxClip, clip);
            return clip;
        }

        private AudioClip GetMusicClip(MusicClip musicClip)
        {
            if (_musicClips.TryGetValue(musicClip, out var clip))
            {
                return clip;
            }

            clip = Resources.Load<AudioClip>($"Music/{musicClip.ToString()}");
            _musicClips.Add(musicClip, clip);
            return clip;
        }
    }
}