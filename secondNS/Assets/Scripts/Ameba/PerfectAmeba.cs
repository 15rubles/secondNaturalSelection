using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PerfectAmeba : MonoBehaviour
{
    // Own Parametrs
    public PerfectIntellect intellect;
    Rigidbody2D rb2d;
    CapsuleCollider2D mycollider;

    // For sup
    GameObject child;
    PerfectAmeba supameba;
    PerfectIntellect intellectsup;
    public AmebaGenerator amebaGenerator;

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
        CountLifeTime();
        SpendEnergyForLife();
        CheckIfEnergyEnoughForLife();
        CheckIfEnergyEnoughForDublicate();
        MoveBody(intellect.Think(GetInformation()));
    }
    #region Private Methods
    private void CountLifeTime()
    {
        intellect.LifeTime += Time.fixedDeltaTime;
    }
    private void SpendEnergyForLife()
    {
        intellect.Energy -= intellect.genom.EatFoodPerSecond * Time.fixedDeltaTime;
    }
    private void CheckIfEnergyEnoughForLife()
    {
        if (intellect.Energy <= 0)
            Destroy(gameObject);
    }
    private void CheckIfEnergyEnoughForDublicate()
    {
        if (intellect.Energy >= intellect.genom.EnergyForDublicate)
        {
            GameObject gameObject = Instantiate(child, transform.position, new Quaternion());
            supameba = gameObject.GetComponent<PerfectAmeba>();
            supameba.intellect = new PerfectIntellect(intellect);
            supameba.intellect.Mutate();
            supameba.amebaGenerator = amebaGenerator;
            amebaGenerator.AddAmebaInList(gameObject);
            intellect.Energy /= 2;
        }
    }
    private void MoveBody(List<float> Desision)
    {
        //Debug.Log("Result: " + Desision[0] + " " + Desision[1] + " " + Desision[2]);
        rb2d.velocity += new Vector2(Desision[0] * intellect.genom.Speed * (Desision[2] + 1) / 2, Desision[1] * intellect.genom.Speed * (Desision[2] + 1.1f) / 2);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x) * Mathf.Rad2Deg - 90)), intellect.genom.RotateSpeed * Time.deltaTime);
    }
    #endregion
    #region Get Information To Think
    List<float> GetInformation()
    {
        List<float> information = new List<float>();
        AddInfoOnDiraction(information, new Vector2(0,1));
        AddInfoOnDiraction(information, new Vector2(-1,1));
        AddInfoOnDiraction(information, new Vector2(-1,0));
        AddInfoOnDiraction(information, new Vector2(-1,-1));
        AddInfoOnDiraction(information, new Vector2(0,-1));
        AddInfoOnDiraction(information, new Vector2(1,-1));
        AddInfoOnDiraction(information, new Vector2(0.0001f,0));
        AddInfoOnDiraction(information, new Vector2(1,1));
        //Debug.Log("Info: " + information[0] + " " + information[1] + " " + information[2] + " " + information[3] + " " + information[4] + " " + information[5] + " " + information[6]);
        return information;
    }
    void AddInfoOnDiraction(List<float> information, Vector2 diraction)
    {
        RaycastHit2D[] hitall = Physics2D.RaycastAll(transform.position, transform.TransformDirection(diraction.normalized), intellect.genom.DetectionRadius);
        //Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(new Vector3(diraction.normalized.x * intellect.genom.DetectionRadius, diraction.normalized.y * intellect.genom.DetectionRadius, 0)), Color.red);
        RaycastHit2D hit;
        if (hitall.Length >= 2)
            hit = hitall.ToList().First(x => x.collider != mycollider);
        else
            hit = new RaycastHit2D();
        if (hit.collider != null)
        {
            Vector2 vector = (hit.point - new Vector2(transform.position.x, transform.position.y)) / intellect.genom.DetectionRadius;
            //Debug.Log("Vector: " + vector + " Dir: " + diraction + " Tag: " + hit.transform.gameObject.tag + " " + GetGradientFromTag(hit.transform.gameObject));
            information.Add(vector.x);
            information.Add(vector.y);
            information.Add(GetGradientFromTag(hit.transform.gameObject));
        }
        else
        {
            information.Add(0);
            information.Add(0);
            information.Add(0);
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
        if (intellect.genom.SecondName == genom.SecondName || intellect.genom.AttackSkill < genom.DefenceSkill && intellect.genom.DefenceSkill > genom.AttackSkill)
        {
            return "Friend";
        }
        else
        {
            return "Enemy";
        }
    }
    #endregion
    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            intellect.Energy += intellect.genom.AbsorbSkill;
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ameba"))
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
        if (collision.gameObject.CompareTag("Ameba"))
        {
            intellectsup = collision.gameObject.GetComponent<PerfectAmeba>().intellect;
            if (intellectsup.genom.DefenceSkill < intellect.genom.AttackSkill && intellectsup.genom.SecondName != intellect.genom.SecondName)
            {
                intellectsup.Energy -= intellect.genom.AttackSkill * intellect.genom.AttackSuckCoeficient;
                intellect.Energy += intellect.genom.AttackSkill * intellect.genom.EatSuckCoeficient;
            }
        }
    }
    #endregion
}
