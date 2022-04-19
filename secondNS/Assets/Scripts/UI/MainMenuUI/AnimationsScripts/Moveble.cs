using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveble : MonoBehaviour
{
    public float Speed;
    public Vector2 position;
    RectTransform RT;
    private void Awake()
    {
        RT = gameObject.GetComponent<RectTransform>();
    }
    void Update()
    {
        RT.anchoredPosition = Vector3.Lerp(RT.anchoredPosition, new Vector3(position.x, position.y, RT.position.z), Speed * Time.unscaledDeltaTime);
    }
    
}
