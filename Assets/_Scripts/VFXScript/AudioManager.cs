using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Utility.Audio;

using AudioType = Utility.Audio.AudioType;

namespace Utility.GameFlow
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource audioSource;
        //Structs
        [Serializable]
        private struct ThemeAssociationPhase
        {
            [SerializeField] private GamePhase phase;
            [SerializeField] private string themeCode;

            public GamePhase Phase => phase;
            public string ThemeCode => themeCode;
        }

        //Static variables
        private static Func<string, ClipData> _GetAudioClipHelper;

        private static Action<ClipData> _PlayClipHelper;

        private static Action<ClipData> _PlayClipOneShotHelper;

        private static Action<ClipData, AudioSource> _PlayClipCustomHelper;

        private static Action<ClipData, AudioSource> _PlayClipOneShotCustomHelper;

        private static Action<string, float> _ChangeVolumeHelper;

        //Editor assignable variables
        [Header("REFERENCES")]
        [Header("Requirements")]
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioClipBank bank;
        [SerializeField] private AudioSource musicAudioSource, sfxAudioSource;
        [Space]
        [Header("PARAMETERS")]
        [Header("Audio Theme Flow")]
        [SerializeField] private List<ThemeAssociationPhase> themeAssociationList = new List<ThemeAssociationPhase>();

        //Utility
        private static float _master;
        private static float _music;
        private static float _sfx;
        private static float _ambient;

        //Accessors
        public static float MasterVolume
        {
            get => _master;
            private set
            {
                if (value != _master)
                {
                    _master = Mathf.Clamp01(value);

                    PlayerPrefs.SetFloat("Master", _master);
                }
            }
        }

        public static float MusicVolume
        {
            get => _music;
            private set
            {
                if (value != _music)
                {
                    _music = Mathf.Clamp01(value);

                    PlayerPrefs.SetFloat("Music", _music);
                }
            }
        }

        public static float SfxVolume
        {
            get => _sfx;
            private set
            {
                if (value != _sfx)
                {
                    _sfx = Mathf.Clamp01(value);

                    PlayerPrefs.SetFloat("SFX", _sfx);
                }
            }
        }

        public static float AmbientVolume
        {
            get => _ambient;
            private set
            {
                if (value != _ambient)
                {
                    _ambient = Mathf.Clamp01(value);

                    PlayerPrefs.SetFloat("Ambient", _ambient);
                }
            }
        }

        private void Awake()
        {
            //Clip related associations
            _GetAudioClipHelper += bank.GetClipDataReferenceByCode;

            _PlayClipHelper += Play;

            _PlayClipOneShotHelper += PlayOneShot;

            //Init volumes
            SetUpVolumes();

            //Mixer Related
            _ChangeVolumeHelper += ChangeMixerVolume;

            //Bind to phase change
            GameManager.OnPhaseChanged += ThemeChange;
        }

        private void OnDestroy()
        {
            //Unbind to phase change
            GameManager.OnPhaseChanged -= ThemeChange;
        }

        #region Behaviors

        private void ThemeChange(GamePhase phase)
        {
            ThemeAssociationPhase? themeAssociationPhase = themeAssociationList.FirstOrDefault(a => a.Phase == phase);

            if (themeAssociationPhase != null)
            {
                ClipData clipData = GetClipData(themeAssociationPhase.Value.ThemeCode);

                if (clipData != null) PlayClip(clipData);
            }
        }

        public static ClipData GetClipData(string code)
        {
            return _GetAudioClipHelper(code);
        }

        public static void PlayClip(ClipData clipData)
        {
            _PlayClipHelper?.Invoke(clipData);
        }

        public static void PlayClipOneShot(ClipData clipData)
        {
            _PlayClipOneShotHelper?.Invoke(clipData);
        }

        public static void PlayClipCustom(ClipData clipData, AudioSource targetAudioSource)
        {
            _PlayClipCustomHelper?.Invoke(clipData, targetAudioSource);
        }

        public static void PlayClipOneShotCustom(ClipData clipData, AudioSource targetAudioSource)
        {
            _PlayClipOneShotCustomHelper?.Invoke(clipData, targetAudioSource);
        }

        public static void ChangeVolume(string channelName, float volume)
        {
            _ChangeVolumeHelper?.Invoke(channelName, volume);
        }

        private void Play(ClipData clipData)
        {
            Play(clipData, SetClipToAudioSource(clipData));
        }

        private void PlayOneShot(ClipData clipData)
        {
            PlayOneShot(clipData, SetClipToAudioSource(clipData));
        }

        private void Play(ClipData clipData, AudioSource targetAudioSource)
        {
            if (!targetAudioSource) return;

            targetAudioSource.clip = clipData.clip;
            targetAudioSource.Play();
        }

        private void PlayOneShot(ClipData clipData, AudioSource targetAudioSource)
        {
            if (!targetAudioSource) return;

            targetAudioSource.PlayOneShot(clipData.clip);
        }

        private void ChangeMixerVolume(string channelName, float volume)
        {
            if (!audioMixer) return;

            float clampedValue = Mathf.Clamp01(volume);

            PlayerPrefs.SetFloat(channelName, clampedValue);
            audioMixer.SetFloat(channelName, Mathf.Log10(clampedValue == 0 ? 0.000001f : volume) * 20);
            
        }

        #endregion

        #region Utility

        private AudioSource SetClipToAudioSource(ClipData clipData)
        {
            switch (clipData.type)
            {
                case AudioType.Music:
                    return musicAudioSource;
                case AudioType.SFX:
                    return sfxAudioSource;
                default:
                    return null;
            }
        }

        private void SetUpVolumes()
        {
            MasterVolume = PlayerPrefs.GetFloat("Master", 0.5f);
            MusicVolume = PlayerPrefs.GetFloat("Music", 0.5f);
            SfxVolume = PlayerPrefs.GetFloat("SFX", 0.5f);
            AmbientVolume = PlayerPrefs.GetFloat("Ambient", 0.5f);

            ChangeMixerVolume("Master", MasterVolume);
            ChangeMixerVolume("Music", MusicVolume);
            ChangeMixerVolume("SFX", SfxVolume);
            ChangeMixerVolume("Ambient", AmbientVolume);
        }

        #endregion
    }
}