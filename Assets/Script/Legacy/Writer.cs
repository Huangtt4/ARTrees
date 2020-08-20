using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Writer : MonoBehaviour
    
{
    private string myfilePath;
    // Start is called before the first frame update
    void Start()
    {

        myfilePath = Application.persistentDataPath + "/testfile.txt";

        if (File.Exists(myfilePath))
        {
            try
            {
                File.Delete(myfilePath);
                Debug.Log("file deleted");
            }
            catch (System.Exception e)
            {
                Debug.LogError("cannot delte the file!");
            }
        }
    }

     public void WriteToFile(string message)
    {
        try
        {
            StreamWriter fileWriter = new StreamWriter(myfilePath, true);

            fileWriter.Write(message);
            fileWriter.Close();
        }

        catch(System.Exception e)
        {
            Debug.LogError("cannot write into the file!");
        }
    }
}
