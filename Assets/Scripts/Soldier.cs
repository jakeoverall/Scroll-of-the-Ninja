using UnityEngine;
using System.Collections;

public class Soldier : MonoBehaviour
{

    public Projectile projectile;

    public bool patrolling;
    public bool searching;
    public float speed = 0.7f;
    public float patrolTimer = 15f;
    public float idleTimer = 4f;

    public bool alert = false;
    public float attackWaitTime = 1f;
    public float searchTime = 15f;
    public bool facingRight;

    private LookForward looking;

    public enum State
    {
        Idle = 0,
        StartPatrolling = 1,
        Patrolling = 2,
        Alerted = 3,
        Attacking = 4,
        Searching = 5
    }

    private State currentState;
    private Detect detect;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        detect = GetComponentInChildren<Detect>();
        looking = GetComponent<LookForward>();
        facingRight = looking.lookingRight;
    }


    void FixedUpdate()
    {
        if (patrolling)
        {
            anim.SetInteger("AnimState", 2);
            rigidbody2D.velocity = new Vector2(transform.localScale.x, 0) * speed;
            patrolTimer -= Time.deltaTime;
        }
        if (searching)
        {
            anim.SetInteger("AnimState", 2);
            rigidbody2D.velocity = new Vector2(transform.localScale.x, 0) * speed * 1.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        facingRight = looking.lookingRight;

        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.StartPatrolling:
                StopSearching();
                break;
            case State.Alerted:
                Alerted();
                break;
            case State.Patrolling:
                Patrolling();
                break;
            case State.Attacking:
                Attack();
                break;
            case State.Searching:
                Searching();
                break;
            default:
                Idle();
                break;
        }

        if (detect.detected)
        {
            currentState = State.Alerted;
        }

        if (!detect.detected && searchTime > 0)
        {
            currentState = State.Searching;
        }
        if (searchTime <= 0)
        {
            currentState = State.StartPatrolling;
        }
        if (patrolTimer < 0)
        {
            currentState = State.Idle;
        }
        if (idleTimer <= 0)
        {
            currentState = State.Patrolling;
            RestartPatrol();
        }
    }

    public void Alerted()
    {
        alert = true;
        anim.SetBool("Alerted", true);
        anim.SetBool("Detected", true);
        StopPatrol();
        searchTime = 15f;
        if (attackWaitTime <= 0)
        {
            anim.SetBool("Attacking", true);
            Attack();
        }
        else
        {
            anim.SetBool("Attacking", false);
        }
        attackWaitTime -= Time.deltaTime;
    }

    public void Attack()
    {
        print("shooting");
        currentState = State.Attacking;
        Fire();
        attackWaitTime = 0.5f;
    }

    void Fire()
    {
        if (projectile)
        {
            var clone = Instantiate(projectile, transform.position, Quaternion.identity) as Projectile;
            clone.rigidbody2D.AddForce(facingRight ? new Vector2(1000, 0) : new Vector2(-1000, 0));
        }
    }

    public void StartPatrol()
    {
        anim.SetInteger("AnimState", 1);
        Patrolling();
    }

    public void Patrolling()
    {
        idleTimer = 8;
        patrolling = true;
        searching = false;
    }

    public void Searching()
    {
        anim.SetInteger("AnimState", 2);
        anim.SetBool("Attacking", false);
        anim.SetBool("Detected", false);
        searchTime -= Time.deltaTime;
        searching = true;
        patrolling = false;
    }

    public void StopSearching()
    {
        alert = false;
        searching = false;
        searchTime = 0;
        patrolling = true;
    }

    public void StopPatrol()
    {
        patrolling = false;
        searching = false;
        currentState = State.Idle;
        anim.SetBool("Alerted", true);
    }

    public void Idle()
    {
        patrolling = false;
        searching = false;
        anim.SetInteger("AnimState", 8);
        anim.SetBool("Alerted", false);
        idleTimer -= Time.deltaTime;
    }

    public void RestartPatrol()
    {
        patrolTimer = 15f;
    }
}
