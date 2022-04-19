using UnityEngine;

public class ArrowPlus : MonoBehaviour
{
    [SerializeField]
    TimeScale timeScale;
    [SerializeField]
    float change = 0.5f;
    public void Click()
    {
        timeScale.timescale += change;
        timeScale.UpdateTimescale();
    }
}
