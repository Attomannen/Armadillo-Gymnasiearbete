using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GenerateStems : MonoBehaviour
{
    [SerializeField] GameObject stem;
    
    [SerializeField] Vector3 offset;
    [SerializeField] int amount = 220;
    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Generate()
    {
        for (var i = 0; i < amount; i++)
        {
            Instantiate(stem, new Vector3(i * 4.75f + offset.x, transform.localPosition.y + offset.y, transform.localPosition.z + offset.z), Quaternion.identity, gameObject.transform);

        }
        for (var i = 0; i < amount; i++)
        {
            Instantiate(stem, new Vector3(i * -4.75f + -offset.x - 8.2f, transform.localPosition.y + offset.y, transform.localPosition.z + -offset.z), Quaternion.identity, gameObject.transform);

        }
        for (var i = 0; i < amount; i++)
        {
            Instantiate(stem, new Vector3(transform.localPosition.x + -offset.x, transform.localPosition.y + offset.y, i * -4.75f + offset.z), Quaternion.Euler(0,90,0), gameObject.transform);

        }
        for (var i = 0; i < amount; i++)
        {
            Instantiate(stem, new Vector3(transform.localPosition.x + -499f, transform.localPosition.y + offset.y, i * -4.75f + offset.z), Quaternion.Euler(0, 90, 0), gameObject.transform);

        }
    }
}
