using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class EnemyMove : MonoBehaviour {

    [SerializeField] Transform player;
    private NavMeshAgent nav;
    private Animator anim;

	void Start ()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
	}

    private void Awake()
    {
        Assert.IsNotNull(player);
    }

    void Update ()
    {
        nav.SetDestination(player.position);
	}
}
