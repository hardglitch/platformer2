using System.Collections;
using UnityEngine;

public class EnemyBomber : MonoBehaviour
{
    public GameObject bullet;
    public Transform shoot;
    public float timeShoot = 2f;

    // Start is called before the first frame update
    void Start()
    {
        shoot.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        StartCoroutine(Shooting());
    }

    private void Update()
    {
        shoot.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(timeShoot);
        Instantiate(bullet, shoot.transform.position, transform.rotation);
        StartCoroutine(Shooting());
    }
}
