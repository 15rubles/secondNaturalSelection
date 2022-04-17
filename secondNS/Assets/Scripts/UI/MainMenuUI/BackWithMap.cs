using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackWithMap : MonoBehaviour
{
    [SerializeField]
    List<MoveOnMouseEnter> allbuttons;

    [SerializeField]
    Moveble StartButton;
    [SerializeField]
    Vector2 StartButtonPosition;
    [SerializeField]
    Moveble PopulationButton;
    [SerializeField]
    Vector2 PopulationButtonPosition;
    [SerializeField]
    Moveble MapButton;
    [SerializeField]
    Vector2 MapButtonPosition;
    [SerializeField]
    Moveble ExitButton;
    [SerializeField]
    Vector2 ExitButtonPosition;

    [SerializeField]
    Moveble SelectMapPanel;
    [SerializeField]
    Vector2 SelectMapPanelPosition;

    [SerializeField]
    Moveble BackMapButton;
    [SerializeField]
    Vector2 BackMapButtonPosition;
    [SerializeField]
    MoveOnMouseEnter BackMapButtonMouseEnter;
    public void Click()
    {
        foreach (MoveOnMouseEnter btn in allbuttons)
        {
            btn.enabled = true;
        }
        StartButton.position = StartButtonPosition;
        PopulationButton.position = PopulationButtonPosition;
        ExitButton.position = ExitButtonPosition;
        MapButton.position = MapButtonPosition;
        SelectMapPanel.position = SelectMapPanelPosition;
        BackMapButton.position = BackMapButtonPosition;
        BackMapButtonMouseEnter.enabled = true;
    }
}
