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

        // Start is called before the first frame update
        void Start()
        {
            playerConversant = PlayerSingleton.instance.GetComponent<PlayerConversant>();
            nextButton.onClick.AddListener(Next);

            UpdateUI();
        }


        void Next()
        {
            playerConversant.Next();
            UpdateUI();
        }
        private void UpdateUI()
        {
            AIText.text = playerConversant.GetText();
            nextButton.gameObject.SetActive(playerConversant.HasNext());
        }
    }
}
