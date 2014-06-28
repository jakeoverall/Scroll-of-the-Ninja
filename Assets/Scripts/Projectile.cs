using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

    public float life = 3f;

    // Use this for initialization
    void Awake()
    {
        life = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0)
        { Destroy(gameObject); }
    }

    void OnCollisionEnter2D(Collision2D x)
    {
        Destroy(gameObject);
    }
}
