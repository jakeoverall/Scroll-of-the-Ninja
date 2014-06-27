using UnityEngine;
using System.Collections;

public class Detect : MonoBehaviour
{
    public Transform sightStart, sightEnd;
    public bool detected = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        detected = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));
	}
}
