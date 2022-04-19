using System.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AmebaWriterInFile
{
    public void WritePrfectIntellectInFile(PerfectIntellect intellect, string FullDirectoryPath, string filename)
    {
        string json = "";
        json += JsonUtility.ToJson(intellect) + " ";
        foreach (Neuron neuron in intellect.neurons)
        {
            json += JsonUtility.ToJson(neuron) + " ";
        }
        foreach (Gen gen in intellect.gens)
        {
            json += JsonUtility.ToJson(gen) + " ";
        }
        json += JsonUtility.ToJson(intellect.genom);
        File.WriteAllText(FullDirectoryPath + "/" + filename + ".json", json);
    }
    public PerfectIntellect ReadAllPrfectIntellectFromFile(string FullDirectoryPath, string filename)
    {
        filename = "Ameba" + filename.Split('a').ToList().Last();
        List<string> data = File.ReadAllText(FullDirectoryPath + "/" + filename).Split(' ').ToList();
        PerfectIntellect perfectIntellect = JsonUtility.FromJson<PerfectIntellect>(data[0]);
        perfectIntellect.neurons = new List<Neuron>();
        perfectIntellect.gens = new List<Gen>();
        for (int i = 1; i < perfectIntellect.AllNeuronsCount; i++)
        {
            perfectIntellect.neurons.Add(JsonUtility.FromJson<Neuron>(data[i]));
        }
        for (int i = 1 + perfectIntellect.AllNeuronsCount; i < perfectIntellect.AllGensCount; i++)
        {
            perfectIntellect.gens.Add(JsonUtility.FromJson<Gen>(data[i]));
        }
        perfectIntellect.genom = JsonUtility.FromJson<Genom>(data[1 + perfectIntellect.AllNeuronsCount + perfectIntellect.AllGensCount]);
        perfectIntellect.ReloadAfterBirth();
        return perfectIntellect;
    }
}
