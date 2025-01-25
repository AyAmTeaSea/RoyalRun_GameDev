using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float minFOV = 30f;
    [SerializeField] private float maxFOV = 120f;
    [SerializeField] private float zoomDuration = 0.5f;
    [SerializeField] private float zoomModifier = 5f;
    [SerializeField] ParticleSystem speedUpParticleSystem;

    private CinemachineCamera cinemachineCamera;

    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(speedAmount));

        if (speedAmount > 0)
        {
            speedUpParticleSystem.Play();
        }
    }

    IEnumerator ChangeFOVRoutine(float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV + (speedAmount * zoomModifier), minFOV, maxFOV);
        
        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;
            
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, elapsedTime / zoomDuration);
            yield return null;
        }
        
        cinemachineCamera.Lens.FieldOfView = targetFOV;
    }
}
