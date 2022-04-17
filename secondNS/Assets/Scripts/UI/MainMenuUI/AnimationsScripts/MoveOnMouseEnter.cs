using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveOnMouseEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Vector2 startpoint;
    [SerializeField]
    Vector2 moveto;
    Moveble moveble;
    private void Awake()
    {
        moveble = GetComponent<Moveble>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        moveble.position = moveto;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        moveble.position = startpoint;
    }
}
