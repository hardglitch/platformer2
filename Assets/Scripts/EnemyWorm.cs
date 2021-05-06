using UnityEngine;

public class EnemyWorm : MonoBehaviour
{
    public float Speed = 3f;
    public bool moveLeft = true;
    public Transform groundDetect;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, 1f);

        if (!groundInfo.collider)
            TurnBack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
            TurnBack();
    }

    private void TurnBack()
    {
        if (moveLeft)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            moveLeft = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            moveLeft = true;
        }
    }
}
