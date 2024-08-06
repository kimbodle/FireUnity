using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    //private Animator animator;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // 입력 처리
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();  // 대각선 이동 속도를 일정하게 유지

        // 애니메이션 상태 업데이트
        //UpdateAnimation(); 
    }
    void FixedUpdate()
    {
        // 물리 이동 처리
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    /*
     void UpdateAnimation()
    {
        if (movement.magnitude > 0)
        {
            animator.SetBool("isMoving", true);

            // 이동 방향에 따라 애니메이션 변경
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                animator.SetFloat("MoveX", movement.x);
                animator.SetFloat("MoveY", 0);
            }
            else
            {
                animator.SetFloat("MoveX", 0);
                animator.SetFloat("MoveY", movement.y);
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }*/
}
