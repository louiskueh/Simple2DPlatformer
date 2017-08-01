using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimplePlatformController : MonoBehaviour {
    [HideInInspector]
    public bool facingRight = true;

    [HideInInspector]
    public bool jump = false;

    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1200f;
    // Check if player is on ground 
    public Transform groundCheck;
    public Text countText;
    private int count;

    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        
    }
  void Start()
    {
        count = 0;
        setCountText();
    }
    // Update is called once per frame
    void Update () {
        //check if we hit anything below
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
        setCountText();

    }
    void setCountText()
    {
        countText.text = "Count  " + count.ToString();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Crate"))
        {
            Debug.Log("Hello, count is :" + count.ToString(), gameObject);
            count++;
            setCountText();
            Destroy(other.gameObject);

        }
    }


    //Physics - fixedUpdate
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
        {
            rb2d.AddForce(Vector2.right * h * moveForce);
        }
        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
        {
            // Set velocity to max speed
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        }
        if ( h > 0 && !facingRight)
        {
            Flip();
        } 
        else if ( h < 0 && facingRight)
        {
            Flip();
        }
        if (jump)
        {
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    //Flip right to left
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale; 
    }
}
