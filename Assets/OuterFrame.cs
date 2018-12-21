using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterFrame : MonoBehaviour {


    [SerializeField] float frameTargetPosY = 550f;
    [SerializeField] float FrameMoveSpeed = 40f;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void MoveFrame()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, FrameMoveSpeed);
    }
}