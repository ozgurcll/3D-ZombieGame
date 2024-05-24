using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Player player;
    PlayerController controller;

    [SerializeField] private Transform aim;

    [SerializeField] private LayerMask aimlayerMask;

    private Vector2 mouseInput;
    private RaycastHit lastKnownMouseHit;

    private void Start()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {
        InputAction();
    }

    public RaycastHit GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(mouseInput);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimlayerMask))
        {
            lastKnownMouseHit = hitInfo;
            return hitInfo;
        }

        return lastKnownMouseHit;
    }

    private void InputAction()
    {
        controller = player.inputActions;

        controller.Player.Aim.performed += ctx => mouseInput = ctx.ReadValue<Vector2>();
        controller.Player.Aim.canceled += ctx => mouseInput = Vector2.zero;
    }

}
