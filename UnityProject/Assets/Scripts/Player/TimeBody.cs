using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public float recordTime = 5f;

    private player player;

    private bool isRewinding = false;
    private bool isRecording = false;
    
    private List<Vector3> positions;


	// Use this for initialization
	void Start ()
	{
	    this.player = GetComponent<player>();
		positions = new List<Vector3>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void FixedUpdate()
    {
        if (this.player.currHealth <= 0)
        {
            StopRewind();
            return;
        }

        if (isRewinding && this.player.currHealth > 0)
        {
            Rewind();
        }

        if (isRecording)
        {
            Record();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Teleporter")
        {
            StartRecording();
            StopRewind();
        }
        if (other.gameObject.tag == "Trap")
        {
            StopRecording();
            StartRewind();
        }
    }

    void Record()
    {
        if (positions.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            positions.RemoveAt(positions.Count - 1);
        }
        positions.Insert(0, transform.position);
    }

    void Rewind()
    {
        if (positions.Count > 0)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }
    
    void StartRewind()
    {
        isRewinding = true;
        PlayerPrefs.SetString("Rewind", "yes");
    }

    void StopRewind()
    {
        isRewinding = false;
        PlayerPrefs.SetString("Rewind", "no");
        //var temp = transform.position;
        //temp.y += 0.2f;
        //transform.position = temp;
    }

    void StartRecording()
    {
        isRecording = true;
    }

    void StopRecording()
    {
        isRecording = false;
    }
}
