using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class animationStateController : MonoBehaviour
{
    PhotonView photonView;
    Animator animator;
    AudioSource audioSWalk;
    AudioSource audioSHit;
    AudioSource audioSJump;
    AudioSource audioSJump2;
    int walkingHash;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        audioSWalk = GetComponents<AudioSource>()[0];
        audioSHit = GetComponents<AudioSource>()[1];
        audioSJump = GetComponents<AudioSource>()[2];
        audioSJump2 = GetComponents<AudioSource>()[3];
        animator = GetComponent<Animator>();
        walkingHash = Animator.StringToHash("walking");
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        bool walking = animator.GetBool(walkingHash);
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");

        //bool punch = Input.GetKey(KeyCode.Mouse0);


        // if player presses w key
        if (forwardPressed || backwardPressed || leftPressed || rightPressed)
        {
            animator.SetBool(walkingHash, true);
        }
        else
        {
            animator.SetBool(walkingHash, false);
        }

        // if player walks

        if (walking)
        {
            if (!audioSWalk.isPlaying) audioSWalk.Play();
        }
        else {
            audioSWalk.Stop();
        }

        //if player is not pressing a key

        if (Input.GetKey(KeyCode.Mouse0))
        {

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Armature_IdleAnimation")|| animator.GetCurrentAnimatorStateInfo(0).IsName("Armature_WalkAnimation"))
            {
                if(!audioSHit.isPlaying) audioSHit.Play();
                GetComponentInParent<PlayerCollision_Own>().hit();

                animator.SetBool("hitting", true);

            }
            
       
        }
        else
        {
            animator.SetBool("hitting", false);
        }
    }



}
