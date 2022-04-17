using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButtonClick : MonoBehaviour
{
    public string MapName;
    GlobalInfo globalInfo;
    GameObject empty;
    public void Awake()
    {
        globalInfo = GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>();
        empty = Resources.Load<GameObject>("Empty");
    }
    public void Click()
    {
        Destroy(GameObject.Find("Enviroment"));
        GameObject map = Resources.Load<GameObject>("Enviroment/" + MapName);
        globalInfo.EnviromentName = MapName;
        GameObject env = Instantiate(empty);
        env.name = "Enviroment";
        Instantiate(map, env.transform);
    }
}
