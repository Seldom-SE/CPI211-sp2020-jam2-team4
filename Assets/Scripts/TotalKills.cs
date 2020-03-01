using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalKills : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = "Total Kills: " + PlayerController.savedScore;
    }
}
