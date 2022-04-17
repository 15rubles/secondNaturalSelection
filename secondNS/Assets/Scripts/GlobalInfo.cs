using UnityEngine;
using System.IO;

public class GlobalInfo : MonoBehaviour
{
    [SerializeField]
    public string MainDiractoryPath = "D:/Github repositories/secondNaturalSelection/secondNS/Assets/AmebasIntellectsData";
    [SerializeField]
    public string EnviromentName = "Default";
    [SerializeField]
    public string GenerationFolder = "None";
    [SerializeField]
    public string GenerationName = "Free";

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        if (!Directory.Exists(MainDiractoryPath + "/UserSaves"))
        {
            Directory.CreateDirectory(MainDiractoryPath + "/UserSaves");
        }

        GameObject empty = Resources.Load<GameObject>("Empty");
        GameObject map = Resources.Load<GameObject>("Enviroment/" + EnviromentName);
        GameObject env = Instantiate(empty);
        env.name = "Enviroment";
        Instantiate(map, env.transform);
    }
}
