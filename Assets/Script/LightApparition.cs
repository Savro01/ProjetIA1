using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightApparition : MonoBehaviour
{
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float time = 1f;
        if (timer <= time)
            timer += Time.deltaTime;

        if (timer > time)
        {
            int random = Random.Range(0, 2);
            timer = 0;
            if (random < 1)
            {
                Debug.Log("Apparition d'un element");
            }
        }
    }
}
