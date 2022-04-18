using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PopulationButtonClick : MonoBehaviour
{
    public string FolderName;
    GlobalInfo globalInfo;
    Slider slider;
    Text maxtext;
    ChangePopulation chpop;

    Moveble DeletePanel;
    [SerializeField]
    Vector2 DeletePanelPosition;

    Moveble ChooseGenerationPanel;
    [SerializeField]
    Vector2 ChooseGenerationPanelPosition;
    public void Awake()
    {
        globalInfo = GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>();
    }
    public void Start()
    {
        DeletePanel = GameObject.Find("DeletePanel").GetComponent<Moveble>();
        ChooseGenerationPanel = GameObject.Find("SelectGenerationPanel").GetComponent<Moveble>();
        slider = GameObject.Find("SelectGenerationPanel").GetComponentInChildren<Slider>();
        chpop = GameObject.Find("Population").GetComponent<ChangePopulation>();
        maxtext = GameObject.Find("RightVal").GetComponent<Text>();
    }
    public void Click()
    {
        globalInfo.GenerationFolder = FolderName;
        slider.value = globalInfo.ChoosedGenerationNumber;
        slider.maxValue = Directory.GetDirectories(globalInfo.projectPath + "/" + FolderName).Length;
        maxtext.text = Convert.ToString(slider.maxValue);
        if (chpop.ispressed)
        {
            DeletePanel.position = DeletePanelPosition;
            ChooseGenerationPanel.position = ChooseGenerationPanelPosition;
        }
    }
}
