using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Referencees

    private static AudioManager instance = null;

    #endregion

    #region Booleans



    #endregion

    #region Fields

    private AudioSource audioBGM;
    private AudioSource audioMovement;
    private AudioSource audioEnvironment;
    private AudioSource audioDamage;

    #endregion

    #region Properties

    public AudioClip damageSound, jumpTakeoffSound, run, checkpoint;

    public AudioClip menuMusic, levelMusic;

    public AudioMixerGroup groupMusic;

    public AudioMixerGroup groupSFX;



    #endregion

    #region Unity methods

    void Awake()
    {
        audioBGM = AddAudio(menuMusic, true, true, 0.2f, groupMusic);
        audioMovement = AddAudio(jumpTakeoffSound, false, false, 1f, groupSFX);
        audioEnvironment = AddAudio(checkpoint, false, false, 0.3f, groupSFX);
        audioDamage = AddAudio(damageSound, false, false, 0.6f, groupSFX);


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

    private void Start()
    {
        audioBGM.Play();
    }

    #endregion

    #region Play SFX and music metohds

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol, AudioMixerGroup group)
    {

        AudioSource newAudio = gameObject.AddComponent<AudioSource>();

        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        newAudio.outputAudioMixerGroup = group;

        return newAudio;

    }

    public void PlayMovement(string name, bool letItRepeat)
    {
        switch (name)
        {
            case "Run":
                if (audioMovement.isPlaying)
                {
                    return;
                }

                ChangeMusic(audioMovement, run, letItRepeat);
                audioMovement.volume = 0.3f;

                break;
            case "Jump":
                    audioMovement.volume = 1f;
                    ChangeMusic(audioMovement, jumpTakeoffSound, letItRepeat);
                break;
        }
        audioMovement.Play();
    }

    public void PlayDamage()
    {
        audioDamage.Play();
    }

    public void PlayEnvironment()
    {
        audioEnvironment.Play();
    }

    public void ChangeMusic(AudioSource source, AudioClip music, bool letItRepeat)
    {
        if (source.clip.name == music.name) { return; }

        source.Stop();
        source.clip = music;
        source.Play();
    }

    #endregion
}
