using UnityEngine;
using UnityEngine.UI;

public class ToggleOffOn : MonoBehaviour
{
    Toggle toggle;
    Text text;
    Color NotChoosed = Color.black;
    Color Choosed = Color.white;
    public void Awake()
    {
        toggle = GetComponent<Toggle>();
        text = GetComponentInChildren<Text>();
    }
    public void OnChange()
    {
        if (toggle.isOn)
        {
            text.color = Choosed;
        }
        else
        {
            text.color = NotChoosed;
        }
    }
}
