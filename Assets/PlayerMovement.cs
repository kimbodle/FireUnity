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
        // �Է� ó��
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();  // �밢�� �̵� �ӵ��� �����ϰ� ����

        // �ִϸ��̼� ���� ������Ʈ
        //UpdateAnimation(); 
    }
    void FixedUpdate()
    {
        // ���� �̵� ó��
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    /*
     void UpdateAnimation()
    {
        if (movement.magnitude > 0)
        {
            animator.SetBool("isMoving", true);

            // �̵� ���⿡ ���� �ִϸ��̼� ����
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
