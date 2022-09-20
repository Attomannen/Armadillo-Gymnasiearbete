using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class DebuggerForSceneLoading : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.qKey.isPressed)
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }
}