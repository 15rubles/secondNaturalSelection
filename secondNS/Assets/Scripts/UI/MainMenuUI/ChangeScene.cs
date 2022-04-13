using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    Text ChangeSceneText;
    GlobalInfo globalinfo;
    public void Awake()
    {
        globalinfo = GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>();
    }
    public void Changescene()
    {
        SceneManager.LoadScene("ChangeEnviroment");
    }
    public void Update()
    {
        ChangeSceneText.text = "Current Scene: " + globalinfo.EnviromentName;
    }
}
