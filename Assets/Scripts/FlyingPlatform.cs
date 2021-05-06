using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float speed = 1f;
    int i = 1;

    void Start()
    {
        transform.position = new Vector3(points[0].position.x, points[0].position.y, transform.position.z);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        float posXold = transform.position.x;
        float posYold = transform.position.y;

        if (collision.gameObject.CompareTag("Player"))
        {
            transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

            collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x + transform.position.x - posXold, collision.gameObject.transform.position.y + transform.position.y - posYold, collision.gameObject.transform.position.z);

            if (transform.position == points[i].position)
                if (i < points.Length - 1)
                    i++;
                else i = 0;
        }
    }
}
