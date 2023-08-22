using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace NetworkController
{
    public class CustomNetworkManager : NetworkManager
    {
        private static CustomNetworkManager instance;

        public static CustomNetworkManager Instance { get => instance; }

        public override void Awake()
        {
            if (instance == null) instance = this;
            base.Awake();
        }

        public void ChangeDestination(string destination)
        {
            this.networkAddress = destination;
        }
    }
}
