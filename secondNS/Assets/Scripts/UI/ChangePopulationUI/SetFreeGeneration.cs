using UnityEngine;
using UnityEngine.SceneManagement;

public class SetFreeGeneration : MonoBehaviour
{
    public void SetFree()
    {
        GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>().GenerationName = "Free";
        SceneManager.LoadScene("MainMenu");
    }
}
