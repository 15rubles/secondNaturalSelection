using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrashBag : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Toggle freegeneration;
    [SerializeField]
    ToggleGroup tg;
    [SerializeField]
    GlobalInfo globalInfo;
    public void Awake()
    {
        globalInfo = GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        List<Toggle> toggle = tg.ActiveToggles().ToList();
        PopulationButtonClick pbc = toggle[0].gameObject.GetComponent<PopulationButtonClick>();
        Directory.Delete(globalInfo.projectPath + "/" + pbc.FolderName, true);
        if (File.Exists(globalInfo.projectPath + "/" + pbc.FolderName + ".meta"))
            File.Delete(globalInfo.projectPath + "/" + pbc.FolderName + ".meta");
        Destroy(toggle[0].gameObject);
        freegeneration.isOn = true;
    }
}
