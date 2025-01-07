using Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ParticleSystem collisionParticleSystem;
    [SerializeField] AudioSource audioSource;

    [Header("Modifiers")]
    [Tooltip("Modifies screenshake on rock collision")]
    [SerializeField] float shakeModifier = 10f;
    [SerializeField] float volumeModifier = 5f;
    [SerializeField] float collisionCooldown = 1f;

    CinemachineImpulseSource cinemachineImpulseSource;

    float collisionTimer = 1f;

    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        collisionTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collisionTimer < collisionCooldown) { return; }

        FireImpulse();
        CollisionFX(collision);
        collisionTimer = 0f;
    }

    void FireImpulse()
    {
        float shakeIntensity = CalculateDistance(shakeModifier);
        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    void CollisionFX(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        collisionParticleSystem.transform.position = contactPoint.point;
        collisionParticleSystem.Play();

        RockImpactSound();
    }

    void RockImpactSound()
    {
        audioSource.volume = CalculateDistance(volumeModifier);
        audioSource.Play();

    }

    float CalculateDistance(float modifier)
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float intensity = 1f / distance * modifier;
        intensity = Mathf.Min(intensity, 1f);
        return intensity;
    }
}
