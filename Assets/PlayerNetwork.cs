using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<Vector2> direction = new NetworkVariable<Vector2>(new(), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    void Update()
    {
        if (!IsOwner) return;

        Vector2 movement = new();

        if (Input.GetKey(KeyCode.W)) movement.y = +1f;
        if (Input.GetKey(KeyCode.S)) movement.y = -1f;
        if (Input.GetKey(KeyCode.A)) movement.x = -1f;
        if (Input.GetKey(KeyCode.D)) movement.x = +1f;

        if (direction.Value != movement)
            direction.Value = movement;
    }

    private void FixedUpdate()
    {
        if (!IsServer) return;

        float moveSpeed = 3f;
        transform.position += moveSpeed * Time.deltaTime * new Vector3(direction.Value.x, 0, direction.Value.y);
    }
}
