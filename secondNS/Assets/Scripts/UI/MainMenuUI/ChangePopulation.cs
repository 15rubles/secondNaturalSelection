using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangePopulation : MonoBehaviour
{
    GlobalInfo globalinfo;
    [SerializeField]
    Text CurrentPopulationText;
    public void Awake()
    {
        globalinfo = GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>();
    }
    public void ChangePopulationn()
    {
        SceneManager.LoadScene("ChangePopulation");
    }
    public void Update()
    {
        CurrentPopulationText.text = "Current Population: " + globalinfo.GenerationName;
    }
}
