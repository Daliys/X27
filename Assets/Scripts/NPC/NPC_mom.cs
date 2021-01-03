using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter))]
public class NPC_mom : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter character { get; private set; } // the character we are controlling
    private Transform currentTarget;                                    // target to aim for
    private int currentTargetId = 0;


    [SerializeField]
    public Transform[] targets;
    public GameObject cakeObj;
    private Plot plot;
    bool isSenedToActivation = false;
    string idName;
    private Animator animator;

    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();

        agent.updateRotation = false;
        agent.updatePosition = true;
        animator = GetComponent<Animator>();
        
        currentTarget = targets[0];
       // isCurrentTaskComplited = false; 
        
    }


    private void Update()
    {
      /*  if (currentTarget == null)
        {
            if(targets.GetLength(0)-1 >= currentTargetId)
            currentTarget = targets[currentTargetId];
            currentTargetId++;
        }
        */
        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.position);         
        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            animator.SetBool("IsMoving", true);
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            animator.SetBool("IsMoving", false);
            character.Move(Vector3.zero, false, false);
        }

        if (currentTargetId == 1 && agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
            transform.LookAt(cakeObj.transform);
            cakeObj.transform.parent = transform;
            cakeObj.transform.localEulerAngles = new Vector3(-90, 0, 0);
            cakeObj.transform.localPosition = new Vector3(0, 1.47f, 1.369f);
            currentTargetId++;
            currentTarget = targets[currentTargetId];
            agent.SetDestination(currentTarget.position);
        }

        if (!isSenedToActivation && currentTargetId == 2 && agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
            isSenedToActivation = true;
            InteractiveActivationObject interactive = GetComponent<InteractiveActivationObject>();
            interactive.SetActiveInteractive(plot, true, idName);
        }
        if(currentTargetId == 3 && agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
            cakeObj.transform.parent = null;
            cakeObj.transform.position = new Vector3(-4.8f, 1f, 4f);
            transform.eulerAngles = new Vector3(0, 98.1f, 0);
            animator.SetBool("IsSitting", true);
        }


        /*   if (currentTargetId == 2)
           {
               cakeObj.transform.parent = transform;
               isCurrentTaskComplited = true;
           }else if(currentTargetId == 3)
           {
               InteractiveActivationObject interactive = GetComponent<InteractiveActivationObject>();
               interactive.SetActiveInteractive(plot, true);
           }


           if (isCurrentTaskComplited) currentTarget = null;
           */
    }


    public void ActivateAction(Plot plot, string idName)
    {
        this.plot = plot;
        currentTargetId++;
        currentTarget = targets[currentTargetId];
        agent.SetDestination(currentTarget.position);
        this.idName = idName;
    }

    public void SetTarget(Transform target)
    {
        this.currentTarget = target;
        
    }
}

