using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinButton : MonoBehaviour
{
    [SerializeField]
    Moveble DeletePanel;
    [SerializeField]
    Vector2 DeletePanelPosition;
    public void Click()
    {
        DeletePanel.position = DeletePanelPosition;
    }
}
