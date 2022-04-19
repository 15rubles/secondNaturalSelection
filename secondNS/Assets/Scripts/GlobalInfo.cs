using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            //filename = "Ameba" + filename.Split('a').ToList().Last();
            //List<string> data = File.ReadAllText(FullDirectoryPath + "/" + filename).Split(' ').ToList();
            //PerfectIntellect perfectIntellect = JsonUtility.FromJson<PerfectIntellect>(data[0]);

            string a = Application.dataPath + "/Resources/Globalinfo.json";
            List<string> js = File.ReadAllText(a).Split(' ').ToList();
            ConvertData info = JsonUtility.FromJson<ConvertData>(js[0]);
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
        ConvertData cd = new ConvertData();
        cd.EnviromentName = EnviromentName;
        cd.GenerationFolder = GenerationFolder;
        cd.ChoosedGenerationNumber = ChoosedGenerationNumber;
        cd.NewGenerationNumber = NewGenerationNumber;
        string json = JsonUtility.ToJson(cd);
        File.WriteAllText(Application.dataPath + "/Resources/Globalinfo.json", json);
    }
}

public class ConvertData
{
    public string EnviromentName = "Default";
    public string GenerationFolder = "None";
    public int ChoosedGenerationNumber = 1;
    public int NewGenerationNumber = 1;
}
