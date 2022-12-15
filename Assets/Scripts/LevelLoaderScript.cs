using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{

    [SerializeField] Animator transition;


    // // Update is called once per frame
    // void Update()
    // {
    //     if(Input.Get)
    // }

    public void LoadNextLevel(){
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadMenu(){
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int levelIndex){
        transition.SetTrigger("startCrossfade");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelIndex);
    }
}
