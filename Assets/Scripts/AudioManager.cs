using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Music Sources")]
    [SerializeField] private AudioSource MusicIntro;
    [SerializeField] private AudioSource MusicLoop;

    [Header("SFX Source")]
    [SerializeField] public AudioSource Sfx;

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private float targetVolume = 0.56f; 

    public enum BgAudio
    {
        Stage,
        Waves,
        Boss,
        Death
    }

    [Header("Music Clips")]
    public AudioClip StageIntro;
    public AudioClip StageLoop;
    public AudioClip StageWavesIntro;
    public AudioClip StageWavesLoop;
    public AudioClip StageBossIntro;
    public AudioClip StageBossLoop;
    public AudioClip DeathIntro;
    public AudioClip DeathLoop;

    [Header("SFX Clips")]
    public AudioClip shootfx;
    public AudioClip Reloadfx;
    public AudioClip walkfx;
    public AudioClip dashfx;
    public AudioClip Damagedfx;
    public AudioClip deathfx;
    public AudioClip enemyatkfx;
    public AudioClip enemydeathfx;
    public AudioClip BossAtk;
    public AudioClip BossDeath;
    public AudioClip bossGrab;
    public AudioClip bossGrabHit;
    public AudioClip bossThrow;
    public AudioClip bossChange;
    public AudioClip equip;
    public AudioClip unequip;
    public AudioClip craft;
    public AudioClip Hover;


    [Header("Settings")]

    public TextMeshProUGUI SFXText;
    public TextMeshProUGUI MusicText;
    public Slider SliderSFX;
    public Slider SliderMusic;

    private Coroutine currentFadeOut;
    private BgAudio? currentTrack = null;

    public void setMusicVolume()
    {
        targetVolume = SliderMusic.value;
        MusicIntro.volume = SliderMusic.value;
        MusicLoop.volume = SliderMusic.value;
        MusicText.text = "Music : " + Mathf.RoundToInt(SliderMusic.value * 100) + "%";

        PlayerPrefs.SetFloat("MusicVolume", SliderMusic.value);
    }

    public void setSFXVolume()
    {
        Sfx.volume = SliderSFX.value;
        SFXText.text = "SFX : " + Mathf.RoundToInt(SliderSFX.value * 100) + "%";

        PlayerPrefs.SetFloat("SFXVolume", SliderSFX.value);
    }

    public void playcraftsound()
    {
        PlayClip(craft);
    }
    public void playHoverSound()
    {
        PlayClip(Hover);
    }
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume");
            SliderMusic.value = savedMusicVolume;
            targetVolume = savedMusicVolume;
            MusicIntro.volume = savedMusicVolume;
            MusicLoop.volume = savedMusicVolume;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume");
            SliderSFX.value = savedSFXVolume;
            Sfx.volume = savedSFXVolume;
        }
        setMusicVolume();
        setSFXVolume();
        PreloadClips();
        PlayMusic(BgAudio.Stage);
    }
    private void PreloadClips()
    {
        Preload(StageIntro);
        Preload(StageLoop);
        Preload(StageWavesIntro);
        Preload(StageWavesLoop);
        Preload(StageBossIntro);
        Preload(StageBossLoop);
        Preload(DeathIntro);
        Preload(DeathLoop);
    }

    private void Preload(AudioClip clip)
    {
        if (clip == null) return;
        Sfx.PlayOneShot(clip, 0f); // play silently to trigger load
    }

    public void PlayMusic(BgAudio type)
    {
        if (type == currentTrack) return; // avoid restarting same music

        currentTrack = type;

        AudioClip intro = null;
        AudioClip loop = null;

        switch (type)
        {
            case BgAudio.Stage:
                intro = StageIntro;
                loop = StageLoop;
                break;
            case BgAudio.Waves:
                intro = StageWavesIntro;
                loop = StageWavesLoop;
                break;
            case BgAudio.Boss:
                intro = StageBossIntro;
                loop = StageBossLoop;
                break;
            case BgAudio.Death:
                intro = DeathIntro;
                loop = DeathLoop;
                break;
        }

        if (currentFadeOut != null)
            StopCoroutine(currentFadeOut);

        currentFadeOut = StartCoroutine(FadeOutAndScheduleNewMusic(intro, loop));
    }

    private IEnumerator FadeOutAndScheduleNewMusic(AudioClip introClip, AudioClip loopClip)
    {
        // Only fade out if something is currently playing
        if (MusicIntro.isPlaying || MusicLoop.isPlaying)
        {   
            yield return StartCoroutine(FadeOut(MusicIntro, fadeDuration));
            yield return StartCoroutine(FadeOut(MusicLoop, fadeDuration));
        }


        double startTime = AudioSettings.dspTime + 0.1;
        double introDuration = introClip.length;

        MusicIntro.clip = introClip;
        MusicLoop.clip = loopClip;
        MusicLoop.loop = true;

        MusicIntro.volume = 0f;
        MusicLoop.volume = targetVolume;

        MusicIntro.PlayScheduled(startTime);
        MusicLoop.PlayScheduled(startTime + introDuration);

        StartCoroutine(FadeIn(MusicIntro, fadeDuration, startTime));
    }

    private IEnumerator FadeOut(AudioSource source, float duration)
    {
        float startVolume = source.volume;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        source.Stop();
        source.volume = startVolume;
    }

    private IEnumerator FadeIn(AudioSource source, float duration, double dspScheduledTime)
    {
        while (AudioSettings.dspTime < dspScheduledTime)
            yield return null;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(0f, targetVolume, t / duration);
            yield return null;
        }

        source.volume = targetVolume;
    }

    public void PlayClip(AudioClip clip)
    {
        if (clip != null)
            Sfx.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        if (currentFadeOut != null)
            StopCoroutine(currentFadeOut);

        StopAllCoroutines();
        MusicIntro.Stop();
        MusicLoop.Stop();
    }
}
