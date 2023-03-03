using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel((SceneManager.GetActiveScene().buildIndex + 1)));
    }
    // adding delay so scene transition is not immediate
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("StartFade");

        yield return new WaitForSeconds(transitionTime);

        //SceneManager.LoadScene(levelIndex);

        transition.SetTrigger("EndFade");

        yield return new WaitForSeconds(transitionTime);

        transition.SetTrigger("Original");

        

    }
}
