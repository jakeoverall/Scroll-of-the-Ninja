using System;
using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    public Door door;
    public bool ignoreTrigger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D target)
    {
        if(ignoreTrigger)
            return;

        if (target.gameObject.tag == "Player")
        {
            door.Open();
        }
    }
    void OnTriggerExit2D(Collider2D target)
    {
        if (ignoreTrigger)
            return;

        if (target.gameObject.tag == "Player")
        {
            door.Close();
        }
    }

    public void Toggle(bool p)
    {
        if(p)
            door.Open();
        else
            door.Close();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = ignoreTrigger ? Color.gray : Color.green;

        var doorTriggerCollider = GetComponent<BoxCollider2D>();
        var doorTriggerPosition = doorTriggerCollider.transform.position;
        var newPosition = new Vector2(doorTriggerPosition.x + doorTriggerCollider.offset.x,
            doorTriggerPosition.y + doorTriggerCollider.offset.y);
        Gizmos.DrawWireCube(newPosition, new Vector2(doorTriggerCollider.size.x, doorTriggerCollider.size.y));
    }
}
