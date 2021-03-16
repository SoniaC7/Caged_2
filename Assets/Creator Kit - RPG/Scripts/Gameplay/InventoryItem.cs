using RPGM.Core;
using RPGM.Gameplay;
using RPGM.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace RPGM.Gameplay
{
    /// <summary>
    /// Marks a gameObject as a collectable item.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
    public class InventoryItem : MonoBehaviour
    {
        public int count = 1;
        public Sprite sprite;
        
        public KeyCode interactKey;
        public bool isInRange;
        public GameObject[] item_collec;
        public Manage_items manage_it;
        public bool displaymessage;
        public float hideTextDuration = 2.0f;
        public bool pedra = false;
        
        GameModel model = Schedule.GetModel<GameModel>();
        
        void Start()
        {
            item_collec = GameObject.FindGameObjectsWithTag("items");
            manage_it = item_collec[0].GetComponent<Manage_items>();
        }

        void Reset()
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
        }

        void OnEnable()
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
        }

        void Update()
        {
            if (isInRange)
            {
                if (manage_it.firstTime)
                {
                    displaymessage = true;
                    StartCoroutine(WaitAndMakeTextDisappear(hideTextDuration));
                }

                if (Input.GetKeyDown(interactKey))
                {
                    MessageBar.Show($"You collected: {name} x {count}");
                    
                    if(this.CompareTag("pedra")){
                        pedra = true;
                        Debug.Log("Dins de update" + pedra);
                        //OnGUI();
                        StartCoroutine(WaitAndMakeTextDisappear(hideTextDuration));
                    } 

                    model.AddInventoryItem(this);
                    UserInterfaceAudio.OnCollect();
                    gameObject.SetActive(false);
                    Debug.Log("Key pressed");                    
                }
            }
        }

        private IEnumerator WaitAndMakeTextDisappear(float waitTimeInSeconds)
        {
            yield return new WaitForSeconds(1.5f);
            displaymessage = false;
            pedra = false;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                isInRange = true;
                Debug.Log("Player in range");
            }

        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            isInRange = false;
            Debug.Log("Player not in range");
            manage_it.firstTime = false;

        }

        public void OnGUI()
        {
            if (displaymessage)
            {
                GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Apreta E per agafar");
            }
            
            Debug.Log("Dins de ONGUI" + pedra);
            
            if(pedra)
            {
                Debug.Log(pedra);
                GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Ja em puc tallar");
                Debug.Log("pedra");
            }
        }
    }
}