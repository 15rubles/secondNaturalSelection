using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField]
    Moveble Hide;
    [SerializeField]
    Vector2 HideHidePosition;

    [SerializeField]
    Moveble pause;
    [SerializeField]
    Vector2 PauseHidePosition;

    [SerializeField]
    Moveble Continue;
    [SerializeField]
    Vector2 ContinuePosition;

    [SerializeField]
    Moveble Restart;
    [SerializeField]
    Vector2 RestartPosition;

    [SerializeField]
    Moveble BackToMenu;
    [SerializeField]
    Vector2 BackToMenuPosition;

    [SerializeField]
    Moveble Exit;
    [SerializeField]
    Vector2 ExitPosition;

    [SerializeField]
    Moveble Logo;
    [SerializeField]
    Vector2 LogoPosition;

    [SerializeField]
    MoveOnMouseEnter moveOn;

    [SerializeField]
    MoveOnMouseEnter moveOnContinue;
    public void Click()
    {
        Time.timeScale = 0;
        Hide.position = HideHidePosition;
        pause.position = PauseHidePosition;
        Continue.position = ContinuePosition;
        Restart.position = RestartPosition;
        BackToMenu.position = BackToMenuPosition;
        Exit.position = ExitPosition;
        Logo.position = LogoPosition;
        moveOn.enabled = false;
        moveOnContinue.enabled = true;
    }
}
