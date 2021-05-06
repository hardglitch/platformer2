using System.Collections;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    private readonly float timeToDisable = 3f;

    private void Start()
    {
        StartCoroutine(SetDisabled());
    }

    void Update()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }

    IEnumerator SetDisabled()
    {
        yield return new WaitForSeconds(timeToDisable);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine(SetDisabled());
            gameObject.SetActive(false);
        }
    }
}
