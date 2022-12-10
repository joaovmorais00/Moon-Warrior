using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoss : MonoBehaviour
{
    private Vampire boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = gameObject.transform.parent.GetComponent<Vampire>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Attack") {
            boss.takeDamage();
            // Debug.Log("dano");
        }
    }
}
