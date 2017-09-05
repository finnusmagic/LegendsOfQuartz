using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {


    [SerializeField] int startingHealth = 20;
    [SerializeField] float timeSinceLastHit = 0.5f;
    [SerializeField] float dissapearSpeed = .5f;

    private AudioSource audio;
    float timer = 0f;
    Animator anim;
    NavMeshAgent nav;
    bool isAlive;
    Rigidbody rigidBody;
    CapsuleCollider capsuleCollider;
    bool dissapearEnemy = false;
    int currentHealth;

    public bool IsAlive
    {
        get { return isAlive; }
    }

	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        isAlive = true;
        currentHealth = startingHealth;
	}
	
	void Update ()
    {
        timer += Time.deltaTime;

        if (dissapearEnemy)
        {
            transform.Translate(-Vector3.up * dissapearSpeed * Time.deltaTime);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if(other.tag == "PlayerWeapon")
            {
                TakeHit();
                timer = 0;
            }
        }
    }

    void TakeHit()
    {
        if (currentHealth > 0)
        {
            audio.PlayOneShot(audio.clip);
            anim.Play("Hurt");
            currentHealth -= 10;
        }
        if (currentHealth <= 0)
        {
            isAlive = false;
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        capsuleCollider.enabled = false;
        nav.enabled = false;
        anim.SetTrigger("EnemyDie");
        rigidBody.isKinematic = true;

        StartCoroutine(RemoveEnemy());
    }

    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(2f);
        dissapearEnemy = true;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
