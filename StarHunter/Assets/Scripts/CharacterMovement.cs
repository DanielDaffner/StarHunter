using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float rotateSpeed = 0.6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool is_doublejump_available = true;
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

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime );

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            is_doublejump_available = true;
        }

        if (Input.GetButtonDown("Jump") )
        {
            if(isGrounded) {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            } else if (is_doublejump_available) {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                is_doublejump_available = false;
            }        
        }

        if (!isGrounded)
        {
            print("Mid Air!");
            velocity.y += gravity * Time.deltaTime;
           
        }
        controller.Move(velocity * Time.deltaTime);
    }
}
