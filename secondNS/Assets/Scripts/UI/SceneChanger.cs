using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    GameObject hidergo;
    SpriteRenderer hider;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        hider = GetComponentInChildren<SpriteRenderer>();
        hidergo = GetComponentInChildren<Transform>().gameObject;
    }
    public void Changescene(string SceneName)
    {

    }
}
