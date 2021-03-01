using RPGM.Core;
using RPGM.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell_Walls : MonoBehaviour
{
    public bool is_South_Wall = false;
    public bool is_West_Wall = false;
    public bool is_East_Wall = false;
    public bool is_North_Wall = false;

    public void SouthWall_trigger(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            is_South_Wall = true;
        }
    }
    
    public void WestWall_trigger(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            is_West_Wall = true;
        }
    }
    
    public void EastWall_trigger(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            is_East_Wall = true;
        }
    }
    
    public void NorthWall_trigger(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            is_North_Wall = true;
        }
    }

    public void Reset_values()
    {
        is_South_Wall = false;
        is_West_Wall = false;
        is_East_Wall = false;
        is_North_Wall = false;
    }
}
