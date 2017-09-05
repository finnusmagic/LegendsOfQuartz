using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class EnemyMove : MonoBehaviour {

    [SerializeField] Transform player;
    private NavMeshAgent nav;
    private Animator anim;
    private EnemyHealth enemyHealth;

	void Start ()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();

    }

    private void Awake()
    {
        Assert.IsNotNull(player);
    }

    void Update ()
    {
        if (!GameManager.instance.GameOver && enemyHealth.IsAlive)
        {
            nav.SetDestination(player.position);
        }
        else if ((!GameManager.instance.GameOver || GameManager.instance.GameOver) && !enemyHealth.IsAlive)
        {
            nav.enabled = false;
        }
        else
        {
            nav.enabled = false;
            anim.Play("Idle");
        }
	}
}
