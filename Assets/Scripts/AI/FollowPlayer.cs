using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    // CODE FOR NPC AGENT FOLLOWING PLAYER (attach to agent)
    public Transform player;
    public float radiusToFollow;
    private NavMeshAgent agent;
    public float agentSpeed = 3.5f;
    public GameObject npcGuide;
    //public bool hasBeenCaught;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            // increase speed so that "chase" speed is faster than roam speed
            agent.speed = agentSpeed;
            float distNPCToPlayer = Vector3.Distance(transform.position, player.position);

            //if the NPC comes within a certain radius of the player, start following the player
            if (distNPCToPlayer <= radiusToFollow) {
                agent.destination = player.position;
            }
        }
    }
    /////////////////////////////////////////////////////

    // if monkey collides with player
    void OnCollisionEnter(Collision collision) {
        LostFoundGame lfScript = npcGuide.GetComponent<LostFoundGame>();
        CollectPlateGame cpScript = npcGuide.GetComponent<CollectPlateGame>();

        // if collide and have played intro dialogue but game hasn't been played yet
        if (collision.gameObject.CompareTag("Player")) {
            if (lfScript != null) {
                if (lfScript.hasPlayedIntroDialogue && !lfScript.gameIsOver) {
                    lfScript.StealFromInventory();
                }
            }
            if (cpScript != null) {
                if (cpScript.hasPlayedIntroDialogue && !cpScript.gameIsOver) {
                    cpScript.BreakPlate();
                }
            }
        }
    }
    //to see the radius around the player that the NPC has to be in to start following it
    // void OnDrawGizmosSelected() {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireSphere(player.position, radiusToFollow);
    // }
}
