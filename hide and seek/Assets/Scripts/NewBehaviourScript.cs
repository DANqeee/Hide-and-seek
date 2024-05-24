using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;


public class NewBehaviourScript : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text= (1 / Time.deltaTime).ToString();
    }
}
