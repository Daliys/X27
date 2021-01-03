using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPS_Generall : MonoBehaviour
{
    public GameObject player;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(JoyAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);//, Vector3.up);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
    

    private IEnumerator JoyAnimation()
    {
        yield return new WaitForSecondsRealtime(Random.Range(0f, 1f));

        if (Random.value > 0.5f) animator.SetBool("IsJoy1", true);
        else animator.SetBool("IsJoy2", true); 

        yield return new WaitForSecondsRealtime(Random.Range(5, 10));
        animator.SetBool("IsJoy1", false);
        animator.SetBool("IsJoy2", false);
    }

}
