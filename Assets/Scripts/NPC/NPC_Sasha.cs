using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter))]
public class NPC_Sasha : MonoBehaviour
{
    public GameObject player;
    public GameObject gift;
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter character { get; private set; } // the character we are controlling
    public Transform target;                                    // target to aim for
    private Animator animator;
    public float speedAnimation;

    private bool isFollow = false;

    void Start()
    {
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
        agent.updateRotation = false;
        agent.updatePosition = true;

        animator = GetComponent<Animator>();
        animator.SetBool("IsWaitingWithGift", true);
        animator.SetFloat("SpeedMovement", speedAnimation);
    }

    // Update is called once per frame
    void Update()
    {
         transform.LookAt(player.transform);//, Vector3.up);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (target != null && isFollow)
        {
            agent.SetDestination(target.position);
        }

        if (agent.remainingDistance > agent.stoppingDistance && isFollow)
        {
            animator.SetBool("IsMovement", true);
            character.Move(agent.desiredVelocity, false, false);
        }
        else if(isFollow)
        {
            animator.SetBool("IsMovement", false);
            character.Move(Vector3.zero, false, false);
        }

     
    }

    public void HideGift()
    {
        gift.SetActive(false);
        isFollow = true;
        animator.SetBool("IsWaitingWithGift", false);
    }
}
