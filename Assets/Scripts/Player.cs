using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController inputActions { get; private set; }
    public PlayerMovement movement { get; private set; }
    //public PlayerAim aim { get; private set; }
    public PlayerCamera cam { get; private set; }

    private void Awake()
    {
        inputActions = new PlayerController();
        movement = GetComponent<PlayerMovement>();
        //aim = GetComponent<PlayerAim>();
        cam = GetComponent<PlayerCamera>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}
