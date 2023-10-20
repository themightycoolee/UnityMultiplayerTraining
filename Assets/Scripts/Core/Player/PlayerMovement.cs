using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Rigidbody2D rb;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float turningRate = 270f;

    private Vector2 previousMovementInput;

    public override void OnNetworkSpawn()
    {
        if(!IsOwner) {
            return;
        }
        print("hehehe");
        inputReader.MovementEvent += HandleMove;
        inputReader.PrimaryFireEvent += HandleDefault;
        inputReader.SecondaryFireEvent += HandleDefault;
        print("setup");
    }

    public override void OnNetworkDespawn()
    {
        if(!IsOwner) {
            return;
        }
        print("destroy");
        inputReader.MovementEvent -= HandleMove;
        inputReader.PrimaryFireEvent -= HandleDefault;
        inputReader.SecondaryFireEvent -= HandleDefault;
    }

    private void HandleMove(Vector2 movementInput) {
        previousMovementInput = movementInput;
    }

    private void Update()
    {
        if(!IsOwner) {
            return;
        }

        float zRotation = previousMovementInput.x * -turningRate * Time.deltaTime; // deltaTime to avoid frame dependency 
        bodyTransform.Rotate(0f, 0f, zRotation);
    }

    private void FixedUpdate() { // Physically move the object, since its rigidBody it is recommended in this function to avoid desync (called everyframe of PHYSICS engine)
        if(!IsOwner) {
            return;
        }

        rb.velocity = (Vector2)bodyTransform.up * previousMovementInput.y * movementSpeed;
    }

    private void HandleDefault(bool test) {
        print(test?"button pressed":"button released");
    }
}
