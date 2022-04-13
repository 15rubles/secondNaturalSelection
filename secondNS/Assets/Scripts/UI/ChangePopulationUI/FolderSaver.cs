using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FolderSaver : MonoBehaviour
{
    public string CurrentFolder = "None";
    GlobalInfo globalInfo;
    GameObject folder;
    GameObject generation;
    [SerializeField]
    public GameObject FoldersContent;
    [SerializeField]
    public GameObject GenerationsContent;
    public void Awake()
    {
        folder = (GameObject)Resources.Load("Folder");
        generation = (GameObject)Resources.Load("Generation");
        globalInfo = GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>();
    }
    public void Start()
    {
        List<string> directories = Directory.GetDirectories(globalInfo.MainDiractoryPath).ToList();
        foreach (string name in directories)
        {
            GameObject go = Instantiate(folder, FoldersContent.transform);
            go.GetComponent<FoldelGridComponent>().FolderName = Path.GetDirectoryName(name);
            go.GetComponentInChildren<Text>().text = name; 
        }
    }
    public void ViewFolder()
    {
        List<SelectGeneration> todelete = GenerationsContent.GetComponentsInChildren<SelectGeneration>().ToList();
        foreach (SelectGeneration item in todelete)
        {
            Destroy(item.gameObject);
        }
        List<string> directories = Directory.GetDirectories(globalInfo.MainDiractoryPath + "/" + CurrentFolder).ToList();
        foreach (string name in directories)
        {
            GameObject go = Instantiate(generation, GenerationsContent.transform);
            go.GetComponent<SelectGeneration>().FolderGenerationName = Path.GetDirectoryName(name);
            go.GetComponentInChildren<Text>().text = name;
        }
    }
}
