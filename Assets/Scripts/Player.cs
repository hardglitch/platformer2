using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform groundCheck;
    private Animator animPlayer;
    public Main main;

    public float Speed = 5f;
    public float JumpHeight = 15f;

    private readonly int maxHealth = 3;
    private int curHealth;
    private int coins;
    private float breathTime, breathTimeMax = 30;
    public Image PlayerBreath;

    private bool isGrounded = true;
    private bool isClimbing = false;
    private bool key = false;
    public bool teleportation = false;
    public bool isImmortal = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animPlayer = GetComponent<Animator>();
        curHealth = maxHealth;
        breathTime = breathTimeMax;
    }

    private void Update()
    {
        CheckGround();

        if (Input.GetAxis("Horizontal") == 0 && isGrounded && !isClimbing)
        {
            animPlayer.SetInteger("State", 1);
        }
        else
        {
            FlipPlayer();
            if (isGrounded && !isClimbing)
            {
                animPlayer.SetInteger("State", 2);
            }
        }

        if (Input.GetAxis("Vertical") < 0 && isGrounded && !isClimbing)
            animPlayer.SetInteger("State", 4);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * Speed, rb.velocity.y);
        if (Input.GetAxis("Jump") > 0 && isGrounded && !isClimbing)
            //rb.AddForce(transform.up * JumpHeight, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
    }

    private void FlipPlayer()
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    public void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        isGrounded = colliders.Length > 1;
        if (!isGrounded && !isClimbing)
            animPlayer.SetInteger("State", 3);
    }

    public void RecountHealth(int deltaHealth)
    {
        if (!isImmortal)
        {
            curHealth += deltaHealth;

            if (curHealth <= 0)
            {
                curHealth = 0;
                GetComponent<BoxCollider2D>().enabled = false;
                Invoke(nameof(Lose), 1.5f);
            }
            if (curHealth > maxHealth)
                curHealth = maxHealth;

        }
    }

    //public IEnumerator OnHit()
    //{

    //    yield return new WaitForSeconds(0.3f);
    //    print("2222");
    //}


    //public IEnumerator Invulnerability(float time)
    //{
    //    isImmortal = true;

    //    while (true)
    //    {
    //        if (GetComponent<SpriteRenderer>().color.a < 1f)
    //        {
    //            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    //        }
    //        else
    //        {
    //            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
    //        }

    //    }



    //    print("Неуязвимость");
    //    yield return new WaitForSeconds(time);
    //    //StopCoroutine(OnHit());
    //    StopCoroutine(Invulnerability(0));
    //    isImmortal = false;
    //    print("Смертность");
    //}


    private void Lose()
    {
        main.GetComponent<Main>().Lose();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            coins++;
        }

        if (collision.gameObject.CompareTag("Heart"))
        {
            Destroy(collision.gameObject);
            RecountHealth(+2);
        }

        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            key = true;
        }

        if (collision.gameObject.CompareTag("Door"))
            if (collision.gameObject.GetComponent<Door>().isOpen && !teleportation)
            {
                collision.gameObject.GetComponent<Door>().Teleport(gameObject);
                teleportation = true;
                StartCoroutine(WaitTeleportation());
            }
            else 
            if (key && !collision.gameObject.GetComponent<Door>().isOpen)
                collision.gameObject.GetComponent<Door>().Unlock();


        if (collision.gameObject.CompareTag("Water"))
            StopCoroutine(BreathRestore());

    }

    private IEnumerator WaitTeleportation()
    {
        yield return new WaitForSeconds(1f);
        teleportation = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
//        if (collision.gameObject.CompareTag("Door") && key && teleportation)
//            teleportation = false;
        
        if (collision.gameObject.CompareTag("Ladder"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            isClimbing = false;
        }

        if (collision.gameObject.CompareTag("Water"))
            StartCoroutine(BreathRestore());
    }

    private IEnumerator BreathRestore()
    {
        breathTime += Time.deltaTime;
        PlayerBreath.fillAmount = breathTime / breathTimeMax;

        yield return new WaitForSeconds(0.00001f);

        if (breathTime >= breathTimeMax)
            StopCoroutine(BreathRestore());
        else
            StartCoroutine(BreathRestore());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
            if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0 && isClimbing)
            {
                transform.Translate(Vector3.up * Input.GetAxis("Vertical") * Speed * Time.deltaTime);
                rb.bodyType = RigidbodyType2D.Kinematic;
                isClimbing = true;
                animPlayer.SetInteger("State", 5);
            }

        if (collision.gameObject.CompareTag("Water"))
        {
            StopCoroutine(BreathRestore());
            breathTime -= Time.deltaTime;

            if (breathTime <= 0)
            {
                RecountHealth(-1);
                breathTime = 5;
            }

            PlayerBreath.fillAmount = breathTime / breathTimeMax;
        }
    }

    private IEnumerator TrampolineAnimation(Animator anim)
    {
        anim.SetBool("isJump", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isJump", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trampoline"))
            StartCoroutine(TrampolineAnimation(collision.gameObject.GetComponentInParent<Animator>()));
    }


    public int GetCoins()
    {
        return coins;
    }

    public int GetHealth()
    {
        return curHealth;
    }
}
