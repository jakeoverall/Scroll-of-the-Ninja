using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
	private bool inRange;	
	private Animator animator;
	public Soldier enemy;
	public float meleeCooldown = .9f; 
	// Use this for initialization
	void Start ()
	{
		animator = GetComponentInParent<Animator> ();
	}
	void Update(){
		if (inRange && meleeCooldown > 0) {
			meleeCooldown -= Time.deltaTime;
		} else {
			attack();					
		}
	}
	void OnTriggerEnter2D(Collider2D target)
	{
		inRange = target.CompareTag("Player");
	}

	void OnTriggerExit2D(Collider2D target)
	{
		inRange = target.CompareTag("Player");
	}

	void attack(){
		if (inRange) {
			print("MELEE ATTACK");
			meleeCooldown = 1.2f;				
			enemy.Attack();
		}
	}
}