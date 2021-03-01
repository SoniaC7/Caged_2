using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EastWall : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.GetComponentInParent<Cell_Walls>().EastWall_trigger(collision);

    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponentInParent<Cell_Walls>().Reset_values();

    }
}
