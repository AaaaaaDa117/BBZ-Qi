using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHealthUI(int health)
    {
        if (health == 2)
        {
            heart3.SetActive(false);
        }
        if (health == 1)
        {
            heart3.SetActive(false);
            heart2.SetActive(false);
        }
        if (health == 0)
        {
            heart3.SetActive(false);
            heart2.SetActive(false);
            heart1.SetActive(false);
        }
    }
}
