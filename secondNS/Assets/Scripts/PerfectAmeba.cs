using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectAmeba : MonoBehaviour
{
    [SerializeField]
    float LifeTime = 0;
    // Own Parametrs
    public PerfectIntellect intellect;
    Rigidbody2D rb2d;

    // For sup
    GameObject child;

    private void Awake()
    {
        child = (GameObject)Resources.Load("Ameba");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
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
        
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x) * Mathf.Rad2Deg - 90)), intellect.genom.RotateSpeed * Time.deltaTime);
    }
}
