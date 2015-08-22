using UnityEngine;
using System.Collections;

public class LookBehind : MonoBehaviour
{
	
	public Transform sightStart, sightEnd;
	private bool collision = false;
	public bool turnAround = true;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		collision = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player")) || Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Enemy"));
		Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
		
		if (collision)
		{
			print("TURN AROUND!");
			this.transform.localScale = new Vector3((transform.localScale.x == -1) ? 1 : -1, 1, 1);
		}
		turnAround = transform.localScale.x == 1;

	}
}

