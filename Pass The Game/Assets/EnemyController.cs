using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Hero hero;
    public List<GameObject> patrol_list;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        
        int count = 0;
        while (hero)
        {
            hero.OnClickGround(patrol_list[count++].transform.position);
            
            yield return new WaitForSeconds(8f);
            
            if (count >= patrol_list.Count)
            {
                count = 0;
            }
        }
    }
}
