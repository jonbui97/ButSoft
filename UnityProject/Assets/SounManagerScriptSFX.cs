using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SounManagerScriptSFX : MonoBehaviour
{

    public static AudioClip damageSound, jumpTakeoffSound, jumpDownSound;
    static AudioSource audioSrc;

    // Use this for initialization
    void Start()
    {
        damageSound = Resources.Load<AudioClip>("damage");
        jumpTakeoffSound = Resources.Load<AudioClip>("jump_takeoff");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() { }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "damage":
                audioSrc.PlayOneShot(damageSound);
                break;
            case "jump_takeoff":
                audioSrc.PlayOneShot(jumpTakeoffSound);
                break;
        }
    }
}
