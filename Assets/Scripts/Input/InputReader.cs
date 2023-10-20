using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/Input Reader")]
public class InputReader : ScriptableObject, IPlayerActions
{

    public event Action<bool> PrimaryFireEvent;
    public event Action<bool> SecondaryFireEvent;
    public event Action<Vector2> MovementEvent;

    public Vector2 AimPosition {get; private set;}

    private Controls controls;

    private void OnEnable() {
        if(controls == null) // if not setup 
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);
        }

        controls.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementEvent?.Invoke(context.ReadValue<Vector2>()); // On lit la valeur du vecteur de mouvement qu'on envoit dans l'évènement, ? pour qu'il ne soit pas null
    }

    public void OnPrimaryFire(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            PrimaryFireEvent?.Invoke(true);
        }
        else if(context.canceled)
        {
            PrimaryFireEvent?.Invoke(false);
        }
    }

    public void OnSecondaryFire(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            SecondaryFireEvent?.Invoke(true);
        }
        else if(context.canceled)
        {
            SecondaryFireEvent?.Invoke(false);
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        AimPosition = context.ReadValue<Vector2>();
    }
}
