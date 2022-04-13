using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    GlobalInfo globalInfo;
    public void Awake()
    {
        globalInfo = GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>();
    }
    public void Start()
    {
        GameObject enviroment = Resources.Load<GameObject>("Enviroment/" + globalInfo.EnviromentName);
        Instantiate(enviroment, new Vector3(0, 0, 0), new Quaternion());
        if (globalInfo.GenerationFolder != "None")
        {

        }
    }
}
