using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AmebaWriterInFile
{
    public string DataPath = "C:/GitHub/secondNaturalSelection/secondNS";
    public void WritePrfectIntellectInFile(PerfectIntellect intellect, string directorypath, string filename)
    {
        File.WriteAllText(DataPath + "/Assets/AmebasIntellectsData/" + directorypath + "/" + filename + ".json", JsonUtility.ToJson(intellect));
    }
    public PerfectIntellect ReadAllPrfectIntellectFromFile(string directorypath, string filename)
    {
        return JsonUtility.FromJson<PerfectIntellect>(File.ReadAllText(DataPath + "/Assets/AmebasIntellectsData/" + directorypath + "/" + filename + ".json"));
    }
}
