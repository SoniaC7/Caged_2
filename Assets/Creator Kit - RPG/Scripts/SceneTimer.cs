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
    public GameObject[] canvas;
    public Image black_image;

    public KeyCode interactKey; //Lletra per canviar d'escena encara que falti temps

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

        timerText.text = "NEXT SCENE" + "\n" + mins.ToString("00") + ":" + secs.ToString("00"); //2 zeros to show only 2 digits

    }
    private void updatePausedTimer()
    {
        timerText.text = ""; //timer GUI not shown
    }

    void Start()
    {
        total_scenes = 4;

        player_spawns = new Vector3[total_scenes];
        player_spawns[0] = new Vector3(-0.5f, 11f, 0f); //Cell
        player_spawns[1] = new Vector3(5.5f, -3.5f, 0f); //Visit room
        player_spawns[2] = new Vector3(2f, 21f, 0f); //Playground
        player_spawns[3] = new Vector3(-5.5f, -5f, 0f); //Economato

        //will set the incoming max times
        scene_time = new float[total_scenes];
        scene_time[0] = 120.0f;
        scene_time[1] = 300.0f;
        scene_time[2] = 300.0f;
        scene_time[3] = 300.0f;

        npc_collection = GameObject.FindGameObjectsWithTag("npc");  //get all npcs
        player = GameObject.FindGameObjectsWithTag("Player")[0];    //only there is one, the player
        black_image = GameObject.FindGameObjectsWithTag("image")[0].GetComponent<Image>();
        black_image.canvasRenderer.SetAlpha(0.0f);

        timerText = GameObject.FindGameObjectsWithTag("timer")[0].GetComponent<Text>();
        updateTimer();
    }
    
    public IEnumerator WaitAndFade()
    {
        black_image.CrossFadeAlpha(1,0.2f,false);
        yield return new WaitForSeconds(1f);
        black_image.CrossFadeAlpha(0,0.2f,false);
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
        
        for (int i = 0; i < 16; i++) //test for all the NPCs
        {
            np = npc_collection[i].GetComponent<NPCController>();
            if (np.conv_start == true)  //if Player is chatting with somebody
            {
                chatting = true;
            }
        }

       
        if ( (this.time >= this.scene_time[this.scene_index] && !chatting) || Input.GetKeyDown(interactKey)) //if arrives at the maximum scene time change the "scene" (player's room)
        {

            int next_room = scene_index + 1;
            this.time = 0.0f; //reset time
            scene_index = (scene_index + 1) % total_scenes;
            if (next_room == 4) VisitedRoom(); //Si ja hem anat a la sala de visites la eliminem del bucle
            
            StartCoroutine(WaitAndFade());

            player.transform.position = player_spawns[scene_index];//update player position

            Debug.Log(player_spawns[scene_index]);
            Debug.Log("La escena ha cambiado a la numero "+ scene_index );

        }
          

    }

    public void VisitedRoom()
    {
        total_scenes = 3;

        player_spawns = new Vector3[total_scenes];
        player_spawns[0] = new Vector3(-0.5f, 11f, 0f); //Cell
        player_spawns[1] = new Vector3(2f, 21f, 0f); //Playground
        player_spawns[2] = new Vector3(-5.5f, -5f, 0f); //Economato
    }
}
