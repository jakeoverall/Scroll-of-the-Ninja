using UnityEngine;
using System.Collections;

public class LaserState : MonoBehaviour
{
    public bool laserOn = true;
    private Animator animator;

    public const int IDLE = 0;
    public const int TURNINGON = 1;
    public const int ON = 2;
    public const int TURNINGOFF = 3;
    public float closeDelay = 0.5f;

    private int state = IDLE;

    // Use this for initialization
	void Start ()
	{
	    animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void TurnOnStart()
    {
        state = TURNINGON;
    }

    void TurnOnEnd()
    {
        state = ON;
    }

    void TurnOffStart()
    {
        state = TURNINGOFF;
    }

    void TurnOffEnd()
    {
        state = IDLE;
    }

    void DissableCollider2D()
    {
        collider2D.enabled = false;
    }
    void EnableCollider2D()
    {
        collider2D.enabled = true;
    }

    public void On()
    {
        animator.SetInteger("AnimState", 0);
    }

    public void Off()
    {
        StartCoroutine(OffNow());
    }

    public IEnumerator OffNow()
    {
        yield return new WaitForSeconds(closeDelay);
        animator.SetInteger("AnimState", 2);
    }
}
