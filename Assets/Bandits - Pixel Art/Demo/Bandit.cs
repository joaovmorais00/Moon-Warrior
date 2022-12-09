using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {

    [SerializeField] float  m_speed;
    // [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float maxHeight, maxWidth, minHeight, minWidth;

    private float yForce;
    private float walkTimer;
    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;
    private Transform target;
    private bool facingRight;

    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<HeroKnight>().transform;
        m_animator.SetInteger("AnimState", 2);
    }
	
	// Update is called once per frame
	void Update () {

        facingRight = (target.position.x < transform.position.x) ? false : true;
        if (facingRight) {
            GetComponent<SpriteRenderer>().flipX = true;
            facingRight = false;
        } else {
            GetComponent<SpriteRenderer>().flipX = false;
            facingRight = true;
        }

        walkTimer += Time.deltaTime;

    

        // -- Handle Animations --
        //Death
        // if (Input.GetKeyDown("e")) {
        //     if(!m_isDead)
        //         m_animator.SetTrigger("Death");
        //     else
        //         m_animator.SetTrigger("Recover");

        //     m_isDead = !m_isDead;
        // }
            
        // //Hurt
        // else if (Input.GetKeyDown("q"))
        //     m_animator.SetTrigger("Hurt");

        // //Attack
        // else if(Input.GetMouseButtonDown(0)) {
        //     m_animator.SetTrigger("Attack");
        // }

        // //Change between idle and combat idle
        // else if (Input.GetKeyDown("f"))
        //     m_combatIdle = !m_combatIdle;


        // // //Run
        // // else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        // //     m_animator.SetInteger("AnimState", 2);

        // //Combat Idle
        // else if (m_combatIdle)
        //     m_animator.SetInteger("AnimState", 1);

        // //Idle
        // else
        //     m_animator.SetInteger("AnimState", 0);
    }


    private void FixedUpdate(){
        if(!m_isDead){
            Vector2 targetDistance = target.position - transform.position;
            float hForce = targetDistance.x / Mathf.Abs(targetDistance.x);

            if(walkTimer >= Random.Range(1f, 2f)){
                yForce = Random.Range(-1, 2);
                walkTimer = 0;
            }

            if(Mathf.Abs(targetDistance.y) < 1.5f){
                hForce = 0;
            }

            m_body2d.velocity = new Vector2(hForce * m_speed, yForce * m_speed);

            m_body2d.position = new Vector2(Mathf.Clamp(m_body2d.position.x, minWidth + 1, maxWidth - 1), Mathf.Clamp(m_body2d.position.x, minHeight + 1, maxHeight - 1));
        }



}
}
