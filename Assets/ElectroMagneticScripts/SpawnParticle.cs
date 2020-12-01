using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticle : MonoBehaviour
{
    public int buttonID = 0;
    public GameObject positiveParticle;
    public GameObject negativeParticle;

    private static bool spawnPositive = true;

    private GameObject hapticDevice = null;   //!< Reference to the GameObject representing the Haptic Device
    private bool buttonStatus = false;
    private bool spherePlaced = false;

    // Start is called before the first frame update
    void Start()
    {
        if (hapticDevice == null)
        {

            HapticPlugin[] HPs = (HapticPlugin[])Object.FindObjectsOfType(typeof(HapticPlugin));
            foreach (HapticPlugin HP in HPs)
            {
                if (HP.hapticManipulator == this.gameObject)
                {
                    hapticDevice = HP.gameObject;
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

        spawnPositive = GameObject.Find("SimulationController").GetComponent<SimulationController>().spawnPositive;
        bool buttonStatus = hapticDevice.GetComponent<HapticPlugin>().Buttons[buttonID] == 1;


        if (buttonStatus == true && !spherePlaced)
        {
            GameObject particleToSpawn;

            if(spawnPositive)
            {
                particleToSpawn = positiveParticle;
            }
            else
            {
                particleToSpawn = negativeParticle;
            }

            Vector3 position = this.transform.position;
            Instantiate(particleToSpawn, position, Quaternion.identity);
            spherePlaced = true;
        } 
        else if (buttonStatus == false && spherePlaced)
        {
            spherePlaced = false;
        }
    }
}
