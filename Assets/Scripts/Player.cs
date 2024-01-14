using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public float speed = 5;
    public float jumph = 5;
    private bool isGrounded = false;
    private bool mobile_jump = false;
    private Rigidbody2D rb;

    private Animator anim;
    private Vector3 rotation;

    private CoinManager m;
    private ButtonManager s;

    public GameObject gameOverScreen;
    public GameObject finishScreen;

    public GameObject deathEffect;

    public GameObject kamera;

    public FixedJoystick joystick;
    public GameObject jumpButton;
    public GameObject controllstick;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rotation = transform.eulerAngles;
        m = GameObject.FindGameObjectWithTag("Text").GetComponent<CoinManager>();
        s = GetComponent<ButtonManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float richtung = Input.GetAxis("Horizontal"); // Variabel in welche Richtung der Spieler laufen soll
        float mobile_richtung = joystick.Horizontal;
        if (mobile_richtung != 0)
        {
            richtung = mobile_richtung;
        }
        Steuerung(richtung);
        Animation(richtung);
        kamera.transform.position = new Vector3(transform.position.x, 0, -10);
    }
    public void JumpButton()
    {
        mobile_jump = true;
        MobileJump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            gameOverScreen.SetActive(true);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag ==  "CoinB")
        {
            m.AddMoney(1);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "CoinS")
        {
            m.AddMoney(2);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "CoinG")
        {
            m.AddMoney(3);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Spike")
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            gameOverScreen.SetActive(true); 
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Finish")
        {
            finishScreen.SetActive(true);
            Destroy(gameObject);
        }
    }
    
    void Steuerung(float richtung)
    {
        if (richtung < 0)
        {
            transform.eulerAngles = rotation - new Vector3(0, 180, 0);
            transform.Translate(Vector2.right * speed * -richtung * Time.deltaTime);
        }
        if (richtung > 0)
        {
            transform.eulerAngles = rotation;
            transform.Translate(Vector2.right * speed * richtung * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumph, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void MobileJump()
    {
        if (isGrounded && mobile_jump)
        {
            mobile_jump = false;
            rb.AddForce(Vector2.up * jumph, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void Animation(float richtung)
    {
        if (richtung != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        if (isGrounded == false)
        {
            anim.SetBool("IsJumping", true);
        }
        else
        {
            anim.SetBool("IsJumping", false);
        }
    }

    public void ShowMobileControll()
    {
        jumpButton.SetActive(true);
        controllstick.SetActive(true);
        s.HideSettings();
    }
    public void HideMobileControll()
    {
        jumpButton.SetActive(false);
        controllstick.SetActive(false);
        s.HideSettings();
    }
}