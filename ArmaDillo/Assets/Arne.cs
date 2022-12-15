using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arne : MonoBehaviour
{
    [SerializeField] float health = 100f;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    bool hasNotDied = false;
    // Update is called once per frame
    void Update()
    {
        if(health <= 1 && !hasNotDied)
        {
            hasNotDied = true;
            anim.SetTrigger("Death");
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
