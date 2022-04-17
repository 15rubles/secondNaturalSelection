using UnityEngine;
using System.IO;

public class GlobalInfo : MonoBehaviour
{
    [SerializeField]
    public string MainDiractoryPath = "D:/Github repositories/secondNaturalSelection/secondNS/Assets/AmebasIntellectsData";
    [SerializeField]
    public string EnviromentName = "Standart";
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
    }
}
