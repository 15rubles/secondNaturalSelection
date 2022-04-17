using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextInvertOnClick : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    Text ThisText;
    Color NormalColor = Color.black;
    Color SelectedColor = Color.white;
    private void Awake()
    {
        ThisText = GetComponentInChildren<Text>();
    }
    private void Start()
    {
        ThisText.color = NormalColor;
    }
    public void OnSelect(BaseEventData eventData)
    {
        ThisText.color = SelectedColor;
    }
    public void OnDeselect(BaseEventData eventData)
    {
        ThisText.color = NormalColor;
    }
}
