using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnviromentFiller : MonoBehaviour
{
    [SerializeField]
    GameObject EnviromentGrid;
    GameObject enviroment;
    public void Awake()
    {
        enviroment = Resources.Load<GameObject>("scene");
    }
    public void Start()
    {
        List<GameObject> gameObjects = Resources.LoadAll<GameObject>("Enviroment").ToList();
        foreach (GameObject item in gameObjects)
        {
            GameObject go = Instantiate(enviroment, EnviromentGrid.transform);
            go.GetComponent<GridElement>().EnviromentName = item.name;
            go.GetComponentInChildren<Text>().text = item.name;
        }
    }
}
