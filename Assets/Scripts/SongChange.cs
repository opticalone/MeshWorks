using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongChange : MonoBehaviour {

    AudioSource source;
    int counter = 0;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();    
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void nextSong()
    {
        ++counter;
        if (counter < 1)
        {
            source.Stop();
        }
        if (counter == 1)
        {
            source.clip = clip1;
            source.Play();
        }
        if (counter == 2)
        {
            source.clip = clip2;
            source.Play();
        }
        if (counter == 3)
        {
            source.clip = clip3;
            source.Play();
        }
        if (counter == 3)
        {
            counter = 0;
          
        }
        Debug.Log("counter = " + counter);

    }
}
