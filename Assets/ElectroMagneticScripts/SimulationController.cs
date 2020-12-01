using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationController : MonoBehaviour
{
    public bool play = true;
    public bool spawnPositive = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            play = !play;
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            spawnPositive = true;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            spawnPositive = false;
        }
    }
}
