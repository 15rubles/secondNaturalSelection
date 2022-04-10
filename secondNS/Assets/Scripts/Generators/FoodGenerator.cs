using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    GameObject food;
    readonly int StartCount = 450;
    readonly int MaxCount = 1500;
    readonly float timedelay = 0.015f;
    private float timer = 0;
    List<GameObject> AllFood = new List<GameObject>();
    [SerializeField]
    float leftborder, rightborder, upborder, downborder;
    private void Awake()
    {
        food = (GameObject)Resources.Load("food");
    }
    private void Start()
    {
        for (int i = 0; i < StartCount; i++)
        {
            AllFood.Add(Instantiate(food, new Vector3(Random.Range(leftborder, rightborder), Random.Range(upborder, downborder), 0), new Quaternion()));
        }
    }
    void FixedUpdate()
    {
        for (int i = 0; i < AllFood.Count; i++)
        {
            if (AllFood[i] == null)
            {
                AllFood.Remove(AllFood[i]); i--;
            }
        }
        timer += Time.fixedDeltaTime;
        if (timer >= timedelay && AllFood.Count <= MaxCount)
        {
            AllFood.Add(Instantiate(food, new Vector3(Random.Range(leftborder, rightborder), Random.Range(upborder, downborder), 0), new Quaternion()));
            timer = 0;
        }
    }
}
