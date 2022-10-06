using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Rigidbody playerRb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private JoystickScript joystick;
    private float accelerationSpeed = 10f;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerRb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = playerInputActions.Kart.Move.ReadValue<Vector2>();
        Move(moveDirection);

        if(joystick.inputDir != Vector2.zero)
        {
            moveDirection = joystick.inputDir;
            Move(moveDirection);
        }
    }

    private void Move(Vector2 direction)
    {
        playerRb.velocity = new Vector3(direction.x * moveSpeed * Time.fixedDeltaTime, 0f, direction.y * moveSpeed * Time.fixedDeltaTime);
        if (playerInputActions.Kart.Accelerate.IsPressed())
        {
            playerRb.AddRelativeForce(Vector3.forward * accelerationSpeed, ForceMode.Impulse);
        }
    }
}
