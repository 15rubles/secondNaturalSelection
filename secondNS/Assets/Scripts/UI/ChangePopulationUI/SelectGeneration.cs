using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectGeneration : MonoBehaviour
{
    public string FolderGenerationName = "None";
    public void Select()
    {
        GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>().GenerationName = FolderGenerationName;
        SceneManager.LoadScene("MainMenu");
    }
}
