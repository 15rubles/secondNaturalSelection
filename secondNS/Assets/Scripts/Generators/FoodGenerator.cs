using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    [SerializeField]
    readonly int StartCount = 450;
    [SerializeField]
    readonly int MaxCount = 1500;
    [SerializeField]
    readonly float timedelay = 0.015f;
    [SerializeField]
    private float timer = 0;

    GameObject food;
    Starter starter;
    List<GameObject> AllFood = new List<GameObject>();
    float leftborder, rightborder, upborder, downborder;
    Vector3 point;
    private void Awake()
    {
        food = (GameObject)Resources.Load("food");
        starter = GameObject.Find("Starter").GetComponent<Starter>();
    }
    private void Start()
    {
        MapName mn = starter.enviroment.GetComponent<MapName>();
        leftborder = -mn.Size.x/2;
        rightborder = mn.Size.x/2;
        upborder = mn.Size.y / 2;
        downborder = -mn.Size.y / 2;

        for (int i = 0; i < StartCount; i++)
        {
            while (Physics2D.OverlapPoint(point))
            {
                point = new Vector3(Random.Range(leftborder, rightborder), Random.Range(upborder, downborder), 0);
            }
            AllFood.Add(Instantiate(food, point, new Quaternion()));
        }
    }
    void FixedUpdate()
    {
        ClearListFromNull();
        timer += Time.fixedDeltaTime;
        if (timer >= timedelay && AllFood.Count <= MaxCount)
        {
            while (Physics2D.OverlapPoint(point))
            {
                point = new Vector3(Random.Range(leftborder, rightborder), Random.Range(upborder, downborder), 0);
            }
            AllFood.Add(Instantiate(food, point, new Quaternion()));
            timer = 0;
        }
    }
    private void ClearListFromNull()
    {
        for (int i = 0; i < AllFood.Count; i++)
        {
            if (AllFood[i] == null)
            {
                AllFood.Remove(AllFood[i]); i--;
            }
        }
    }
}
