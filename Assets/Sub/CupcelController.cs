using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupcelController : MonoBehaviour
{
    CharacterController charactor;

    Vector3 moveDirect = Vector3.zero;

    public float gravity;
    public float zSpeed;
    public float xSpeed;
    public float JumpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        charactor = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0.0f)
        {
            moveDirect.z = Input.GetAxis("Vertical") * zSpeed;
        }
        else
        {
            moveDirect.z = 0;
        }

        transform.Rotate(0, Input.GetAxis("Horizontal") * 5,0);
        
        if (Input.GetKeyDown("space"))
        {
            moveDirect.y = JumpSpeed;
        }
        moveDirect.y -= gravity * Time.deltaTime;

        Vector3 globalDirect = transform.TransformDirection(moveDirect);
        charactor.Move(globalDirect * Time.deltaTime);
    }
}
