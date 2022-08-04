using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lod1AnimLog : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = "1x Ship part \n" + PlayerPrefs.GetInt("stavbaLodiJedna").ToString() + " / 40"; 
    }
}
