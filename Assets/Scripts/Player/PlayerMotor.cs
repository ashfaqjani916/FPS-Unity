using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1f;
    public bool sprinting = false;
    public bool crouching = false;
    private float crouchTimer = 0f;
    private bool lerpCrouch = true;
    private GameObject player;
    private GameObject barrel ;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if(lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer/1;
            p *= p;
            if(crouching)
            controller.height = Mathf.Lerp(controller.height,1,p);
            else
            controller.height = Mathf.Lerp(controller.height,2,p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }

    }

    // recieve the inputs from our InputManager.cs and apply them to out character controller.
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection)*speed*Time.deltaTime);
        playerVelocity.y+= gravity *Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
        playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
        
    }

    public void jump(){
        if(isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if(sprinting)
        speed = 8;
        else 
        speed = 5;
    }

    public void Shoot()
    {
         
        
        
        barrel = GameObject.FindGameObjectWithTag("Player");
        Vector3 point = barrel.transform.position;
        Ray ray = new Ray(point,barrel.transform.forward);
       RaycastHit hit;
       if(Physics.Raycast(point,ray.direction,out hit,20f))
       {
        
        GameObject bullet = GameObject.Instantiate(Resources.Load("bullet 1") as GameObject,point,player.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-1f,1f),Vector3.up) * ray.direction*40;
        
        Debug.DrawRay(ray.origin, ray.direction*20f);
       }
    }
}
