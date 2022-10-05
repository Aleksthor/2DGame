using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{

    public class AIConversant : MonoBehaviour
    {
        private GameObject playerObject;
        private PlayerConversant playerConversant;
        public float interactRange = 3f;

        [SerializeField] Dialogue AIDialogue;

        private void Start()
        {
            playerObject = PlayerSingleton.instance.gameObject;
            playerConversant = PlayerSingleton.instance.GetComponent<PlayerConversant>();
        }
        private void Update()
        {
            if (Vector2.Distance(playerObject.transform.position, transform.position) < interactRange && !playerConversant.isActive())
            {
                playerConversant.StartDialogue(AIDialogue);

            }

            if (Vector2.Distance(playerObject.transform.position, transform.position) > interactRange 
                && playerConversant.GetCurrentDialogue() == AIDialogue)
            {
                playerConversant.Quit();
            }


        }
    }
}