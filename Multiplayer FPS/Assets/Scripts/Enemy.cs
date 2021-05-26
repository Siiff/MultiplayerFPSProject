using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator     animator;
    private NavMeshAgent agent;
    private float        speedFast;
    private bool         alerted = false;
    private bool         attacking = false;

    public  GameObject   player;
    public  GameObject[] target;
    public  AudioClip    alert;
    public  AudioClip    zombieAtk;
            AudioSource  audioSource;
    public  int          actualTarget = 0;
    public  float        seekPlayerMinDistance = 10;
    public  float        speedNormal;
    public  float        speedAdd = 1;


    void Start()
    {
        player      = GameObject.FindWithTag("Player");
        agent       = GetComponent<NavMeshAgent>();
        animator    = GetComponent<Animator>();
        speedNormal = agent.speed;
        speedFast   = speedNormal + speedAdd;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        //*Celso*: se o Player chegar perto, SEEK
        if (CheckDistanceFrom(player) <= seekPlayerMinDistance)
        {
            agent.speed = speedFast;
            agent.SetDestination(player.transform.position);
            if (!alerted)
            {
                audioSource.PlayOneShot(alert);
                alerted = true;
            }
        }

        //*Celso*: se o Player se afastar, volta PATROL
        else
        {
            agent.speed = speedNormal;
            agent.SetDestination(target[actualTarget].transform.position);
            alerted = false;

            if (CheckDistanceFrom(target[actualTarget]) <= agent.stoppingDistance)
            {
                Debug.Log("Path complete.");
                actualTarget++;

                if (actualTarget >= target.Length)
                {
                    actualTarget = 0;
                }

                agent.SetDestination(target[actualTarget].transform.position);
            }
        }

        //*Celso*: Reaching the player ATTACK
        if (CheckDistanceFrom(player) <= agent.stoppingDistance && !attacking)
        {
            attacking = true;
            animator.SetTrigger("Attack");
            
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            audioSource.PlayOneShot(zombieAtk);
        }
        else
            attacking = false;
    }

    public float CheckDistanceFrom(GameObject chico)
    {
        return Vector3.Distance(transform.position, chico.transform.position);
    }

}
