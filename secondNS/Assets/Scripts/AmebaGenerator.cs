using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmebaGenerator : MonoBehaviour
{
    GameObject ameba;
    int StartCount = 40;
    [SerializeField]
    float leftborder, rightborder, upborder, downborder;
    PerfectAmeba amebaobj;
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
            amebaobj.intellect = new PerfectIntellect(37, 24, 3, 50);
        }
    }
}
