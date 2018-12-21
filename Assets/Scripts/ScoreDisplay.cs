using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour{

    GameSession gameSession;
    // Use this for initialization
    void Start () {
        gameSession = FindObjectOfType<GameSession>();
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<TextMeshPro>().text = "SCORE\n" + gameSession.GetScore().ToString();
    }

}