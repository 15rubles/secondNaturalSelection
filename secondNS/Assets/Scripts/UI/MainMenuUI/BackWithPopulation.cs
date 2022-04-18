using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackWithPopulation : MonoBehaviour
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
    Moveble DeletePopulationPanel;
    [SerializeField]
    Vector2 DeletePopulationPanelPosition;

    [SerializeField]
    Moveble SelectPopulationPanel;
    [SerializeField]
    Vector2 SelectPopulationPanelPosition;

    [SerializeField]
    Moveble SelectGenerationPanel;
    [SerializeField]
    Vector2 SelectGenerationPanelPosition;

    [SerializeField]
    Moveble BackPopulationButton;
    [SerializeField]
    Vector2 BackPopulationButtonPosition;
    [SerializeField]
    MoveOnMouseEnter BackPopulationButtonMouseEnter;
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

        DeletePopulationPanel.position = DeletePopulationPanelPosition;
        SelectPopulationPanel.position = SelectPopulationPanelPosition;
        SelectGenerationPanel.position = SelectGenerationPanelPosition;

        BackPopulationButton.position = BackPopulationButtonPosition;
        BackPopulationButtonMouseEnter.enabled = false;
    }
}
