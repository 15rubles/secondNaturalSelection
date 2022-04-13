using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour
{
    [SerializeField]
    public string EnviromentName;
    public void ChangeSelectedScene()
    {
        GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>().EnviromentName = EnviromentName;
    }
}
