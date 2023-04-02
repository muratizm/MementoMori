using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 currentCheckpoint;
    private CharacterController2D controller;
    private Animator animator;

    private float horizontalInput;
    public float runSpeed = 40.0f;

    private bool jump =false;
    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        transform.position = GameObject.FindGameObjectWithTag("StartPoint").transform.position;
        currentCheckpoint = transform.position;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            TeleportCheckpoint();
        }

    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalInput * Time.fixedDeltaTime * runSpeed, false, jump);
        jump = false;   
    }

    public void TeleportCheckpoint()
    {
        transform.position = currentCheckpoint;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
