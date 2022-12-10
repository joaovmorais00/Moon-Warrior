using UnityEngine;
using System.Collections;

public class BoarWarrior : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private float               m_delayToIdle = 0.0f;

    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update ()
    {
        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (inputX < 0)
            GetComponent<SpriteRenderer>().flipX = true;

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        // -- Handle Animations --
        if(Input.GetMouseButtonDown(0))
            m_animator.SetTrigger("Attack");

        if (Input.GetMouseButtonDown(1))
            m_animator.SetTrigger("HeavyAttack");

        //Walk
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }
    }
}
