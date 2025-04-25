using UnityEngine;
using System.Collections;

public class SmartFloatingUI : MonoBehaviour
{
    [Header("Tracking Settings")]
    public Transform cameraTransform; 
    public float distanceFromCamera = 1.5f;
    public Vector3 canvasOffset = new Vector3(0, -0.2f, 0);
    public float recenterAngleThreshold = 30f; 
    public float recenterCooldown = 1.0f;

    [Header("Smoothing")]
    public float smoothDuration = 0.5f;

    private float cooldownTimer = 0f;
    private bool isRecentering = false;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f && !isRecentering)
        {
            cooldownTimer = recenterCooldown;

            Vector3 toUI = transform.position - cameraTransform.position;
            Vector3 flatToUI = new Vector3(toUI.x, 0, toUI.z).normalized;
            Vector3 flatForward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;

            float angle = Vector3.Angle(flatForward, flatToUI);

            if (angle > recenterAngleThreshold)
            {
                RecenterSmooth();
            }
        }
    }

    private void RecenterSmooth()
    {
        Vector3 forward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;
        Vector3 targetPosition = cameraTransform.position + forward * distanceFromCamera + canvasOffset;
        Quaternion targetRotation = Quaternion.LookRotation(forward);

        StopAllCoroutines();
        StartCoroutine(SmoothRecenter(targetPosition, targetRotation));
    }

    private IEnumerator SmoothRecenter(Vector3 targetPosition, Quaternion targetRotation)
    {
        isRecentering = true;

        float t = 0f;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        while (t < 1f)
        {
            t += Time.deltaTime / smoothDuration;
            transform.position = Vector3.Lerp(startPos, targetPosition, t);
            transform.rotation = Quaternion.Slerp(startRot, targetRotation, t);
            yield return null;
        }

        isRecentering = false;
    }
}
