using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAiming : NetworkBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform turretTransform;

    private void LateUpdate() { // Avoid jitter
        if(!IsOwner) {
            return;
        }

        turretTransform.up = (Vector2)(Camera.main.ScreenToWorldPoint(inputReader.AimPosition) - turretTransform.position); // Cast vector2, align turret up vector with turret-cursor vector
    }
}
