using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private int Damage = -1;
    private bool isHit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isHit)
        {
            collision.gameObject.GetComponent<Player>().RecountHealth(Damage);
            //StartCoroutine(collision.gameObject.GetComponent<Player>().Invulnerability(5f));
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 8f, ForceMode2D.Impulse);
        }
    }

    public IEnumerator Death()
    {
        isHit = true;
        GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, -1);
        GetComponent<Animator>().SetBool("Death", true);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = false;
        
        transform.Find("WeakPoint").GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public void StartDeath()
    {
        StartCoroutine(Death());
    }
}
