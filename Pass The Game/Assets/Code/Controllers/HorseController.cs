using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    public MovementController movement_controller;
    public MovementController player_obj;

    public float speed = 3.5f;
    public float max_distance = 3.5f;
    public float min_distance = 2;

    private void Start()
    {
        movement_controller.SetMovementSpeed(speed);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player_obj.transform.position) >= max_distance)
        {
            if (!movement_controller.IsWalking())
            {
                movement_controller.MoveTo(GetPositionBehindPlayer());
            }
        }
    }

    private Vector3 GetPositionBehindPlayer()
    {
        return player_obj.transform.position + ((player_obj.GetFacingDir() * -1) * min_distance);
    }
}
