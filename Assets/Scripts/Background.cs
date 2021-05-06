using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float lenght, startPosition;
    public GameObject cam;
    public float parallaxEffect = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (parallaxEffect >= 0 && parallaxEffect <= 1)
        {
            float tmp = cam.transform.position.x * (1 - parallaxEffect);
            float distance = cam.transform.position.x * parallaxEffect;
            transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

            if (tmp > startPosition + lenght)
                startPosition += lenght;
            else if (tmp < startPosition - lenght)
                startPosition -= lenght;
        }
    }
}
