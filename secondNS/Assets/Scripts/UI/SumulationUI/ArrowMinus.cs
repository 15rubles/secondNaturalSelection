using UnityEngine;

public class ArrowMinus : MonoBehaviour
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    float change = -0.5f;
    [SerializeField]
    Pause pause;
    public void Click()
    {
        if(timeScale.timescale >= 0.5f)
        {
            timeScale.timescale += change;
            timeScale.UpdateTimescale(!pause.isclicked);
        }
    }
}
