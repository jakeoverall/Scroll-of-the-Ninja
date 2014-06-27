using UnityEngine;
using System.Collections;

public class Soldier : MonoBehaviour
{

    private Detect sight;
    // Use this for initialization
	void Start ()
	{
	    sight = GetComponent<Detect>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
