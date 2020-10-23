using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyUI : MonoBehaviour
{
    public GameObject energy1;
    public GameObject energy2;
    public GameObject energy3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateEnergyUI(int energy)
    {
        if (energy == 3)
        {
            energy1.SetActive(true);
            energy2.SetActive(true);
            energy3.SetActive(true);
        }
        if (energy == 2)
        {
            energy1.SetActive(true);
            energy2.SetActive(true);
            energy3.SetActive(false);
        }
        if (energy == 1)
        {
            energy1.SetActive(true);
            energy2.SetActive(false);
            energy3.SetActive(false);
        }
        if (energy == 0)
        {
            energy1.SetActive(false);
            energy2.SetActive(false);
            energy3.SetActive(false);
        }
    }
}
