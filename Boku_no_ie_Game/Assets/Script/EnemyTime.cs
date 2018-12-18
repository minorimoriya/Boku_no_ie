using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTime : MonoBehaviour
{

    public GameObject enemy;


    void Update()
    {
        Debug.Log("Enemy");
        Invoke("Enemy", 10f);


    }

    void Enemy()
    {

        if (GlobalParameters.kakureta == false)
        {
            enemy.SetActive(true);
        
        }
        else if (GlobalParameters.kakureta == true)
        {
            enemy.SetActive(false);
        }

    }

   
}// Update is called once per frame
    



