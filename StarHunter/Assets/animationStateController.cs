using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int walkingHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        walkingHash = Animator.StringToHash("walking");
    }

    // Update is called once per frame
    void Update()
    {
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

        //if player is not pressing a key

        if (Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetBool("hitting", true);
        }
        else
        {
            animator.SetBool("hitting", false);
        }
    }



}
