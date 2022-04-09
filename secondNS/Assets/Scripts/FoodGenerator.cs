using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    GameObject food;
    readonly int StartCount = 160;
    readonly float timedelay = 0.03f;
    private float timer = 0;
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
            Instantiate(food, new Vector3(Random.Range(leftborder, rightborder), Random.Range(upborder, downborder), 0), new Quaternion());
        }
    }
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= timedelay)
        {
            Instantiate(food, new Vector3(Random.Range(leftborder, rightborder), Random.Range(upborder, downborder), 0), new Quaternion());
            timer = 0;
        }
    }
}
