using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    public int timeOfApparition = 0;
    float timer = 0;

    private void Update()
    {
        float time = 1f;
        if (timer <= time)
            timer += Time.deltaTime;

        if (timer > time)
        {
            timeOfApparition++;
            timer = 0;
        }
    }
}
