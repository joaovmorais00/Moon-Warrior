using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{

    [SerializeField] public LevelLoaderScript loadLevel;

    // Start is called before the first frame update
    void Start()
    {
        
        Invoke("WaitToEnd", 60);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)){
            loadLevel.LoadNextLevel();
        }
    }

    public void WaitToEnd(){
        loadLevel.LoadNextLevel();
    }
}
