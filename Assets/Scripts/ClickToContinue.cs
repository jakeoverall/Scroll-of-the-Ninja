using UnityEngine;
using System.Collections;

public class ClickToContinue : MonoBehaviour
{

    public string scene;
	private float cooldown = 1.3f;
    private bool loadLock;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		cooldown -= Time.deltaTime;
		if (Input.anyKeyDown && !loadLock && cooldown <= 0)
	        LoadScene();
	}

    void LoadScene()
    {
		cooldown = 1.3f;
        loadLock = true;
        Application.LoadLevel(scene);
    }
}
