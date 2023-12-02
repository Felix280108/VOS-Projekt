using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    public float jumpH = 5;
    private bool isGrounded = false;
    private Rigidbody2D rb;

    private Vector3 rotation;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        float richtung = Input.GetAxis("Horizontal");
        Steuerung(richtung);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    void Steuerung(float richtung)
    {
        if (richtung > 0)
        {
            transform.eulerAngles = rotation;
            transform.Translate(Vector2.right * speed * richtung * Time.deltaTime);
        }
        if (richtung < 0)
        {
            transform.eulerAngles = rotation - new Vector3(0, 180, 0);
            transform.Translate(Vector2.right * speed * -richtung * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpH, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
}
