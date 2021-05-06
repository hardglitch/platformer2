using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destoyable : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 8f, ForceMode2D.Impulse);
            gameObject.GetComponentInParent<EnemyCollision>().StartDeath();
        }
    }
}
