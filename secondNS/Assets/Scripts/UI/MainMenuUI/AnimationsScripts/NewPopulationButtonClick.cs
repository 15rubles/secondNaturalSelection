using UnityEngine;

public class NewPopulationButtonClick : MonoBehaviour
{
    GlobalInfo globalInfo;
    [SerializeField]
    ChangePopulation chpop;

    [SerializeField]
    Moveble DeletePanel;
    [SerializeField]
    Vector2 DeletePanelPosition;

    [SerializeField]
    Moveble ChooseGenerationPanel;
    [SerializeField]
    Vector2 ChooseGenerationPanelPosition;
    public void Awake()
    {
        globalInfo = GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>();
    }
    public void Click()
    {
        globalInfo.GenerationFolder = "None";
        if (chpop.ispressed)
        {
            DeletePanel.position = DeletePanelPosition;
            ChooseGenerationPanel.position = ChooseGenerationPanelPosition;
        }
    }
}
