using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour
{
    public bool patrolling;
    public bool searching;
    public float speed = 0.7f;


    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolling)
        {
            searching = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0) * speed;
        }
        if (searching)
        {
            patrolling = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0) * speed * 1.5f;
        }
    }
}
