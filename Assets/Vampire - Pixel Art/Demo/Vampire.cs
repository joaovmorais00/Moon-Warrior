using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Vampire : MonoBehaviour {

    [SerializeField] float      m_speed = 1.4f;
    // [SerializeField] float      m_jumpForce = 2.0f;
    // [SerializeField] float      m_rollForce = 80.0f;
    [SerializeField] float      m_airAttackForce = 120.0f;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Vampire      m_groundSensor;
    private bool                m_grounded = false;
    private bool                m_isAttack1 = true;
    private float               m_disableInputTimer = 0.0f;

    private bool               m_isDead = false;
    
    private Transform target;

    private Transform attackBoss;

    [SerializeField] public int health = 3;

    private float attackRate = 2f;
    private float nextAttack = 0;
    private float timeDeath = 3f;
    private float damageTime = 1f;
    //Variavel pra setar animação da morte do player somente uma vez
    private bool setDeath = false;

    [SerializeField] public bool facingRight= false;
    private float yForce;
    private float walkTimer;

    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Vampire>();
        target = FindObjectOfType<HeroKnight>().transform;
        attackBoss = transform.Find("AttackBoss").transform;

        m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
    }
	
	// Update is called once per frame
	void Update () {
        //Check if character just landed on the ground
        // if (!m_grounded && m_groundSensor.State()) {
            
        // }
        

        //Check if character just started falling
        // if(m_grounded && !m_groundSensor.State()) {
        //     m_grounded = false;
        //     m_animator.SetBool("Grounded", m_grounded);
        // }

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // -- Handle input and movement --
        m_disableInputTimer -= Time.deltaTime;

        if (m_disableInputTimer < 0.0f) {
            if(!m_isDead){
                facingRight = (target.position.x + 0.25  < transform.position.x) ? true : false;
                if (facingRight) {
                    GetComponent<SpriteRenderer>().flipX = true;
                    facingRight = false;
                    // Vector3 position = new Vector3(0f, attackBoss.transform.localPosition.y, attackBoss.transform.localPosition.z);
                    // attackBoss.transform.localPosition = position;
                } else {
                    GetComponent<SpriteRenderer>().flipX = false;
                    facingRight = true;
                    // Vector3 position = new Vector3(0f, attackBoss.transform.localPosition.y, attackBoss.transform.localPosition.z);
                    // attackBoss.transform.localPosition = position;
                }

                walkTimer += Time.deltaTime;
                damageTime -= Time.deltaTime;

                if(health<=0){
                    m_isDead = true;
                }

                
                

                // Move
                // m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

                // -- Handle Animations --
                //Death
                // if (Input.GetKeyDown("e"))
                //     m_animator.SetTrigger("Death");

                //Hurt
                // else if (Input.GetKeyDown("q"))
                //     m_animator.SetTrigger("Hurt");

                //Attack
                // else if (Input.GetMouseButtonDown(0)) {
                //     if(m_isAttack1)
                //         m_animator.SetTrigger("Attack 1");
                //     else
                //         m_animator.SetTrigger("Attack 2");
                //     m_isAttack1 = !m_isAttack1;
                //     m_body2d.velocity = new Vector2(0.0f, m_body2d.velocity.y);
                //     m_disableInputTimer = 0.5f;
                // }

                //Air Attack
                // else if (Input.GetMouseButtonDown(1)) {
                //     m_animator.SetTrigger("AirAttack");
                //     m_body2d.velocity = new Vector2(0.0f, m_body2d.velocity.y);
                //     m_body2d.AddForce(transform.right * transform.localScale.x * m_airAttackForce);
                //     m_disableInputTimer = 0.6f;
                // }

                //Roll
                // else if (Input.GetKeyDown("left shift")) {
                //     m_animator.SetTrigger("Roll");
                //     m_body2d.velocity = new Vector2(0.0f, m_body2d.velocity.y);
                //     m_body2d.AddForce(transform.right * transform.localScale.x * m_rollForce);
                //     m_disableInputTimer = 0.7f;
                // }

                //Jump
                // else if (Input.GetKeyDown("space") && m_grounded) {
                //     m_animator.SetTrigger("Jump");
                //     m_grounded = false;
                //     m_animator.SetBool("Grounded", m_grounded);
                //     m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                //     m_groundSensor.Disable(0.2f);
                // }

                //Run
                // else if (Mathf.Abs(inputX) > Mathf.Epsilon)
                //     m_animator.SetInteger("AnimState", 1);

                //Idle
                
                    // m_animator.SetInteger("AnimState", 0);
            }
            else{
                if(m_isDead && timeDeath>0 ){
                    timeDeath -= Time.deltaTime;
                    if(!setDeath){
                        m_animator.SetTrigger("Death");
                        setDeath=true;
                    }
                }

                if(timeDeath<=0){
                    gameObject.SetActive(false);
                    SceneManager.LoadScene(3);
                }

               
            }
            
        }
    }

    private void FixedUpdate(){
        if(!m_isDead){
            Vector2 targetDistance = target.position - transform.position;
            float hForce = targetDistance.x / Mathf.Abs(targetDistance.x);
           

            if(Mathf.Abs(targetDistance.x) < 4f && Mathf.Abs(targetDistance.y) <4f && Time.time> nextAttack) {
                
                m_animator.SetTrigger("Attack 1");
                nextAttack = Time.time + attackRate;
            }
            if(walkTimer >= Random.Range(1f, 2f)){
                yForce = Random.Range(-1, 2);
                walkTimer = 0;
            }

            if(Mathf.Abs(targetDistance.y) < 1.5f){
                hForce = 0;
            }

            if(hForce !=0 || yForce !=0){
                Debug.Log("entrou");
                m_animator.SetInteger("AnimState", 1);
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
