using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    SpriteRenderer hider;

    GameObject lockpanel;
    GameObject lockpanelobj;

    float hidetime = 0.5f;
    float opentime = 0.5f;

    string SceneName;
    bool hiding = false;
    bool opening = false;
    float time = 0;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        lockpanel = Resources.Load<GameObject>("Panel");
        hider = GetComponentInChildren<SpriteRenderer>();
    }
    public void Changescene(string SceneName)
    {
        this.SceneName = SceneName;
        hiding = true;
        Instantiate(lockpanel, GameObject.Find("Canvas").transform);
    }
    private void Update()
    {
        if (hiding)
        {
            time += Time.unscaledDeltaTime;
            hider.color = new Color(hider.color.r, hider.color.g, hider.color.b, time/ hidetime);
            if (time >= hidetime)
            {
                hiding = false;
                SceneManager.LoadScene(SceneName);
                lockpanelobj = Instantiate(lockpanel, GameObject.Find("Canvas").transform);
                opening = true;
                time = 0;
            }
        }
        if (opening)
        {
            time += Time.unscaledDeltaTime;
            hider.color = new Color(hider.color.r, hider.color.g, hider.color.b, Mathf.Abs(1 - time / hidetime));
            if (time - opentime >= 0)
            {
                opening = false;
                hider.color = new Color(hider.color.r, hider.color.g, hider.color.b, 0);
                time = 0;
                Destroy(lockpanelobj);
            }
        }
    }
}
