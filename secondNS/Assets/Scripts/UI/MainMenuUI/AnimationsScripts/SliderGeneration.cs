using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderGeneration : MonoBehaviour
{
    GlobalInfo globalInfo;
    Slider slider;
    [SerializeField]
    Text text;
    [SerializeField]
    Moveble DeletePanel;
    [SerializeField]
    Vector2 DeletePanelPosition;
    public void Awake()
    {
        globalInfo = GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>();
        slider = GetComponent<Slider>();
    }

    public void OnValueChanged()
    {
        globalInfo.ChoosedGenerationNumber = (int)slider.value;
        text.text = "Популяция " + Convert.ToString(slider.value);
    }
}
