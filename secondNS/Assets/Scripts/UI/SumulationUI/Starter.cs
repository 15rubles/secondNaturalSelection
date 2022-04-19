using UnityEngine;

public class Starter : MonoBehaviour
{
    GlobalInfo globalInfo;
    public GameObject enviroment;
    public void Awake()
    {
        globalInfo = GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>();
    }
    public void Start()
    {
        enviroment = Resources.Load<GameObject>("Enviroment/" + globalInfo.EnviromentName);
        Instantiate(enviroment, new Vector3(0, 0, 0), new Quaternion());
    }
}
