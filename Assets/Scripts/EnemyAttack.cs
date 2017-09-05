using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField] float range = 3f;
    [SerializeField] float timeBetweenAttacks = 1f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private BoxCollider[] weaponColliders;
    private EnemyHealth enemyHealth;

    void Start()
    {
        weaponColliders = GetComponentsInChildren<BoxCollider>();
        player = GameManager.instance.Player;
        anim = GetComponent<Animator>();
        StartCoroutine(Attack());
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive)
        {
            playerInRange = true;
        }
        else {
            playerInRange = false;
        }
    }

    IEnumerator Attack()
    {
        if (playerInRange && !GameManager.instance.GameOver)
        {
            anim.Play("Attack");
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        yield return null;
        StartCoroutine(Attack());
    }

    public void EnemyBeginAttack()
    {
        foreach (var weapon in weaponColliders)
        {
            weapon.enabled = true;
        }
    }

    public void EnemyEndAttack()
    {
        foreach (var weapon in weaponColliders)
        {
            weapon.enabled = false;
        }
    }
}
