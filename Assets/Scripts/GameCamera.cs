using System;
using System.Collections;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public float speed = 3f;
    public Transform target;
    public float bottomY = -4f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            Vector3 position = target.position;
            position.z = transform.position.z;
            if (position.y < bottomY)
                position.y = bottomY;
            transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
        }
        catch (Exception error)
        {
            Debug.LogError(error);
        }
    }
}
