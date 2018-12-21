using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour {
    [SerializeField] int score = 0;

	// Use this for initialization
	void Awake ()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update () {
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public void StartTheGame()
    {
        score = 0;
    } 

}