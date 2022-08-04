using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lod2AnimLog : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = "1x Ship part \n" + PlayerPrefs.GetInt("stavbaLodiDva").ToString() + " / 80";
    }
}
