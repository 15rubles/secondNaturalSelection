using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButtonAnimation : MonoBehaviour
{
    [SerializeField]
    List<MoveOnMouseEnter> allbuttons;

    [SerializeField]
    Moveble StartButton;
    [SerializeField]
    Vector2 StartButtonHidePosition;
    [SerializeField]
    Moveble PopulationButton;
    [SerializeField]
    Vector2 PopulationButtonHidePosition;
    [SerializeField]
    Moveble ExitButton;
    [SerializeField]
    Vector2 ExitButtonHidePosition;

    [SerializeField]
    Moveble MapButton;
    [SerializeField]
    Vector2 MapButtonPosition;

    [SerializeField]
    Moveble SelectMapPanel;
    [SerializeField]
    Vector2 SelectMapPanelPosition;
    public void Click()
    {
        foreach (MoveOnMouseEnter btn in allbuttons)
        {
            btn.enabled = false;
        }
        StartButton.position = StartButtonHidePosition;
        PopulationButton.position = PopulationButtonHidePosition;
        ExitButton.position = ExitButtonHidePosition;
        MapButton.position = MapButtonPosition;
        SelectMapPanel.position = SelectMapPanelPosition;
    }
}
