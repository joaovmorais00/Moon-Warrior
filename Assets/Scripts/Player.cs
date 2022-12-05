using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    public float maxSpeed = 4;
    public float jumpForce = 400;

    
    private float currentSpeed;
    private Rigidbody rb;
    private Animator anim;
    private Transform groundCheck;
    private bool onGround;
    private bool isDead = false;
    private bool facingRight = false;
    private bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        anim = GetComponent<Animator>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        currentSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        // Debug.Log(onGround);
        if(Input.GetButtonDown("Jump") && !onGround){
            // Debug.Log("teste");
            jump = true;
        }
    }

    private void FixedUpdate(){
        if(!isDead){
            float h = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            
            if(!onGround){
                z=0;

                rb.velocity = new Vector3(h*currentSpeed, rb.velocity.y, z * currentSpeed);

                if(h>0 && !facingRight){
                    Flip();
                }else if(h<0 && facingRight){
                    Flip();
                }
                Debug.Log("pulou");
                if(jump){
                    Debug.Log("pulou");
                    jump = false;
                    rb.AddForce(Vector3.up * jumpForce);
                }
            }
        }
    }

    void Flip(){
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
