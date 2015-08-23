using UnityEngine;
using System.Collections;

public class Detect : MonoBehaviour
{
    public Transform sightStart, sightEnd;
    public bool detected = false;
	public IList targets;  
	public Vector3 lastKnownPosition; 
	// Use this for initialization

	void Start ()
	{
	    detected = false;
	}
	
	// Update is called once per frame
	void Update () {
		detected = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));
		targets = Physics2D.LinecastAll (sightStart.position, sightEnd.position);
		foreach (RaycastHit2D t in targets) {
			if(t.collider.tag == "Ground" || t.collider.tag == "Wall"){
				detected = false;
				break;
			}
			if(t.collider.tag == "Player"){
				detected = true;
				lastKnownPosition = new Vector3 (t.transform.position.x, t.transform.position.y);
				break;
			}
		}
	}
}
