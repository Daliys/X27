using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    private AsyncOperation operation;
    private Canvas canvas;
    private Animator animator;
    public Text text;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>(true);
        DontDestroyOnLoad(gameObject);
        animator = GetComponent<Animator>();
    }

    public void LoadScene(int sceneName)
    {
        StartCoroutine(BeginLoad(sceneName));
    }

    public void LastScene()
    {
        animator.SetBool("IsLoadNew", false);
        StartCoroutine(TextShowing());
    }

   IEnumerator TextShowing()
    {
        yield return new WaitForSecondsRealtime(3);
        text.text = "To be continued...";
    }

    private IEnumerator BeginLoad(int sceneID)
    {

        animator.SetBool("IsLoadNew", false);

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("LoadingScene_1"))
        {

            yield return null;
        }

        operation = SceneManager.LoadSceneAsync(sceneID);


        while (!operation.isDone)
        {
            yield return null;
        }

        operation = null;
        animator.SetBool("IsLoadNew", true);

    }

}
