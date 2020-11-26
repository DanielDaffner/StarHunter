using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraMovement : MonoBehaviour
{
    float rotation_x = 0f;
    [Range(50.0f, 200.0f)]
    public float sensitivity = 200f;
    //public PhotonView photonView;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //photonView = GetComponent<PhotonView>();
        Cursor.lockState = CursorLockMode.Locked;
        //player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       // if (!photonView.IsMine) return;
        float mouse_x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouse_y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotation_x -= mouse_y;
        rotation_x = Mathf.Clamp(rotation_x, -70f, 70f);

        transform.localRotation = Quaternion.Euler(rotation_x, 0f, 0f);
        if(player == null) {
            player = transform.parent.gameObject;
            Debug.Log(player.name);
        }
        player.transform.Rotate(Vector3.up * mouse_x);
    }
}
