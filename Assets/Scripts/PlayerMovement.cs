using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Player player;
    PlayerController controller;
    CharacterController characterController;
    private Animator anim;

    [SerializeField] private float speed;
    private Vector3 moveDir;
    public Vector2 moveInput;

    private float verticalVelocity;


    private void Awake()
    {
        player = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        InputActions();
    }
    private void Update()
    {
        Movement();
        // Rotation();
        AnimationController();
    }
    private void AnimationController()
    {
        float xVelocity = Vector3.Dot(moveDir.normalized, transform.right);
        float zVelocity = Vector3.Dot(moveDir.normalized, transform.forward);

        anim.SetFloat("xVelocity", xVelocity, 0.1f, Time.deltaTime);
        anim.SetFloat("zVelocity", zVelocity, 0.1f, Time.deltaTime);
    }

    private void Movement()
    {
        // moveDir = new Vector3(moveInput.x, 0, moveInput.y);
        // Gravity();
        // if (moveDir.magnitude > 0)
        // {
        //     characterController.Move(moveDir * speed * Time.deltaTime);
        // }

        moveDir = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        moveDir = transform.TransformDirection(moveDir);

        if (moveDir.magnitude >= 0.1f)
        {  
            Vector3 movement = moveDir * speed;
            characterController.Move(movement * Time.deltaTime);
        }
    }

    /* private void Rotation()
     {
         Vector3 lookRotation = player.aim.GetMousePosition().point - transform.position;
         lookRotation.y = 0;
         lookRotation.Normalize();

         Quaternion desiredRotation = Quaternion.LookRotation(lookRotation);
         transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.1f);
     }*/

    private void Gravity()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity -= 9.81f * Time.deltaTime;
            moveDir.y = verticalVelocity;
        }
        else
        {
            verticalVelocity = -.5f;
        }
    }
    private void InputActions()
    {
        controller = player.inputActions;

        controller.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controller.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }
}