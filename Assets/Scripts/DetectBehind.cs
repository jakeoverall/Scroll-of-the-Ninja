using UnityEngine;
using System.Collections;

public class DetectBehind : MonoBehaviour
{
    public Transform sightStart, sightEnd;
    public bool detected = false;

	// Use this for initialization
	void Start ()
	{
	    detected = false;
	}
	
	// Update is called once per frame
	void Update () {
        detected = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));
	}
}
