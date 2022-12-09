using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBandit : MonoBehaviour
{
    private Bandit bandit;
    // Start is called before the first frame update
    void Start()
    {
        bandit = gameObject.transform.parent.GetComponent<Bandit>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Attack") {
            bandit.health--;
            Debug.Log("dano");}
        
        
    }
}
