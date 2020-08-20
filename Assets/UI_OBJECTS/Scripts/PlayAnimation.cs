using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class PlayAnimation : MonoBehaviour
{
    [SerializeField] private VideoPlayer stom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stom.isPlaying)
        {
            this.GetComponent<Image>().enabled = false;
        }
        else
        {
            this.GetComponent<Image>().enabled = true;
        }
    }


    public void Play()
    {
        stom.Play();
    }
}
