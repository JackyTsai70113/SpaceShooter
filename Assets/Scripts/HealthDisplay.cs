using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour{
    
    Player player;
    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<TextMeshPro>().text = "HEALTH\n" + player.GetHealth().ToString();
        
    }

}