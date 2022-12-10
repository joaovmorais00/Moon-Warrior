using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {

    [SerializeField] float  m_speed;

    private float attackRate = 1f;

    private float yForce;
    private float walkTimer;
    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;
    private Transform target;
    private bool facingRight;

    private Transform attackBandit;

    [SerializeField] public int health = 3;

    private float nextAttack = 0;
    private float timeDeath = 1.5f;
    private float damageTime = 1f;

    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<HeroKnight>().transform;
        m_animator.SetInteger("AnimState", 2);
        attackBandit = transform.Find("AttackBandit").transform;
    }
	
	// Update is called once per frame
	void Update () {

        facingRight = (target.position.x + 0.25  < transform.position.x) ? false : true;
        if (facingRight) {
            GetComponent<SpriteRenderer>().flipX = true;
            facingRight = false;
            Vector3 position = new Vector3(1.0f, attackBandit.transform.localPosition.y, attackBandit.transform.localPosition.z);
            attackBandit.transform.localPosition = position;
        } else {
            GetComponent<SpriteRenderer>().flipX = false;
            facingRight = true;
            Vector3 position = new Vector3(0.0f, attackBandit.transform.localPosition.y, attackBandit.transform.localPosition.z);
            attackBandit.transform.localPosition = position;
        }

        walkTimer += Time.deltaTime;
        damageTime -= Time.deltaTime;


        if(health<=0){
            m_isDead = true;
        }

        if(m_isDead){
            m_animator.SetTrigger("Death");
            timeDeath -= Time.deltaTime;
        }

        if(timeDeath<=0){
            gameObject.SetActive(false);
        }

        // // -- Handle input and movement --
        // float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        // if (inputX > 0)
        //     transform.localScale = new Vector3(-3.8f, 3.8f, 3.8f);
        // else if (inputX < 0)
        //     transform.localScale = new Vector3(3.8f, 3.8f, 3.8f);

        // // Move
        // m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        // //Set AirSpeed in animator
        // m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

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
           

            if(Mathf.Abs(targetDistance.x) < 2f && Mathf.Abs(targetDistance.y) <3f && Time.time> nextAttack) {
                
                m_animator.SetTrigger("Attack");
                nextAttack = Time.time + attackRate;
            }
            if(walkTimer >= Random.Range(1f, 2f)){
                yForce = Random.Range(-1, 2);
                walkTimer = 0;
            }

            if(Mathf.Abs(targetDistance.y) < 1.5f){
                hForce = 0;
            }

            m_body2d.velocity = new Vector2(hForce * m_speed, yForce * m_speed);
        }
    }

    public void takeDamage(){
        if(damageTime<=0){
            health--;
            m_animator.SetTrigger("Hurt");
            damageTime = 2;
        }
    }

    
}
