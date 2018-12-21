using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Enemy")]
    [SerializeField] float maxHealth = 500;
    [SerializeField] float health = 500;
    [SerializeField] int score = 0;

    [Header("Projectile")]
    [SerializeField] float shotCounter;
    [SerializeField] float shotYSpeedOfEnemy = 10f;
    [SerializeField] float minBetweenShots = 0.2f;
    [SerializeField] float maxBetweenShots = 3f;
    [SerializeField] float yPaddingOfEnemy = 0.5f;
    [SerializeField] GameObject bulletPrefab;
    float newBulletXPos;
    float newBulletYPos;
    Vector2 newBulletPos;

    [Header("VFX")]
    [SerializeField] GameObject deathExplosionPrefab;
    [SerializeField] float durationOfExplosion = 1f;

    [Header("SFX")]
    [SerializeField] AudioClip shooting;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;
    [SerializeField] AudioClip hitted;
    [SerializeField] [Range(0f, 1f)] float hittedVolume = 1f;
    [SerializeField] AudioClip killed;
    [SerializeField] [Range(0f, 1f)] float killedVolume = 1f;
    
    [SerializeField] GameObject healthBarFrame;
    [SerializeField] GameObject healthBar;

    GameSession gameSession;
    void Start () {
        gameSession = FindObjectOfType<GameSession>();
        shotCounter = Random.Range(minBetweenShots, maxBetweenShots);
    }

    

    // Update is called once per frame
    void Update ()
    {
        countDownAndShoot();
	}

    private void countDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minBetweenShots, maxBetweenShots);
        }
    }

    private void Fire()
    {
        newBulletXPos = transform.position.x;
        newBulletYPos = transform.position.y - yPaddingOfEnemy;
        newBulletPos = new Vector2(newBulletXPos, newBulletYPos);

        GameObject bullet = Instantiate
            (bulletPrefab, newBulletPos, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -shotYSpeedOfEnemy);

        AudioSource.PlayClipAtPoint
            (shooting, Camera.main.transform.position, shootingVolume);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        DamageDealer damageDealer = collider.gameObject.GetComponent<DamageDealer>();
        
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }


    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        float healthPercent = (float)health / (float)maxHealth;
        if (healthPercent < 1)
        {
            healthBarFrame.SetActive(true);
            healthBar.transform.localScale = new Vector3(healthPercent*0.98f, 0.95f, 1f);
            healthBar.transform.localPosition = new Vector3
                ((1-healthPercent)/0.1f*(-0.4f), 
                healthBar.transform.localPosition.y, 
                healthBar.transform.localPosition.z);
        }

        
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
        else
        {
            AudioSource.PlayClipAtPoint
            (hitted, Camera.main.transform.position, hittedVolume);
        }
    }

    private void Die()
    {
        gameSession.AddScore(score);

        AudioSource.PlayClipAtPoint
            (killed, Camera.main.transform.position, killedVolume);
        GameObject explosion = Instantiate
            (deathExplosionPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(gameObject, 0.2f);
        Destroy(explosion, durationOfExplosion);
    }
}
