using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public PlayerCollision_Own playerCollision_Own;

    public float speed = 15f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float rotateSpeed = 0.6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Transform playerBody;

    Vector3 velocity;

    bool isGrounded;

    PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;
        //test
        transform.Rotate(0, Input.GetAxis("RSY") * 120 * Time.deltaTime, 0);
        //Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime );

        //Rotation


        print(Input.GetAxis("RSY"));
       
      //  var forward = transform.TransformDirection(Vector3.forward);
      //  float curSpeed = speed * Input.GetAxis("RSY");
     // controller.SimpleMove(forward * curSpeed);
   

        //Gravity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (!isGrounded)
        {
           // print("Mid Air!");
            velocity.y += gravity * Time.deltaTime;
           
        }
        controller.Move(velocity * Time.deltaTime);



        //Hit
        if (Input.GetMouseButton(0))
        {
            print("try hit");
        }
        if (Input.GetMouseButton(0) && playerCollision_Own.ableToHit)
        {
            playerCollision_Own.hit();
        }

    }

   

}
