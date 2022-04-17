using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangePopulation : MonoBehaviour
{
    GlobalInfo globalinfo;
    [SerializeField]
    Animator anim;
    public void Awake()
    {
        globalinfo = GameObject.Find("GlobalInfo").GetComponent<GlobalInfo>();
        anim = gameObject.GetComponent<Animator>();
    }
    public void ChangePopulationn()
    {
        anim.SetBool("Menu", true);
        //SceneManager.LoadScene("ChangePopulation");
    }
}
