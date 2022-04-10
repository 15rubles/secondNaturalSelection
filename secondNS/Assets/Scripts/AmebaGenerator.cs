using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmebaGenerator : MonoBehaviour
{
    GameObject ameba;
    int StartCount = 20;
    [SerializeField]
    float leftborder, rightborder, upborder, downborder;
    PerfectAmeba amebaobj;
    List<GameObject> amebascount = new List<GameObject>();
    void Awake()
    {
        ameba = (GameObject)Resources.Load("Ameba");
    }
    private void Start()
    {
        for (int i = 0; i < StartCount; i++)
        {
            GameObject gameObject = Instantiate(ameba, new Vector3(Random.Range(leftborder, rightborder), Random.Range(upborder, downborder), 0), new Quaternion());
            amebaobj = gameObject.GetComponent<PerfectAmeba>();
            amebaobj.intellect = new PerfectIntellect(37, 24, 3, 30);
            amebascount.Add(gameObject);
        }
    }
    private void FixedUpdate()
    {
        //int size = amebascount.Count;
        for (int i = 0; i < amebascount.Count; i++)
        {
            if (amebascount[i] == null)
            {
                amebascount.Remove(amebascount[i]); i--; //size--;
            }
        }
        if(amebascount.Count <= 4)
        {
            for (int i = 0; i < StartCount; i++)
            {
                GameObject gameObject = Instantiate(ameba, new Vector3(Random.Range(leftborder, rightborder), Random.Range(upborder, downborder), 0), new Quaternion());
                amebaobj = gameObject.GetComponent<PerfectAmeba>();
                amebaobj.intellect = new PerfectIntellect(37, 24, 3, 30);
                amebascount.Add(gameObject);
            }
        }
    }
}
