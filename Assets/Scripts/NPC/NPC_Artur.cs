using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter))]
public class NPC_Artur : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter character { get; private set; } // the character we are controlling
    private Transform currentTarget;                                    // target to aim for
    private int currentTargetId = 0;


    [SerializeField]
    public Transform[] targets;
    public GameObject mainCharacter;

    private PlotMestoX plot;
    bool isSenedToActivation = false;
    string idName;
    private Animator animator;
    public float speedAnimation;

    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();

        agent.updateRotation = false;
        agent.updatePosition = true;

        animator = GetComponent<Animator>();
        animator.SetFloat("SpeedAnimation", speedAnimation);

        currentTarget = targets[0];
       
    }


    private void Update()
    {
      
        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.position);         
        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            animator.SetBool("IsWalking", true);
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            character.Move(Vector3.zero, false, false);
        }

        if (currentTargetId == 1 && agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {

            currentTargetId++;
            currentTarget = targets[currentTargetId];
            agent.SetDestination(currentTarget.position);
        }
     /*   if (currentTargetId == 2 && agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance == 0 && idName != "Ring")
        {
            isSenedToActivation = true;
            InteractiveWaitForEnter interactive = GetComponent<InteractiveWaitForEnter>();
            interactive.SetActiveInteractive(plot, true, idName);
        }
        */
        if (!isSenedToActivation && currentTargetId == 2 && agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
           // animator.SetBool("IsProposal", true);
           // animator.SetTrigger("isProposalDawn");
           // transform.LookAt(mainCharacter.transform);
            //mainCharacter.transform.LookAt(transform);

            isSenedToActivation = true;
            InteractiveWaitForEnter interactive = GetComponent<InteractiveWaitForEnter>();
            interactive.SetActiveInteractive(plot, true, idName);
        }


  
    }


    public void ActivateAction(PlotMestoX plot, string idName)
    {
        this.plot = plot;
        currentTargetId++;
        currentTarget = targets[currentTargetId];
        agent.SetDestination(currentTarget.position);
        this.idName = idName;
    }
    public void ActivateActionRing(PlotMestoX plot, string idName)
    {
        animator.SetBool("IsProposal", true);
        animator.SetTrigger("isProposalDawn");
        transform.LookAt(mainCharacter.transform);
    }
    public void SetTarget(Transform target)
    {
        this.currentTarget = target;
        
    }
}

