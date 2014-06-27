using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour
{
    public NinjaDeath death;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Deadly")
            OnDeath();
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Deadly")
            OnDeath();
    }

    public void OnDeath()
    {
        Destroy(gameObject);
        var t = transform;

        t.TransformPoint(0, -20, 0);
        NinjaDeath clone = Instantiate(death, t.position, Quaternion.identity) as NinjaDeath;
        if (clone != null) clone.rigidbody2D.AddForce(Vector3.right * Random.Range(-200, 200));

        GameObject go = new GameObject("ClickToContinue");
        ClickToContinue script = go.AddComponent<ClickToContinue>();
        script.scene = Application.loadedLevelName;
        go.AddComponent<DisplayRestartText>();
    }
}
