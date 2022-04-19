using UnityEngine;

public class ToMainMenu : MonoBehaviour
{
    [SerializeField]
    AmebaGenerator amebaGenerator;
    SceneChanger sceneChanger;
    public void Start()
    {
        sceneChanger = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
    }
    public void Click() 
    {
        amebaGenerator.SaveCurrentAmebasGeneration();
        sceneChanger.Changescene("MainMenu");
    }
}
