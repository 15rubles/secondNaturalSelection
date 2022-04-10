using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AmebaGenerator : MonoBehaviour
{
    GameObject ameba;
    int StartCount = 21;
    [SerializeField]
    float leftborder, rightborder, upborder, downborder;
    PerfectAmeba amebaobj;
    List<GameObject> amebascount = new List<GameObject>();
    string diractory;
    int generation = 0;
    AmebaWriterInFile AWIF = new AmebaWriterInFile();
    public string MainDiractoryPath = "C:/GitHub/secondNaturalSelection/secondNS/Assets/AmebasIntellectsData";
    void Awake()
    {
        ameba = (GameObject)Resources.Load("Ameba");
        diractory = "ProgramWork_" + System.DateTime.Now.Year + "_" + System.DateTime.Now.Month + "_" + System.DateTime.Now.Day + "_" + System.DateTime.Now.Hour + "_" + System.DateTime.Now.Minute + "_" + System.DateTime.Now.Second;
        Directory.CreateDirectory(MainDiractoryPath + "/" + diractory);
    }
    private void Start()
    {
        for (int i = 0; i < StartCount; i++)
        {
            GameObject gameObject = Instantiate(ameba, new Vector3(Random.Range(leftborder, rightborder), Random.Range(upborder, downborder), 0), new Quaternion());
            amebaobj = gameObject.GetComponent<PerfectAmeba>();
            amebaobj.intellect = new PerfectIntellect(24, 15, 3, 50);
            amebaobj.amebaGenerator = this;
            amebascount.Add(gameObject);
        }
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < amebascount.Count; i++)
        {
            if (amebascount[i] == null)
            {
                amebascount.Remove(amebascount[i]); i--;
            }
        }
        if(amebascount.Count <= 3)
        {
            Directory.CreateDirectory(MainDiractoryPath + "/" + diractory + "/Generation_" + generation);
            for (int i = 0; i < amebascount.Count; i++)
            {
                AWIF.WritePrfectIntellectInFile(amebascount[i].GetComponent<PerfectAmeba>().intellect, MainDiractoryPath + "/" + diractory + "/Generation_" + generation, "Ameba" + i);
            }
            generation++;
            for (int i = 0; i < StartCount; i++)
            {
                GameObject gameObject = Instantiate(ameba, new Vector3(Random.Range(leftborder, rightborder), Random.Range(upborder, downborder), 0), new Quaternion());
                amebaobj = gameObject.GetComponent<PerfectAmeba>();
                amebaobj.amebaGenerator = this;
                amebaobj.intellect = new PerfectIntellect(amebascount[Random.Range(0, amebascount.Count - 1)].GetComponent<PerfectAmeba>().intellect);
                amebaobj.intellect.Mutate();
                amebascount.Add(gameObject);
            }
        }
    }
    public void AddAmebaInList(GameObject ameba)
    {
        amebascount.Add(ameba);
    }
}
