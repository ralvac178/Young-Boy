using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField] private HasMove hasMove;
    // Start is called before the first frame update
    void Awake()
    {
        inputManager = new InputManager();
    }

    private void OnEnable()
    {
        inputManager.Enable();
        inputManager.Player.Jump.performed += _ => Debug.Log("Jumping");                 
    }

    private void OnDisable()
    {
        inputManager.Player.Jump.performed -= _ => Debug.Log("Jumping");
        inputManager.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        hasMove.OnHasMove(inputManager.Player.HorMove.ReadValue<float>());
    }
}
