using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinnemachineController : MonoBehaviour
{
    [SerializeField] CinemachineTargetGroup group;
    [SerializeField] CinemachineVirtualCamera cinemachineCameraMain;
    public List<GameObject> players;


    
    void Update()
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

        CompareTag("Player");
        for (int i = 0; i < players.Count; i++)
        {
            group.m_Targets[i].target = players[i].transform;
        }
    }
}
