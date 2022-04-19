using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButtonsLoader : MonoBehaviour
{
    GameObject MapButton;
    List<GameObject> gameObjects;

    public void Start()
    {
        MapButton = Resources.Load<GameObject>("MapButton");
        gameObjects = Resources.LoadAll<GameObject>("Enviroment").ToList();
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, gameObjects.Count * 83 + (gameObjects.Count - 1) * 33);
        foreach (GameObject go in gameObjects)
        {
            GameObject gameObject = Instantiate(MapButton, transform);
            gameObject.GetComponentInChildren<Text>().text = go.GetComponent<MapName>().RusMapName;
            gameObject.GetComponent<MapButtonClick>().MapName = go.name;
        }
        GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>().SpawnScene();
    }
}
