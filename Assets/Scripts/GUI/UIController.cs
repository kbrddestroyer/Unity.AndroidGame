using System.Collections;
using System.Collections.Generic;
using UIControl;
using UnityEngine;
using TMPro;
using NetworkController;

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
            CustomNetworkManager.Instance.ChangeDestination(ip_addr);       // Change server IP to connect to

            CustomNetworkManager.Instance.StartClient();
        }

        public void OnCreateButtonClick(string sceneName)
        {
            //sceneController.SceneSwitch(sceneName);
            CustomNetworkManager.Instance.StartHost();
        }
    }
}
