using System.Collections;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float speed = 2f;
    public float waitTime = 2f;
    private bool CanGo = true;
    private bool moveLeft;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(point1.position.x, point1.position.y, transform.position.z);

        if (point1.position.x < point2.position.x)
            moveLeft = false;
        else
            moveLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanGo)
        {
            transform.position = Vector3.MoveTowards(transform.position, point1.position, speed * Time.deltaTime);
        }

        if (transform.position == point1.position)
        {
            Transform tmp = point1;
            point1 = point2;
            point2 = tmp;
            CanGo = false;
            StartCoroutine(Waiting());
        }
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);

        if (moveLeft)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else
            transform.eulerAngles = new Vector3(0, 180, 0);

        moveLeft = !moveLeft;
        CanGo = true;
    }
}
