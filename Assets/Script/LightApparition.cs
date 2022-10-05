using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightApparition : MonoBehaviour
{
    float timer = 0;
    List<GameObject> lightVisible = new List<GameObject>();
    GameObject[] lights;
    Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("light");
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
        }
        grid = GetComponent<Grid>();
        grid.CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        float time = 1f;
        if (timer <= time)
            timer += Time.deltaTime;

        if (timer > time)
        {
            int random = Random.Range(0, 5);

            timer = 0;
            if (random < 1)
            {
                makeLightAppear(0);
            }
            if(lightVisible.Count > 0)
            {
                for(int i = 0; i < lightVisible.Count; i++)
                {
                    if (lightVisible[i].GetComponent<Light>().timeOfApparition > 3)
                    {
                        lightVisible[i].SetActive(false);
                        grid.CreateGrid();
                        lightVisible.RemoveAt(i);
                    }
                }
            }
        }
    }

    void makeLightAppear(int limite)
    {
        if (limite < 5)
        {
            if (lights.Length > 0)
            {
                int randomLight = Random.Range(1, lights.Length);
                GameObject lightToAppear = lights[randomLight];
                if (lightVisible.Contains(lightToAppear))
                {
                    makeLightAppear(limite + 1);
                }
                else
                {
                    lightToAppear.SetActive(true);
                    grid.CreateGrid();
                    lightVisible.Add(lightToAppear);
                }
                   
            }
        }
    }
}
