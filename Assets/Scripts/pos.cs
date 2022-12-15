using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pos : MonoBehaviour
{
   [SerializeField] public LevelLoaderScript loadLevel;

    // Start is called before the first frame update
    void Start()
    {
        
        Invoke("WaitToEnd", 40);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)){
            loadLevel.LoadMenu();
        }
    }

    public void WaitToEnd(){
        loadLevel.LoadMenu();
    }
}
