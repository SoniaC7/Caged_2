using RPGM.Core;
using RPGM.Gameplay;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTimer : MonoBehaviour //asigned to user to change its spawn positions depending on scene and to the cronometer of the room scene
{
    public float time = 0.0f;           //time passing by
    public bool using_time = true;      //using time, bool controler

    public int total_scenes;
    public int scene_index  = 0;    //actual scene
    public float[] scene_time;      //maximum scene time

    private Vector3[] player_spawns;
    public GameObject player;       //player object

    public GameObject[] npc_collection;
    public NPCController np;

    //variables to define the GUI of the time
    private Text timerText;

    public void increaseTime (float amount_time)
    {
        this.time += amount_time;
    }

    private void updateTimer ()
    {
        int mins = 0;
        int secs = 0;
        int timeRemaining = (int) (this.scene_time[this.scene_index] - time); //get remaining time to secs

        mins = timeRemaining / 60;
        secs = timeRemaining % 60;
        secs = Mathf.Max(secs, 0); //stop at zero

        timerText.text = mins.ToString("00") + ":" + secs.ToString("00"); //2 zeros to show only 2 digits

    }
    private void updatePausedTimer()
    {
        timerText.text = ""; //timer GUI not shown
    }

    void Start()
    {
        total_scenes = 3;

        player_spawns = new Vector3[total_scenes];
        player_spawns[0] = new Vector3(-0.5f, 11f, 0f);
        player_spawns[1] = new Vector3(11f, 16f, 0f);
        player_spawns[2] = new Vector3(10f, -3f, 0f);

        //will set the incoming max times
        scene_time = new float[total_scenes];
        scene_time[0] = 120.0f;
        scene_time[1] = 300.0f;
        scene_time[2] = 300.0f;

        npc_collection = GameObject.FindGameObjectsWithTag("npc");  //get all npcs
        player = GameObject.FindGameObjectsWithTag("Player")[0];    //only there is one, the player

        //time GUI inizialised
        timerText = GameObject.FindGameObjectsWithTag("timer")[0].GetComponent<Text>();
        updateTimer();
    }

    void Update()
    {
        if (using_time)
        {
            this.time += Time.deltaTime;
            updateTimer();
        }
        else
        {
            updatePausedTimer();
        }

        bool chatting = false;

        for (int i = 0; i < npc_collection.Length; i++) //test for all the NPCs
        {
            np = npc_collection[i].GetComponent<NPCController>();
            if (np.conv_start == true)  //if Player is chatting with anyone
            {
                chatting = true;
            }
        }

        if (this.time >= this.scene_time[this.scene_index] && !chatting) //if arrives at the maximum scene time change the "scene" (player's room)
        {
            this.time = 0.0f; //reset time
            scene_index = (scene_index + 1) % total_scenes;
            player.transform.position = player_spawns[scene_index]; //update player position
            Debug.Log(player_spawns[scene_index]);
            Debug.Log("La escena ha cambiado a la numero "+ scene_index );
        }
          
    }
}
