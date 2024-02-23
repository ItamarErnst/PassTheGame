using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public MovementController movement_controller;
    public List<GameObject> patrol_list;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        
        int count = 0;
        while (movement_controller)
        {
            movement_controller.MoveTo(patrol_list[count++].transform.position);
            
            yield return new WaitForSeconds(8f);
            
            if (count >= patrol_list.Count)
            {
                count = 0;
            }
        }
    }
}
