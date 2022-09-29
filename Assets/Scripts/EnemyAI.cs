using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject[] player;
    public LayerMask whatIsPlayer;
    public int whoImChasing;
    
    
    //Patrolling
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    NavMeshHit hit;
    
    
    //Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Awake()
    {
        player[0] = GameObject.Find("Player1");
        player[1] = GameObject.Find("Player2");
        agent = GetComponent<NavMeshAgent>();
        whatIsPlayer = LayerMask.GetMask("whatIsPlayer");
    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

    }

    private void SearchWalkPoint()
    {
        
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (NavMesh.SamplePosition(walkPoint, out hit, 1f, 1))
        {
            walkPointSet = true;
        }

    }

    private void ChasePlayer()
    { 
        agent.SetDestination(player[whoImChasing].transform.position);
    }

    private void WhoImChasing()
    {
        if (Vector3.Distance(player[0].transform.position, this.transform.position) < Vector3.Distance(player[1].transform.position, this.transform.position) && !player[0].GetComponent<PlayerController>()._isSafe)
        {
            whoImChasing = 0;
        }
        else if(!player[1].GetComponent<PlayerController>()._isSafe)
        {
            whoImChasing = 1;
        }
        
    }

    private void AttackPlayer()
    {

        agent.SetDestination(transform.position);
        transform.LookAt(player[whoImChasing].transform);

        if (!alreadyAttacked && !player[whoImChasing].GetComponent<PlayerController>()._isSafe)
        {
            //Insert code for attacking
            this.GetComponent<CharacterWeapon>().ShootBullet();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void ResetAttack()
    {

        alreadyAttacked = false;

    }
    
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (player[0].activeSelf && player[1].activeSelf)
        {
            WhoImChasing();
            
            if ((!playerInAttackRange && !playerInSightRange) || player[whoImChasing].GetComponent<PlayerController>()._isSafe)
            {
                Patrolling();
            }
            else if (playerInSightRange && !playerInAttackRange && !player[whoImChasing].GetComponent<PlayerController>()._isSafe)
            {
                ChasePlayer();
            }
            else if (playerInAttackRange && playerInSightRange && !player[whoImChasing].GetComponent<PlayerController>()._isSafe)
            {
                AttackPlayer();
            }
        }
        else
        {
            Application.Quit();
        }
        
        if (agent.isStopped)
        {
            Patrolling();
        }
        
    }
}