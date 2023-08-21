using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIControl
{
    public class SceneController : MonoBehaviour
    { 
        public void SceneSwitch(string sceneName)
        {
            SceneManager.LoadScene(sceneName);      // TODO: Make async switching
        }
    }
}
