using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;
    public override void Enter()
    {
    }
    public override void Perform()
    {
        if(enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);

            if(shotTimer >enemy.fireRate)
            {
                Shoot();
            }
            if(moveTimer > Random.Range(3,7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere *5) );
                moveTimer = 0;
            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if(losePlayerTimer > 8)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }
    public override void Exit()
    {
    }
    
    public void Shoot()
    {
        //store the reference to the gun barrel 
        Transform gunbarrel = enemy.gunBarrel;
        // instantiate a new bullet 
        GameObject bullet = GameObject.Instantiate(Resources.Load("bullet 1") as GameObject,gunbarrel.position,enemy.transform.rotation);
        // calculate the direction to the player
        Vector3 shootDirection = (enemy.Player.transform.position - gunbarrel.transform.position).normalized;
        //add force rigidbody of the bullet.
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-1f,1f),Vector3.up) * shootDirection *40;
        
        shotTimer=0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
