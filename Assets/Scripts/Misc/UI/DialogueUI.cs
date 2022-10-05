using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using UnityEngine.UI;

namespace UI
{
    public class DialogueUI : MonoBehaviour
    {

        PlayerConversant playerConversant;

        [SerializeField] TMPro.TextMeshProUGUI AIText;
        [SerializeField] Button nextButton;
        [SerializeField] Transform choiceRoot;
        [SerializeField] GameObject AIResponse;
        [SerializeField] GameObject choicePrefab;
        [SerializeField] Button quitButton;

        // Start is called before the first frame update
        void Start()
        {
            playerConversant = PlayerSingleton.instance.GetComponent<PlayerConversant>();
            GameEvents.current.OnConversationUpdated += UpdateUI;
            nextButton.onClick.AddListener(Next);
            quitButton.onClick.AddListener(Quit);
            UpdateUI();
        }


        void Next()
        {
            playerConversant.Next();
        }
        void Quit()
        {
            playerConversant.Quit();
        }
        private void UpdateUI()
        {
            gameObject.SetActive(playerConversant.isActive());
            if(!playerConversant.isActive())
            {
                return;
            }


            AIResponse.SetActive(!playerConversant.IsChoosing());

            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());

            // Destroy choices
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }


            // Choise's or AIReponse
            if (playerConversant.IsChoosing())
            {
                foreach (DialogueNode node in playerConversant.GetChoices())
                {
                    GameObject choice = Instantiate(choicePrefab, choiceRoot);
                    choice.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = node.GetText();
                    Button button = choice.GetComponentInChildren<Button>();
                    button.onClick.AddListener(() =>
                    {
                        playerConversant.SelectChoice(node);
                    });
                }
            }
            else
            {
                AIText.text = playerConversant.GetText();
                nextButton.gameObject.SetActive(playerConversant.HasNext());
            }

            


           
        }
    }
}
