using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected GameObject player;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    private void FixedUpdate()
    {
        navMeshAgent.destination = player.transform.position;
    }
}
