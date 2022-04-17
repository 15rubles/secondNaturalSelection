using UnityEngine;
using System.IO;

public class GlobalInfo : MonoBehaviour
{
    public string projectPath;
    public string EnviromentName = "Default";
    public string GenerationFolder = "None";
    public int ChoosedGenerationNumber = 0;
    public int NewGenerationNumber= 1;
    private void Awake()
    {
        projectPath = Application.dataPath + "/Resources/Data";
        DontDestroyOnLoad(gameObject);
        GameObject empty = Resources.Load<GameObject>("Empty");
        GameObject map = Resources.Load<GameObject>("Enviroment/" + EnviromentName);
        GameObject env = Instantiate(empty);
        env.name = "Enviroment";
        Instantiate(map, env.transform);
    }
}
