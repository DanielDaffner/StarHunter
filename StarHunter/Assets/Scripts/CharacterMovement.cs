using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class CharacterMovement : MonoBehaviour {
    public CharacterController controller;
    public PlayerCollision_Own playerCollision_Own;

    public float speed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float rotateSpeed = 0.3f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Transform playerBody;
    Vector3 velocity;

    bool isGrounded;
    bool canAirJump = true;
    PhotonView photonView;

    public bool hasItem = false;
    public int itemType = 0;
    public float bonusMsTime = 0;

    private void Start() {
        photonView = GetComponent<PhotonView>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        if (!photonView.IsMine) return;

        //adjust speed
        if (playerBody.position.y < 2.5) { speed = 5f; } else { speed = 10f; }

        //test
        transform.Rotate(0, Input.GetAxisRaw("Rotate") * 60 * Time.deltaTime, 0);

        //Movement
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        
        if (bonusMsTime > 0) {
            controller.Move(move.normalized * speed * Time.deltaTime * 2f);
            bonusMsTime -= Time.deltaTime;
        }
        else {
            controller.Move(move.normalized * speed * Time.deltaTime);
        }

        //Rotation


        // print(Input.GetAxis("Rotate"));

        //var forward = transform.TransformDirection(Vector3.forward);

        //controller.SimpleMove(forward * speed);


        //Gravity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = 0f;
        }

        if (Input.GetButtonDown("Jump") && (isGrounded || canAirJump)) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canAirJump = false;
        }

        if (!isGrounded) {
            // print("Mid Air!");
            velocity.y += gravity * Time.deltaTime;

        }
        else {
            canAirJump = true;
        }
        controller.Move(velocity * Time.deltaTime);

        //Hit
        //if (Input.GetMouseButton(0)) {
       //     print("try hit");
        //    playerCollision_Own.hit();
       // }

        if (Input.GetMouseButton(1)) {
            if (hasItem) {
                if (itemType == 0) {
                    bonusMsTime = 1.5f;
                }
                if (itemType == 1) {
                    velocity.y += Mathf.Sqrt(jumpHeight * 4 * -2f * gravity);
                }
                hasItem = false;
                photonView.RPC("unsetHasItem", RpcTarget.AllBuffered, photonView.ViewID.ToString());
            }
        }
        else if (Input.GetButton("Fire1")) {
            playerCollision_Own.hit();
        }


    }
    [PunRPC]
    void unsetHasItem(string tmp) {
        
        //other = PhotonView.Find(int.Parse(tmp));
        hasItem = false;

    }



}
