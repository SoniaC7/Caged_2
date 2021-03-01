using RPGM.Core;
using RPGM.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPGM.UI
{
    /// <summary>
    /// Sends user input to the correct control systems.
    /// </summary>
    public class InputController : MonoBehaviour
    {
        public float stepSize = 0.1f;
        GameModel model = Schedule.GetModel<GameModel>();
        public List<Cell_Walls> walls;

        public enum State
        {
            CharacterControl,
            DialogControl,
            Pause
        }

        State state;

        public void ChangeState(State state) => this.state = state;

        void Update()
        {
            switch (state)
            {
                case State.CharacterControl:
                    CharacterControl();
                    break;
                case State.DialogControl:
                    DialogControl();
                    break;
            }
        }

        void DialogControl()
        {
            model.player.nextMoveCommand = Vector3.zero;
            if (Input.GetKeyDown(KeyCode.A))
                model.dialog.FocusButton(-1);
            else if (Input.GetKeyDown(KeyCode.D))
                model.dialog.FocusButton(+1);
            if (Input.GetKeyDown(KeyCode.Return))
                model.dialog.SelectActiveButton();
        }

        void CharacterControl()
        {
            //CompareWall();
            bool[] collision_walls = CompareWalls();
            
            if (Input.GetKey(KeyCode.W) && !collision_walls[0])
                model.player.nextMoveCommand = Vector3.up * stepSize;
            else if (Input.GetKey(KeyCode.S) && !collision_walls[3])
                model.player.nextMoveCommand = Vector3.down * stepSize;
            else if (Input.GetKey(KeyCode.A) && !collision_walls[1])
                model.player.nextMoveCommand = Vector3.left * stepSize;
            else if (Input.GetKey(KeyCode.D) && !collision_walls[2])
                model.player.nextMoveCommand = Vector3.right * stepSize;
            else
                model.player.nextMoveCommand = Vector3.zero;
        }

        public List<bool> AddWalls()
        {
            List<bool> total_walls = new List<bool>();
            //Debug.Log("Walls length:" + walls.Count);

            for (int i = 0; i < walls.Count ; i++)
            {
                total_walls.Add(walls[i].is_North_Wall);
                //Debug.Log("total_Walls " + i + " " + walls[i].is_North_Wall);

                total_walls.Add(walls[i].is_West_Wall);
                //Debug.Log("total_Walls " + i + " " + walls[i].is_West_Wall);

                total_walls.Add( walls[i].is_East_Wall);
                //Debug.Log("total_Walls " + i + " " + walls[i].is_East_Wall);

                total_walls.Add(walls[i].is_South_Wall);
                //Debug.Log("total_Walls " + i + " " + walls[i].is_South_Wall);
            }
            
            return total_walls;
        }
        
        public bool[] CompareWalls()
        {
            List<bool> total_walls = AddWalls();
            bool [] collision_walls = new bool[4];
            //Debug.Log("total_Walls lengt:" + total_walls.Count);

            for (int i = 0; i < total_walls.Count ; i += 4)
            {
                if (total_walls[i]) collision_walls[0] = true;
                if (total_walls[i + 1]) collision_walls[1] = true;
                if (total_walls[i + 2]) collision_walls[2] = true;
                if (total_walls[i + 3]) collision_walls[3] = true;
            }

            return collision_walls;
        }
    }
}