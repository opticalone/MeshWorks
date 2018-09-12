using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAnimator : MonoBehaviour {
    Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();

    }

    void Update()
    {
        //Texture2DArray frames = 0;
        //var framesPerSecond = 10;
        //int index  = (Time.time * framesPerSecond) % frames.Length;
        //rend.material.mainTexture = frames[index];
    }
}
