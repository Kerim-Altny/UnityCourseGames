using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 5;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] bool applyCameraShake;
    [SerializeField] int scorePerHit = 100;
    ScoreKeeper scoreKeeper;
    UIUpdater uiUpdater;

    CameraShake cameraShake;
    AudioManager audioManager;
    LevelManager levelManager;
    void Awake()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        uiUpdater = FindFirstObjectByType<UIUpdater>();
        levelManager = FindFirstObjectByType<LevelManager>();
    }
    void Start()
    {
        
        if (isPlayer && uiUpdater != null)
        {
            uiUpdater.UpdateHealthDisplay(health);
        }
    }
    public int GetCurrentHealth()
    {
        return health;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if we hit a damage dealer

        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitParticles();
            damageDealer.Hit();
            audioManager.PlayDamageSFX();
            if (applyCameraShake)
            {
                cameraShake.Play();
            }
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        
        // Sadece oyuncu hasar aldığında UI güncellensin
        if (isPlayer && uiUpdater != null)
        {
            uiUpdater.UpdateHealthDisplay(health);
        }

        if (health <= 0)
        {
            // Ölüm efekti vs. buraya eklenebilir
            Die();
        }
    }
    void Die()
    {
        if(isPlayer)
        {
            
            levelManager.LoadGameOverScene();
        }
        else
        {
            scoreKeeper.setScore(scorePerHit);
        }

        Destroy(gameObject);
    }

    void PlayHitParticles()
    {
        if (hitParticles != null)
        {
            ParticleSystem particles = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(particles.gameObject, particles.main.duration + particles.main.startLifetime.constantMax);
        }
    }
}

