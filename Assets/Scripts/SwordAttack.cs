using UnityEngine;
using System.Collections;

public class SwordAttack : MonoBehaviour
{
	public float meleeCooldown = .3f; 
	// Use this for initialization
	void Start ()
	{
	}
	void Update(){
		if (Input.GetButtonUp ("Fire1")) {
			attack ();
		} else {
			if(meleeCooldown < 0){
				meleeCooldown = 0;
			} else{
				meleeCooldown -= Time.deltaTime;
			}
		}
	}
	void OnTriggerEnter2D(Collider2D target)
	{

	}
	
	void attack(){
		if (meleeCooldown <= 0) {
			print("Player ATTACK");
			meleeCooldown = .3f;				
		}
	}
}