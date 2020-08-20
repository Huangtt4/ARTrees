using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEditor : MonoBehaviour
{
    // Start is called before the first frame update
    static ParticleSystem particle;
    
    void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void setup(string treetype)
    {
        switch (treetype)
        {
            case "alive":
                var main = particle.main;
                main.startColor = Color.green;
                break;

            case "dead":
                var main2 = particle.main;
                main.startColor = Color.white;
                break;

            case "future":
                var main3 = particle.main;
                main.startColor = new Color(204, 80, 157);
                break;
        }
    }

    public static void toggle(bool state)
    {
        switch(state)
        {
            case true:
                particle.Play();
                break;

            case false:
                particle.Stop();
                break;
        }
    }
}
