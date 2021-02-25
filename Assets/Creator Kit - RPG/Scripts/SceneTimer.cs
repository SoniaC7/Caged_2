using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneTimer : MonoBehaviour //asigned to user to change its spawn positions depending on scene
{
    public float time = 0.0f;         //time passing by
    public float scene_time = 10.0f; //maximum scene time (10s to try it)***
    public int total_scenes = 3;
    public int scene_index  = 0;    //actual scene

    private Vector3[] player_spawns;


    public void increaseTime (float amount_time)
    {
        this.time += amount_time;
    }

    void Start()
    {
        player_spawns = new Vector3[total_scenes];
        player_spawns[0] = new Vector3(-0.5f, 11f, 0f);
        player_spawns[1] = new Vector3(10f, 14f, 0f);
        player_spawns[2] = new Vector3(0f, 0f, 0f);

        //gameObject.transform.position = player_spawns[0]; //set first player position
    }

    void Update()
    {
        this.time += Time.deltaTime;        

        if (this.time >= this.scene_time) //if arrives at the maximum scene time change the "scene" (player's room)
        {
            this.time = 0.0f; //reset time
            scene_index = (scene_index + 1) % total_scenes;
            gameObject.transform.position = player_spawns[scene_index]; //update player position
            Debug.Log("La escena ha cambiado a la numero "+ scene_index );
        }
    }
}

//gameObject en minuscula se refiere al objeto al que este script esta asignado
//podemos utilizar esto si por ejemplo quisieramos mover NPCs pero dandoles arrays de spawn diferentes