using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private HeroKnight player;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.transform.parent.GetComponent<HeroKnight>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "AttackBandit") {
            player.takeDamage();
            // Debug.Log("dano");
        }
    }
}
