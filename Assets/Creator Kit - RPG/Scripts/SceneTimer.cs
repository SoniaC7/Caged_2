using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGM.Gameplay; //namespace to use characterController2D


public class SceneTimer : MonoBehaviour
{
    public float time = 0.0f;         //time passing by
    public float scene_time = 5.0f; //maximum scene time (5s to try it)***
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
        player_spawns[1] = new Vector3(11f, 15f, 0f);
        player_spawns[2] = new Vector3(0f, 0f, 0f);
    }

    void Update()
    {
        this.time += Time.deltaTime;        

        if (this.time >= this.scene_time) //if arrives at the maximum scene time change the "scene" (player's room)
        {
            //CharacterController2D.start = player_spawns[scene_index]; //***update player position
            this.time = 0.0f; //reset time
            scene_index = (scene_index + 1) % total_scenes;
        }
    }
}
