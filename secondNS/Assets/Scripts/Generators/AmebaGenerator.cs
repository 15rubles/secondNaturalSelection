using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AmebaGenerator : MonoBehaviour
{
    List<GameObject> amebascount = new List<GameObject>();
    List<AmebaData> AllAmebasInGeneration = new List<AmebaData>();
    
    [SerializeField]
    float leftborder, rightborder, upborder, downborder;
    
    // Counters
    int generation = 1;
    float time = 0;

    // Constants
    int StartCount = 30;
    float generationtime = 420;
    float PartOfNewFromOld = 0.3f;
    int SafeToFileCount = 6;

    // Sup
    PerfectAmeba amebaobj;
    GameObject ameba;

    // File 
    string diractory;
    AmebaWriterInFile AWIF = new AmebaWriterInFile();
    GlobalInfo globalInfo;

    void Awake()
    {
        globalInfo = GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>();
        ameba = (GameObject)Resources.Load("Ameba");
        diractory = "Population_" + globalInfo.NewGenerationNumber;
        globalInfo.NewGenerationNumber++;
        Directory.CreateDirectory(globalInfo.projectPath + "/" + diractory);
    }
    private void Start()
    {
        if (globalInfo.GenerationFolder != "None")
        {
            PerfectIntellect perfectIntellect;
            foreach (string filename in Directory.GetFiles(globalInfo.projectPath + "/" + globalInfo.GenerationFolder + "/Generation_" + globalInfo.ChoosedGenerationNumber))
            {
                perfectIntellect = AWIF.ReadAllPrfectIntellectFromFile(globalInfo.projectPath + "/" + globalInfo.GenerationFolder + "/Generation_" + globalInfo.ChoosedGenerationNumber, filename);
                CreateNewAmeba(perfectIntellect);
            }
            FillFromItself(StartCount - SafeToFileCount);
        }
        else
        {
            for (int i = 0; i < StartCount; i++)
            {
                CreateNewAmeba();
            }
        }
        SaveCurrentAmebasGeneration();
    }
    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        for (int i = 0; i < AllAmebasInGeneration.Count; i++)
        {
            if(AllAmebasInGeneration[i].link != null)
            {
                AllAmebasInGeneration[i].lifetime += Time.fixedDeltaTime;
            }
        }
        ClearListFromNull();
        if (time >= generationtime || amebascount.Count == 0)
        {
            int CountToDelete = AllAmebasInGeneration.Count;
            SaveCurrentAmebasGeneration();
            for (int i = 0; i < StartCount * PartOfNewFromOld; i++)
            {
                CreateRandomAmebaFromList(AllAmebasInGeneration, SafeToFileCount);
            }
            for (int i = (int)(StartCount * PartOfNewFromOld); i < StartCount; i++)
            {
                CreateNewAmeba();
            }
            KillAndDeleteOldAmebas(CountToDelete);
            time = 0;
        }
    }
    public void SaveCurrentAmebasGeneration()
    {
        SortAmebasByLifetimes();
        Directory.CreateDirectory(globalInfo.projectPath + "/" + diractory + "/Generation_" + generation);
        for (int i = 0; i < SafeToFileCount; i++)
        {
            AWIF.WritePrfectIntellectInFile(AllAmebasInGeneration[i].intellect, globalInfo.projectPath + "/" + diractory + "/Generation_" + generation, "Ameba" + i);
        }
        generation++;
    }
    private void FillFromItself(int count)
    {
        int uniccount = amebascount.Count;
        for (int i = 0; i < count; i++)
        {
            CreateNewAmeba(amebascount[Random.Range(0, uniccount)].GetComponent<PerfectAmeba>().intellect);
        }
    }
    private void CreateNewAmeba()
    {
        GameObject gameObject = Instantiate(ameba, new Vector3(Random.Range(leftborder, rightborder), Random.Range(upborder, downborder), 0), new Quaternion());
        amebaobj = gameObject.GetComponent<PerfectAmeba>();
        amebaobj.amebaGenerator = this;
        amebaobj.intellect = new PerfectIntellect(24, 15, 3, 50);
        AddAmebaInList(gameObject);
    }
    private void CreateNewAmeba(PerfectIntellect pi)
    {
        GameObject gameObject = Instantiate(ameba, new Vector3(Random.Range(leftborder, rightborder), Random.Range(upborder, downborder), 0), new Quaternion());
        amebaobj = gameObject.GetComponent<PerfectAmeba>();
        amebaobj.amebaGenerator = this;
        amebaobj.intellect = new PerfectIntellect(pi);
        AddAmebaInList(gameObject);
    }
    private void CreateRandomAmebaFromList(List<AmebaData> amebas, int rightb)
    {
        GameObject gameObject = Instantiate(ameba, new Vector3(Random.Range(leftborder, rightborder), Random.Range(upborder, downborder), 0), new Quaternion());
        amebaobj = gameObject.GetComponent<PerfectAmeba>();
        amebaobj.amebaGenerator = this;
        amebaobj.intellect = new PerfectIntellect(amebas[Random.Range(0, rightb)].intellect);
        amebaobj.intellect.Mutate();
        AddAmebaInList(gameObject);
    }
    private void ClearListFromNull()
    {
        for (int i = 0; i < amebascount.Count; i++)
        {
            if (amebascount[i] == null)
            {
                amebascount.Remove(amebascount[i]); i--;
            }
        }
    }
    private void KillAndDeleteOldAmebas(int countfromstart)
    {
        for (int i = 0; i < countfromstart; i++)
        {
            if(AllAmebasInGeneration[i].link != null) Destroy(AllAmebasInGeneration[i].link);
        }
        AllAmebasInGeneration.RemoveRange(0, countfromstart);
    }
    private void SortAmebasByLifetimes()
    {
        AllAmebasInGeneration.Sort((y, x) => x.lifetime.CompareTo(y.lifetime));
    }
    public void AddAmebaInList(GameObject ameba)
    {
        amebascount.Add(ameba);
        AllAmebasInGeneration.Add(new AmebaData(ameba, new PerfectIntellect(ameba.GetComponent<PerfectAmeba>().intellect), 0));
    }
    private class AmebaData
    {
        public GameObject link;
        public PerfectIntellect intellect;
        public float lifetime;
        public AmebaData(GameObject go, PerfectIntellect pi, float lt)
        {
            link = go;
            intellect = pi;
            lifetime = lt;
        }
    }
}
