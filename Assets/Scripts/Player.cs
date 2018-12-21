using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour {

    // configuration parameters
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float xPaddingOfPlayer = 0.8f;
    [SerializeField] float yPaddingOfPlayer = 0.5f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    [SerializeField] int health = 500;

    [Header("Projectile")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = 1f;
    float newBulletXPos;
    float newBulletYPos;
    Vector2 newBulletPos;

    [Header("VFX")]
    [Header("SFX")]
    [SerializeField] AudioClip shoot;
    [SerializeField] [Range(0f, 1f)] float shootVolume = 0.25f;
    [SerializeField] AudioClip killed;
    [SerializeField] [Range(0f, 1f)] float killedVolume = 0.5f;
    Coroutine fireCoroutine;
    [SerializeField] bool invulnerable;
    // Use this for initialization
    void Start () {
        
        SetUpMoveBoundaries();

    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x 
            + xPaddingOfPlayer;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x 
            - xPaddingOfPlayer;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y 
            + yPaddingOfPlayer;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y 
            - yPaddingOfPlayer;
    }

    // Update is called once per frame
    void Update () {
        Fire();
        Move();
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!invulnerable)
        {
            DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die ();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint
            (killed, Camera.main.transform.position, killedVolume);
        FindObjectOfType<Level>().LoadGameOver();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }

    }
    IEnumerator FireContinuously()
    {
        while (true) 
        {
            newBulletXPos = transform.position.x;
            newBulletYPos = transform.position.y + yPaddingOfPlayer;
            newBulletPos = new Vector2(newBulletXPos, newBulletYPos);

            GameObject bullet = Instantiate
                (bulletPrefab, newBulletPos, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = 
                new Vector2(0f, projectileSpeed);
            AudioSource.PlayClipAtPoint
                (shoot, Camera.main.transform.position, shootVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);

    }

    public int GetHealth()
    {
        return health;
    }
}
