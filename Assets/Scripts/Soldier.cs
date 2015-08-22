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
	public float attackWaitTime = 1.5f;
	public float searchTime = 0f;
	public bool facingRight;
	public bool detected = false;
	public bool detectedTurnAround = false; 
	public bool idle = true;
	
	private LookForward looking;
	
	private Detect detect;
	private DetectBehind detectBehind;
	private Animator anim;
	
	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		detect = GetComponentInChildren<Detect>();
		detected = detect.detected;
		detectBehind = GetComponentInChildren<DetectBehind>();
		detectedTurnAround = detectBehind.detected;
		looking = GetComponent<LookForward>();
		facingRight = looking.lookingRight;
	}
	
	
	void FixedUpdate()
	{
		if (patrolling)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0) * speed;
			patrolTimer -= Time.deltaTime;
		}
		if (searching)
		{
			anim.SetInteger("AnimState", 2);
			GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0) * speed * 1.5f;
		}
	}
	
	// Update is called once per frame
	void Update()
	{

		facingRight = looking.lookingRight;
		detected = detect.detected;
		detectedTurnAround = detectBehind.detected;

		if (detectedTurnAround) {
			Alerted();
		}

		if (detected || detectedTurnAround)
		{
			Alerted();
			return;
		}
		
		if (!detected && searchTime > 0)
		{
			Searching();
			return;
		}
		if (searching && searchTime <= 0)
		{
			StartPatrol();
			return;
		}
		
		if (patrolling && patrolTimer > 0)
		{
			Patrolling();
		}
		else
		{
			Idle();
		}
		if(idle && idleTimer <= 0)
		{
			RestartPatrol();
		}
	}
	
	public void Alerted()
	{
		if(idle)
			anim.SetInteger("AnimState", 10);
		
		alert = true;
		idle = false;
		patrolling = false;
		searching = false;
		anim.SetBool("Alerted", true);
		anim.SetBool("Detected", true);
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
		Fire();
		attackWaitTime = 0.5f;
	}
	
	void Fire()
	{
		if (projectile)
		{
			var clone = Instantiate(projectile, transform.position, Quaternion.identity) as Projectile;
			clone.GetComponent<Rigidbody2D>().AddForce(facingRight ? new Vector2(1000, 0) : new Vector2(-1000, 0));
		}
	}
	
	public void StartPatrol()
	{
		idle = false;
		alert = false;
		searching = false;
		anim.SetBool("Alerted", false);
		anim.SetInteger("AnimState", 1);
		attackWaitTime = 1.5f;
		Patrolling();
	}
	
	public void Patrolling()
	{
		patrolling = true;
		anim.SetInteger("AnimState", 2);
		patrolTimer -= Time.deltaTime;
	}
	
	public void Searching()
	{
		anim.SetInteger("AnimState", 2);
		anim.SetBool("Attacking", false);
		anim.SetBool("Detected", false);
		searchTime -= Time.deltaTime;
		searching = true;
	}
	
	public void Idle()
	{
		idle = true;
		patrolling = false;
		anim.SetInteger("AnimState", 8);
		idleTimer -= Time.deltaTime;
	}
	
	public void RestartPatrol()
	{
		idle = false;
		idleTimer = 8f;
		patrolTimer = 15f;
		patrolling = true;
	}
}