using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level: MonoBehaviour
{

    [SerializeField] AudioClip clickSound;
    [SerializeField] GameObject scoreText;

    StartMenuDesign startMenuDesign;

    GameSession gameSession;
    OuterFrame outerFrame;
    ShooterImage shooterImage;


    // Use this for initialization
    public void Start()
    {
        outerFrame = FindObjectOfType<OuterFrame>();
        gameSession = FindObjectOfType<GameSession>();
        shooterImage = FindObjectOfType<ShooterImage>();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void MoveFrameAndLoadGame()
    {
        AudioSource.PlayClipAtPoint
            (clickSound, Camera.main.transform.position);
        outerFrame.MoveFrame();
        if (FindObjectOfType<ScoreDisplay>())
        {
            Destroy(scoreText.gameObject, 0.1f);
        }
        FindObjectOfType<StartButton>().DestroySelf();
        FindObjectOfType<QuitButton>().DestroySelf();
        shooterImage.MoveImage();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
        
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadGame()
    {
        gameSession.ResetGame();
        SceneManager.LoadScene("Game");
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}