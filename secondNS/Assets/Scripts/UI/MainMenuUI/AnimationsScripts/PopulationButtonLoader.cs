using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PopulationButtonLoader : MonoBehaviour
{
    GlobalInfo globalInfo;
    GameObject PopulationToggle;
    List<string> directories;
    List<Toggle> alltoggles = new List<Toggle>();
    [SerializeField]
    Toggle NewPopulationToggle;
    ToggleGroup MyToggleGroup;
    public void Awake()
    {
        MyToggleGroup = GetComponent<ToggleGroup>();
        globalInfo = GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>();
        PopulationToggle = Resources.Load<GameObject>("PopulationToggle");
    }
    public void Start()
    {
        directories = Directory.GetDirectories(globalInfo.projectPath).ToList();
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, (directories.Count + 1) * 83 + directories.Count * 33);
        if (globalInfo.GenerationFolder == "None")
        {
            NewPopulationToggle.isOn = true;
        }
        foreach (string name in directories)
        {
            GameObject gameObject = Instantiate(PopulationToggle, transform);
            gameObject.GetComponentInChildren<Text>().text = "Поп. " + name.Split('_')[1];
            alltoggles.Add(gameObject.GetComponent<Toggle>());
            alltoggles.Last().group = MyToggleGroup;
            if (globalInfo.GenerationFolder == name)
            {
                alltoggles.Last().isOn = true;
            }
            gameObject.GetComponent<PopulationButtonClick>().FolderName = name;
        }
    }
}
