using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newton_gif : MonoBehaviour {

    public Texture[] frames;
    public int framesPerSecond = 5;

    public void Update()
    {
       int index = (int)(Time.time * framesPerSecond) % frames.Length;
        GetComponent<Renderer>().material.mainTexture = frames[index];
    }
}
