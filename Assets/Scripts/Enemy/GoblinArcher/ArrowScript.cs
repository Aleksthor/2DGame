using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    float TimeLived = 0f;

    // Update is called once per frame
    void Update()
    {
        TimeLived += Time.deltaTime;

        if (TimeLived > 5f)
        {
            Destroy(this);
        }
    }
}
