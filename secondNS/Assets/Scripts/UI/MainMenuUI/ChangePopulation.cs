using System.Collections.Generic;
using UnityEngine;

public class ChangePopulation : MonoBehaviour
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
    Moveble MapButton;
    [SerializeField]
    Vector2 MapButtonHidePosition;
    [SerializeField]
    Moveble ExitButton;
    [SerializeField]
    Vector2 ExitButtonHidePosition;

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
            btn.enabled = false;
        }
        StartButton.position = StartButtonHidePosition;
        PopulationButton.position = PopulationButtonHidePosition;
        ExitButton.position = ExitButtonHidePosition;
        MapButton.position = MapButtonHidePosition;

        DeletePopulationPanel.position = DeletePopulationPanelPosition;
        SelectPopulationPanel.position = SelectPopulationPanelPosition;
        SelectGenerationPanel.position = SelectGenerationPanelPosition;

        BackPopulationButton.position = BackPopulationButtonPosition;
        BackPopulationButtonMouseEnter.enabled = true;
    }
}
