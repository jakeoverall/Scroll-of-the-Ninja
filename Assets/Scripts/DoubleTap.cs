using UnityEngine;
using System.Collections;

public class DoubleTap : MonoBehaviour
{
    private float ButtonCooler = 0.5f;
    public int ButtonCount = 0;

    void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            if (!(ButtonCooler > 0) || ButtonCount != 2)
            {
                ButtonCooler = 0.5f;
                ButtonCount += 1;
            }
        }

        if (ButtonCooler > 0)
        {
            ButtonCooler -= 1 * Time.deltaTime;
        }
        else
        {
            ButtonCount = 0;
        }
    }
}
