using System.Collections;
using System.Collections.Generic;
using UIControl;
using UnityEngine;
using TMPro;

namespace UIController
{
    public class UIController : MonoBehaviour
    {
        [Header("Scene setup")]
        [SerializeField] private SceneController sceneController;

        public void OnConnectDialogClick(GameObject panel)
        {
            panel.SetActive(!panel.activeInHierarchy);
        }

        public void OnConnectButtonClick(TMP_InputField input)
        {
            string ip_addr = input.text;
            Debug.Log(ip_addr);
        }

        public void OnCreateButtonClick()
        {
            // Lobby creation
        }
    }
}
