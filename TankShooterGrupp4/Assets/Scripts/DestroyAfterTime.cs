using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float destroyTimer = 5;
    private void Awake()
    {

        Destroy(gameObject,destroyTimer);
    }



}
