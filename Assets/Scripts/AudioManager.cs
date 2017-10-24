using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public AudioClip music;
    public AudioClip music2;
    public AudioMixerGroup musicMixer;
    private int currentMusicSources;
    private AudioSource[] musicSources;

    public AudioClip[] clips;
    public AudioMixerGroup sfxMixer;
    public int sfxSourcesCount = 5;
    private int currentSFXSources = 0;
    private AudioSource[] sfxSources;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Assigning sources to objects
	void Start ()
    {
        sfxSources = new AudioSource[sfxSourcesCount];
        for(int i = 0; i < sfxSourcesCount; ++i)
        {
            GameObject g = new GameObject("SFXSource" + i);
            g.transform.parent = transform;
            AudioSource s = g.AddComponent < AudioSource > ();
            s.outputAudioMixerGroup = sfxMixer;
            sfxSources[i] = s;
        }

        musicSources = new AudioSource[2];
        for (int i = 0; i < 2; ++i)
        {
            GameObject g = new GameObject("MusicSource" + i);
            g.transform.parent = transform;
            AudioSource s = g.AddComponent<AudioSource>();
            s.loop = true;
            s.outputAudioMixerGroup = musicMixer;
            musicSources[i] = s;
        }
        PlayMusic();
    }

    public static void PlayVariedEffect (string clipName, float variation = 0.1f)
    {
        PlayEffect(clipName, Random.Range(1 - variation, 1 + variation), Random.Range(1 - variation, 1 + variation));
    }

	public static void PlayEffect(string clipName, float pitch = 1, float volume = 1)
    {
        AudioClip clip = null;
        for(int i = 0; i < instance.clips.Length; ++i)
        {
            if (instance.clips[i].name == clipName) clip = instance.clips[i];
        }
        if (clip == null) return;

        AudioSource source = instance.sfxSources[instance.currentSFXSources];
        instance.currentSFXSources = (instance.currentSFXSources + 1) % instance.sfxSourcesCount;

        source.clip = clip;
        source.pitch = pitch;
        source.volume = volume;
        source.Play();
    }
    
    public static void PlayMusic ()
    {
        instance.musicSources[instance.currentMusicSources].clip = instance.music;
        instance.musicSources[instance.currentMusicSources].Play();
    }

    IEnumerator ChangeMusic ()
    {
        instance.musicSources[instance.currentMusicSources].Stop();
        yield return new WaitForSeconds(0.2f);
        instance.musicSources[instance.currentMusicSources].clip = instance.music2;
        instance.musicSources[instance.currentMusicSources].Play();
    }

    IEnumerator ChangeMusicBack ()
    {
        instance.musicSources[instance.currentMusicSources].Stop();
        yield return new WaitForSeconds(0.1f);
        instance.musicSources[instance.currentMusicSources].clip = instance.music;
        instance.musicSources[instance.currentMusicSources].Play();
    }

    public static void CrossfadeMusic (AudioClip clip, float duration)
    {
        AudioSource nextSource = instance.musicSources[(instance.currentMusicSources + 1) % 2];
        nextSource.clip = clip;
        instance.StartCoroutine("CrossFadeMusicCoroutine", duration);
    }
    IEnumerator CrossfadeMusicCoroutine (float duration)
    {
        AudioSource a = musicSources[currentMusicSources];
        currentMusicSources = (currentMusicSources + 1) % 2;
        AudioSource b = musicSources[currentMusicSources];
        b.volume = 0;
        b.Play();

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float frac = t / duration;
            a.volume = 1 - frac;
            b.volume = frac;
            yield return new WaitForEndOfFrame();
        }
        a.volume = 0;
        a.Stop();
        b.volume = 1; 
    }
}
