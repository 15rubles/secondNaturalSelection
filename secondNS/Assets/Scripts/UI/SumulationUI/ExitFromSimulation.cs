using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitFromSimulation : MonoBehaviour
{
    [SerializeField]
    AmebaGenerator amebaGenerator;
    public void Click()
    {
        amebaGenerator.SaveCurrentAmebasGeneration();
        Application.Quit();
    }
}
