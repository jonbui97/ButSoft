using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    #region Referencees

    private AudioManager _audioManager;

    #endregion

    public Sprite pole;
    public Sprite flag;
    private SpriteRenderer checkpointSpriteRenderer;
    public bool isReached;



    // Use this for initialization
    void Start ()
    {
        checkpointSpriteRenderer = GetComponent<SpriteRenderer>();
        _audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!isReached && PlayerPrefs.GetString("Continue") == "No")
            {
                _audioManager.PlayEnvironment();
            }
            checkpointSpriteRenderer.sprite = flag;
            isReached = true;
        }
    }
}
