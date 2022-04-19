using UnityEngine;
using System.IO;

public class GlobalInfo : MonoBehaviour
{
    public string projectPath;
    public string EnviromentName = "Default";
    public string GenerationFolder = "None";
    public int ChoosedGenerationNumber = 1;
    public int NewGenerationNumber= 1;
    private void Awake()
    {
        if (File.Exists(Application.dataPath + "/Resources/Globalinfo.json"))
        {
            GlobalInfo info = JsonUtility.FromJson<GlobalInfo>(File.ReadAllText(Application.dataPath + "/Resources/Globalinfo.json"));
            EnviromentName = info.EnviromentName;
            GenerationFolder = info.GenerationFolder;
            ChoosedGenerationNumber = info.ChoosedGenerationNumber;
            NewGenerationNumber = info.NewGenerationNumber;
        }
        projectPath = Application.dataPath + "/Resources/Data";
        DontDestroyOnLoad(gameObject);
        GameObject empty = Resources.Load<GameObject>("Empty");
        GameObject map = Resources.Load<GameObject>("Enviroment/" + EnviromentName);
        GameObject env = Instantiate(empty);
        env.name = "Enviroment";
        Instantiate(map, env.transform);
    }

    void OnApplicationQuit()
    {
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(Application.dataPath + "/Resources/Globalinfo.json", json);
    }
}
