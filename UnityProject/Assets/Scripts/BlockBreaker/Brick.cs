using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public Sprite[] sprites;
    public AudioClip crack;
    public bool isBreakable = true;
    public static int breakableCount;
    public GameObject Smoke;

    private int timesHit;
    private int maxHits;

    private BlockBreakerLevelManager levelManager;

	void Start ()
	{
	    timesHit = 0;
	    maxHits = sprites.Length + 1;

	    if (isBreakable)
	    {
	        breakableCount++;
	    }

	    levelManager = GameObject.FindObjectOfType<BlockBreakerLevelManager>();
	}
	
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (isBreakable)
        {
            AudioSource.PlayClipAtPoint(crack,transform.position, 0.5f);
            TakeHit();
        }
    }

    private void TakeHit()
    {
        timesHit++;

        if (timesHit < maxHits)
        {
            LoadSprites();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (timesHit >= maxHits)
        {
            breakableCount--;
            levelManager.BrickDestroyed();
            PuffSmoke();
            Destroy(gameObject);
        }
    }

    private void PuffSmoke()
    {
        GameObject smokePuff = Instantiate(Smoke, transform.position, Quaternion.identity);
        Color brickColor = gameObject.GetComponent<SpriteRenderer>().color;
        var temp = smokePuff.GetComponent<ParticleSystem>().main;
        temp.startColor = brickColor;
    }

    private void LoadSprites()
    {
        int spriteIndex = timesHit - 1;

        if (sprites[spriteIndex])
        {
            this.GetComponent<SpriteRenderer>().sprite = sprites[spriteIndex];
        }
    }
}
