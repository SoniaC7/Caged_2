using RPGM.Core;
using RPGM.Gameplay;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTimer : MonoBehaviour //asigned to user to change its spawn positions depending on scene
{
    public float time = 0.0f;           //time passing by
    public bool using_time = true;      //using time, bool controler

    public int total_scenes;
    public int scene_index  = 0;    //actual scene
    public float[] scene_time;      //maximum scene time
    private Vector3[] player_spawns;

    public GameObject[] npc_collec ;
    public GameObject npc ;
    public NPCController np;

    public void increaseTime (float amount_time)
    {
        this.time += amount_time;
    }

    void Start()
    {
        total_scenes = 3;

        player_spawns = new Vector3[total_scenes];
        player_spawns[0] = new Vector3(-0.5f, 11f, 0f);
        player_spawns[1] = new Vector3(10f, 14f, 0f);
        player_spawns[2] = new Vector3(10f, -3f, 0f);

        //will set the incoming max times
        scene_time = new float[total_scenes];
        scene_time[0] = 10.0f;
        scene_time[1] = 10.0f;
        scene_time[2] = 10.0f;

        npc_collec = GameObject.FindGameObjectsWithTag("npc_collection");

      
        //gameObject.transform.position = player_spawns[0]; //set first player position
    }

    void Update()
    {
        if (using_time)
            this.time += Time.deltaTime;        

        foreach(GameObject npc in npc_collec)
        {
            np = npc.GetComponent<NPCController>();

            if (this.time >= this.scene_time[this.scene_index] && np.conv_start == false) //if arrives at the maximum scene time change the "scene" (player's room)
            {
                this.time = 0.0f; //reset time
                scene_index = (scene_index + 1) % total_scenes;
                gameObject.transform.position = player_spawns[scene_index]; //update player position
                Debug.Log("La escena ha cambiado a la numero "+ scene_index );
            }
        }
        
          
    }
}

//gameObject en minuscula se refiere al objeto al que este script esta asignado
//podemos utilizar esto si por ejemplo quisieramos mover NPCs pero dandoles arrays de spawn diferentes