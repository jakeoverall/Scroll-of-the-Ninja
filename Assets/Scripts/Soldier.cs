using UnityEngine;
using System.Collections;

public class Soldier : MonoBehaviour
{
    public bool patrolling;
    public bool searching;
    public float speed = 0.7f;

    public bool alert = false;
    public float waitTime = 2f;
    public float searchTime = 15f;


    private Detect detect;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        detect = GetComponentInChildren<Detect>();
    }


    void FixedUpdate()
    {
        if (patrolling)
        {
            anim.SetInteger("AnimState", 2);
            searching = false;
            rigidbody2D.velocity = new Vector2(transform.localScale.x, 0) * speed;
        }
        if (searching)
        {
            anim.SetInteger("AnimState", 2);
            patrolling = false;
            rigidbody2D.velocity = new Vector2(transform.localScale.x, 0) * speed * 1.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (detect.detected)
        {
            alert = true;
            patrolling = false;
            searchTime = 15f;
            if (waitTime <= 0)
            {
                Attack();
            }
            waitTime -= Time.deltaTime;
        }

        searching = !detect.detected && searchTime > 0;
        if (alert && !detect.detected)
        {
            searchTime -= Time.deltaTime;
        }
        //return to Gun Idle after searching
        if (alert && searchTime <= 0)
        {
            alert = false;
            searching = false;
            searchTime = 0;
            anim.SetInteger("AnimState", 4);
            patrolling = true;
        }


    }
    public void Attack()
    {
        print("shooting");
        anim.SetInteger("AnimState", 3);
        waitTime = 2;
    }
}
