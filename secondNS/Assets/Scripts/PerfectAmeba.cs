using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PerfectAmeba : MonoBehaviour
{
    [SerializeField]
    float LifeTime = 0;
    // Own Parametrs
    public PerfectIntellect intellect;
    Rigidbody2D rb2d;
    CapsuleCollider2D mycollider;

    // For sup
    GameObject child;
    PerfectAmeba supameba;
    PerfectIntellect intellectsup;

    private void Awake()
    {
        child = (GameObject)Resources.Load("Ameba");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        mycollider = gameObject.GetComponent<CapsuleCollider2D>();
    }
    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(intellect.genom.AttackSkill, intellect.genom.AbsorbSkill, intellect.genom.DefenceSkill);
    }
    void FixedUpdate()
    {
        LifeTime += Time.fixedDeltaTime;
        // Energy for live
        intellect.Energy -= intellect.genom.EatFoodPerSecond * Time.fixedDeltaTime;
        // If energy is empty
        if (intellect.Energy <= 0)
            Destroy(gameObject);
        if(intellect.Energy >= intellect.genom.EnergyForDublicate)
        {
            GameObject gameObject = Instantiate(child, transform.position, new Quaternion());
            supameba = gameObject.GetComponent<PerfectAmeba>();
            supameba.intellect = new PerfectIntellect(intellect);
            supameba.intellect.Mutate();
            intellect.Energy /= 2;
        }
        List<float> result = intellect.Think(GetInformation());
        rb2d.velocity += new Vector2(result[0] * intellect.genom.Speed * (result[2] + 1) / 2, result[1] * intellect.genom.Speed * (result[2] + 1.2f) / 2);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x) * Mathf.Rad2Deg - 90)), intellect.genom.RotateSpeed * Time.deltaTime);
    }
    List<float> GetInformation()
    {
        List<float> information = new List<float>();
        AddInfoOnDiraction(information, new Vector2(0,1));
        AddInfoOnDiraction(information, new Vector2(-1,1));
        AddInfoOnDiraction(information, new Vector2(-1,0));
        AddInfoOnDiraction(information, new Vector2(-1,-1));
        AddInfoOnDiraction(information, new Vector2(0,-1));
        AddInfoOnDiraction(information, new Vector2(1,-1));
        AddInfoOnDiraction(information, new Vector2(0,0));
        AddInfoOnDiraction(information, new Vector2(1,1));
        return information;
    }
    void AddInfoOnDiraction(List<float> information, Vector2 diraction)
    {
        RaycastHit2D[] hitall = Physics2D.RaycastAll(transform.position, diraction.normalized, intellect.genom.DetectionRadius);
        RaycastHit2D hit;
        if (hitall.Length >= 2)
            hit = hitall.ToList().First(x => x.collider != mycollider);
        else
            hit = new RaycastHit2D();
        if (hit.collider != null)
        {
            Vector2 vector = (hit.point - new Vector2(transform.position.x, transform.position.y)) / intellect.genom.DetectionRadius;
            information.Add(vector.x);
            information.Add(vector.y);
            information.Add(GetGradientFromTag(hit.transform.gameObject));
            //Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(diraction.x, diraction.y, 0)) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit " + hit.transform.gameObject.tag + " " + diraction);
        }
        else
        {
            information.Add(0);
            information.Add(0);
            information.Add(0);
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * intellect.genom.DetectionRadius, Color.white);
            //Debug.Log("Did not Hit");
        }
    }
    float GetGradientFromTag(GameObject gameObject)
    {
        string tag = gameObject.tag;
        if(tag == "Food")
        {
            return 1;
        }
        else if (tag == "Ameba")
        {
            if (WhoIsThisAmeba(gameObject.GetComponent<PerfectAmeba>().intellect.genom) == "Friend")
            {
                return -0.5f;
            }
            else
            {
                return 0.5f;
            }
        }
        else if (tag == "Wall")
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
    string WhoIsThisAmeba(Genom genom)
    {
        if (intellect.genom.SecondName == genom.SecondName)
        {
            return "Friend";
        }
        else
        {
            return "Enemy";
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            intellect.Energy += intellect.genom.AbsorbSkill;
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ameba")
        {
            intellectsup = collision.gameObject.GetComponent<PerfectAmeba>().intellect;
            if (intellectsup.genom.DefenceSkill < intellect.genom.AttackSkill && intellectsup.genom.SecondName != intellect.genom.SecondName)
            {
                intellectsup.Energy -= intellect.genom.AttackSkill * intellect.genom.AttackBiteCoeficient;
                intellect.Energy += intellect.genom.AttackSkill;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ameba")
        {
            intellectsup = collision.gameObject.GetComponent<PerfectAmeba>().intellect;
            if (intellectsup.genom.DefenceSkill < intellect.genom.AttackSkill && intellectsup.genom.SecondName != intellect.genom.SecondName)
            {
                intellectsup.Energy -= intellect.genom.AttackSkill * intellect.genom.AttackSuckCoeficient;
                intellect.Energy += intellect.genom.AttackSkill * intellect.genom.EatSuckCoeficient;
            }
        }
    }
}
