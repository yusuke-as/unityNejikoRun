using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;
    const int DefaultLife = 3;
    const float StunDuration = 0.5f;
    bool isDamaged;

    CharacterController controller;
    Animator animator;

    Vector3 moveDirection = Vector3.zero;
    int targetLane;
    int life=DefaultLife;
    float recoverTime = 0.0f;

    public float gravity;
    public float speedZ;
    public float speedX;
    public float speedJump;
    public float accelerationZ;
    public float noCollisionTime;

    public int Life()
    {
        return life;
    }
    public bool IsStan()
    {
        return recoverTime > 0.0f || life <= 0;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isDamaged);
        if (Input.GetKeyDown("left"))
        {
            MoveToLeft();
        }
        if (Input.GetKeyDown("right"))
        {
            MoveToRight();
        }
        if (Input.GetKeyDown("space"))
        {
            Jump();
        }
        if (isDamaged)
        {
            noCollisionTime -= Time.deltaTime;
            if (noCollisionTime <= 0)
            {
                isDamaged = false;
            }           
        }


        if (IsStan())
        {
            moveDirection.x = 0.0f;
            moveDirection.z = 0.0f;
            recoverTime -= Time.deltaTime;
        }
        else
        {
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            //clampで引数1を2,3の間に挟み込む
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

            float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
            moveDirection.x = ratioX * speedX;
        }

        moveDirection.y -= gravity * Time.deltaTime;

        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);

        if (controller.isGrounded)
        {
            moveDirection.y = 0;
        }

        animator.SetBool("run", moveDirection.z > 0.0f);

        /*
        if (controller.isGrounded)
        {
            if (Input.GetAxis("Vertical") > 0.0f)
            {
                moveDirection.z = Input.GetAxis("Vertical") * speedZ;
            }
            else
            {
                moveDirection.z = 0;
            }

            transform.Rotate(0, Input.GetAxis("Horizontal") * 3, 0);

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = speedJump;
                animator.SetTrigger("jump");
                //Debug.Break();
            }
        }
        //Debug.Log(moveDirection.y);
        //deltaTimeは１フレーム毎の時間、機種依存をなくせる
        moveDirection.y -= gravity * Time.deltaTime;
        //実際の移動処理はここだけ！
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);

        if (controller.isGrounded)
        {
            moveDirection.y = 0;
        }

        animator.SetBool("run", moveDirection.z > 0.0f);
        */
    }

    public void MoveToLeft()
    {
        if (IsStan())
        {
            return;
        }
        if(controller.isGrounded&& targetLane > MinLane)
        {
            targetLane--;
        }
    }
    public void MoveToRight()
    {
        if (IsStan())
        {
            return;
        }
        if (controller.isGrounded&& targetLane < MaxLane)
        {
            targetLane++;
        }
    }
    public void Jump()
    {
        if (IsStan())
        {
            return;
        }
        if (controller.isGrounded)
        {
            moveDirection.y = speedJump;
            animator.SetTrigger("jump");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (IsStan())
        {
            return;
        }
       
        if (hit.gameObject.tag == "Robo")
        {
            if (isDamaged==false)
            {
                life--;
                recoverTime = StunDuration;
                animator.SetTrigger("damage");
                isDamaged = true;
                noCollisionTime = 2.0f;
            }                       
            Destroy(hit.gameObject);           
        }
    }
}
