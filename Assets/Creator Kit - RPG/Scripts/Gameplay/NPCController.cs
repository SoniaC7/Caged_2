using RPGM.Core;
using RPGM.Gameplay;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGM.Gameplay
{
    /// <summary>
    /// Main class for implementing NPC game objects.
    /// </summary>
    public class NPCController : MonoBehaviour
    {
        public ConversationScript[] conversations;

        public KeyCode interactKey;

        public bool isInRange;

        public bool displayMessage;

        public bool conv_start = false;

        Quest activeQuest = null;

        Quest[] quests;

        GameModel model = Schedule.GetModel<GameModel>();

        public float hideTextDuration = 2.0f;

        void OnEnable()
        {
            quests = gameObject.GetComponentsInChildren<Quest>();
        }
        
        void Update()
        {
            if (isInRange)
            {
                if (!conv_start)
                {
                    displayMessage = true;
                    StartCoroutine(WaitAndMakeTextDisappear(hideTextDuration));
                }

                if (Input.GetKeyDown(interactKey))
                {
                    Debug.Log("Key pressed");
                    StartConv();
                }
            }
        }
        private IEnumerator WaitAndMakeTextDisappear(float waitTimeInSeconds)
        {
            yield return new WaitForSeconds(1.5f);
            displayMessage = false;
        }

        public void StartConv()
        {
            conv_start = true;
            var c = GetConversation();
                if (c != null)
                {
                    var ev = Schedule.Add<Events.ShowConversation>();
                    ev.conversation = c;
                    ev.npc = this;
                    ev.gameObject = gameObject;
                    ev.conversationItemKey = "";
                }
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
                conv_start = false;
                Debug.Log("Player not in range");
        }
        
        public void OnGUI()
        {
            if (displayMessage)
            {
                GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Apreta E per parlar");
            }
        }

        public void CompleteQuest(Quest q)
        {
            if (activeQuest != q) throw new System.Exception("Completed quest is not the active quest.");
            foreach (var i in activeQuest.requiredItems)
            {
                model.RemoveInventoryItem(i.item, i.count);
            }
            activeQuest.RewardItemsToPlayer();
            activeQuest.OnFinishQuest();
            activeQuest = null;
        }

        public void StartQuest(Quest q)
        {
            if (activeQuest != null) throw new System.Exception("Only one quest should be active.");
            activeQuest = q;
        }

        ConversationScript GetConversation()
        {
            if (activeQuest == null)
                return conversations[0];
            foreach (var q in quests)
            {
                if (q == activeQuest)
                {
                    if (q.IsQuestComplete())
                    {
                        CompleteQuest(q);
                        return q.questCompletedConversation;
                    }
                    return q.questInProgressConversation;
                }
            }
            return null;
        }
    }
}