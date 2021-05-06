using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            timer = 0;
            transform.position = new Vector3(0.5f * transform.localScale.x, transform.position.y, transform.position.z);
        } else
            if (timer >= 1)
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }
}
