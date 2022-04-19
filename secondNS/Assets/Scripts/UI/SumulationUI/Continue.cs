using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    [SerializeField]
    Moveble Hide;
    [SerializeField]
    Vector2 HidePosition;

    [SerializeField]
    Moveble pause;
    [SerializeField]
    Vector2 PausePosition;

    [SerializeField]
    Moveble Ccontinue;
    [SerializeField]
    Vector2 ContinueHidePosition;

    [SerializeField]
    Moveble Restart;
    [SerializeField]
    Vector2 RestartHidePosition;

    [SerializeField]
    Moveble BackToMenu;
    [SerializeField]
    Vector2 BackToMenuHidePosition;

    [SerializeField]
    Moveble Exit;
    [SerializeField]
    Vector2 ExitHidePosition;

    [SerializeField]
    Moveble Logo;
    [SerializeField]
    Vector2 LogoHidePosition;

    [SerializeField]
    TimeScale timeScale;
    public void Click()
    {
        timeScale.UpdateTimescale();
        Hide.position = HidePosition;
        pause.position = PausePosition;
        Ccontinue.position = ContinueHidePosition;
        Restart.position = RestartHidePosition;
        BackToMenu.position = BackToMenuHidePosition;
        Exit.position = ExitHidePosition;
        Logo.position = LogoHidePosition;
    }
}
