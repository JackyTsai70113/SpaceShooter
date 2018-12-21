using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShooterImage : MonoBehaviour
{
    [SerializeField] float shootermoveSpeed = -10f;
    [SerializeField] float imageTargetPosY = -10f;

    GameSession gameSession;
    Level level;
    // Use this for initialization
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        level = FindObjectOfType<Level>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= imageTargetPosY)
        {
            GetComponent<Rigidbody2D>().velocity =
                new Vector2(0f, 0f);
            gameSession.StartTheGame();
            SceneManager.LoadScene("Game");
        }
    }

    public void MoveImage()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, shootermoveSpeed);
    }
}