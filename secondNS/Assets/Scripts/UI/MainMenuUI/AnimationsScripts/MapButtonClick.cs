using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButtonClick : MonoBehaviour
{
    public string MapName;
    GlobalInfo globalInfo;
    GameObject Enviroment;
    public void Awake()
    {
        globalInfo = GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>();
        Enviroment = GameObject.Find("Enviroment");
    }
    public void Click()
    {
        GameObject map = Resources.Load<GameObject>("Enviroment/" + MapName);
        globalInfo.EnviromentName = MapName;
        foreach (Transform item in Enviroment.GetComponentsInChildren<Transform>())
        {
            Destroy(item.gameObject);
        }
        Instantiate(map, Enviroment.transform);
    }
}
