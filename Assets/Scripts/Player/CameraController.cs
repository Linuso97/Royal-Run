using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] ParticleSystem speedupParticleSystem;

    [Header("Camera settings for speed up and down")]
    [SerializeField] float minFOV = 20f;
    [SerializeField] float maxFOV = 120f;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] float zoomSpeedModifier = 5f;

    CinemachineVirtualCamera cinemachineCamera;
    LevelGenerator levelGen;

    [HideInInspector]
    public bool isChangingFOV;

    private void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();

        cinemachineCamera.m_Lens.FieldOfView = 60f;
    }

    public void ChangeCameraFOV(float speedAmount)
    {
        if (isChangingFOV) { return; }
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(speedAmount));

        if (speedAmount > 0)
        {
            speedupParticleSystem.Play();
        }
    }

    IEnumerator ChangeFOVRoutine(float speedAmount)
    {
        isChangingFOV = true;

        float startFOV = cinemachineCamera.m_Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV + speedAmount * zoomSpeedModifier, minFOV, maxFOV);

        float elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            elapsedTime += Time.deltaTime;

            cinemachineCamera.m_Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }

        cinemachineCamera.m_Lens.FieldOfView = targetFOV;

        isChangingFOV = false;
    }
}
