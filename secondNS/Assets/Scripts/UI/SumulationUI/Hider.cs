using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hider : MonoBehaviour
{
    Toggle toggle;

    [SerializeField]
    Moveble Timer;
    [SerializeField]
    Vector2 TimerHidePosition;
    [SerializeField]
    Vector2 TimerNormalPosition;

    [SerializeField]
    Moveble TimeScale;
    [SerializeField]
    Vector2 TimeScaleHidePosition;
    [SerializeField]
    Vector2 TimeScaleNormalPosition;

    [SerializeField]
    Moveble LeftArrow;
    [SerializeField]
    Vector2 LeftArrowHidePosition;
    [SerializeField]
    Vector2 LeftArrowNormalPosition;

    [SerializeField]
    Moveble RightArrow;
    [SerializeField]
    Vector2 RightArrowHidePosition;
    [SerializeField]
    Vector2 RightArrowNormalPosition;

    [SerializeField]
    Moveble Pause;
    [SerializeField]
    Vector2 PauseHidePosition;
    [SerializeField]
    Vector2 PauseNormalPosition;

    [SerializeField]
    Image image;
    public void Start()
    {
        toggle = GetComponent<Toggle>();
    }
    public void ValueChange()
    {
        if (toggle.isOn)
        {
            image.enabled = false;
            Timer.position = TimerNormalPosition;
            TimeScale.position = TimeScaleNormalPosition;
            LeftArrow.position = LeftArrowNormalPosition;
            RightArrow.position = RightArrowNormalPosition;
            Pause.position = PauseNormalPosition;
        }
        else
        {
            image.enabled = true;
            Timer.position = TimerHidePosition;
            TimeScale.position = TimeScaleHidePosition;
            LeftArrow.position = LeftArrowHidePosition;
            RightArrow.position = RightArrowHidePosition;
            Pause.position = PauseHidePosition;
        }
    }
}
