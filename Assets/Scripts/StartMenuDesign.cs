using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuDesign : MonoBehaviour {

    [SerializeField] GameObject outerFrame;
    [SerializeField] float targetPosY = 400f;
    [SerializeField] float moveSpeed = 40f;
    [SerializeField] GameObject startButton;
    [SerializeField] bool ifStart = false;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ifStart)
        {
            MoveOuterFrame();
            DisableStartButton();
        }
    }

    public void MoveOuterFrame()
    {
        var targetPosition = new Vector2(transform.position.x, targetPosY);
        var movementThisFrame = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards
            (transform.position, targetPosition, movementThisFrame);
    }

    public void DisableStartButton()
    {
        startButton.SetActive(false);
    }
    public void StartGame()
    {
        ifStart = true;
    }

}