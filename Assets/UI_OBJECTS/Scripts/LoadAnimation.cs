using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadAnimation : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(JumpToScene());
    }

    IEnumerator JumpToScene()
    {
        // Set to the length of the animation
        yield return new WaitForSeconds(17.9f);

        // Return to main scene

        SceneManager.LoadScene("Scene1");
    }

    public void LoadStomata()
    {

        SceneManager.LoadScene("StomataAnimation");

       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}