using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Player player;
    PlayerController controller;

    [Header("Camera Settings")]
    Transform cam;
    public Transform camTarget;
    public Vector2 mouseInput;

    public float xAxis;
    public float yAxis;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        cam = Camera.main.transform;

        InputAction();
    }

    private void Update()
    {

    }


    private void CameraRotation()
    {
        xAxis += mouseInput.x;
        yAxis += mouseInput.y;
        yAxis = Mathf.Clamp(yAxis, -30, 30);
    }

    private void LateUpdate()
    {
        CameraRotation();
        camTarget.localEulerAngles = new Vector2(-yAxis, camTarget.localEulerAngles.y);
        transform.eulerAngles = new Vector2(transform.eulerAngles.x, xAxis);

    }

    private void InputAction()
    {
        controller = player.inputActions;

        controller.Player.Aim.performed += ctx => mouseInput = ctx.ReadValue<Vector2>();
        controller.Player.Aim.canceled += ctx => mouseInput = Vector2.zero;


    }
}
