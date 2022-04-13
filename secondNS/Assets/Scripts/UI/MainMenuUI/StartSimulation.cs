using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSimulation : MonoBehaviour
{
    public void StartSimulate()
    {
        SceneManager.LoadScene("Simulation");
    }
}
