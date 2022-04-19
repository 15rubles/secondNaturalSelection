using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCenter : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
