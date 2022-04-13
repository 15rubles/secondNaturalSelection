using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoldelGridComponent : MonoBehaviour
{
    public string FolderName;
    Text mytext;
    FolderSaver FS;
    public void Start()
    {
        mytext = GetComponentInChildren<Text>();
        FS = GameObject.Find("FolderSaver").GetComponent<FolderSaver>();
        mytext.text = FolderName;
    }
    public void FolderSelect()
    {
        FS.CurrentFolder = FolderName;
        FS.ViewFolder();
    }
}
