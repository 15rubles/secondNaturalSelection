using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeScale : MonoBehaviour
{
    public bool _lockAll = false;
    [SerializeField]
    public float timescale = 1;
    Text text;
    public void Start()
    {
        text = GetComponent<Text>();
        UpdateTimescale(true);
    }
    public void UpdateTimescale(bool activate)
    {
        if(activate) Time.timeScale = timescale;
        text.text = "x" + Convert.ToString(timescale);
    }
}
