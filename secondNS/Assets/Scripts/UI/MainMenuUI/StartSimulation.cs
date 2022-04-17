using UnityEngine;

public class StartSimulation : MonoBehaviour
{
    public void StartSimulate()
    {
        GameObject.Find("SceneChanger").GetComponent<SceneChanger>().Changescene("Simulation");
    }
}
