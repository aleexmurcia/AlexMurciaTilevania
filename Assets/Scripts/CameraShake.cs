using UnityEngine;
using Unity.Cinemachine;


public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private CinemachineCamera camera;
    private float shakeTimer;

    void Awake()
    {
        Instance = this;
        camera = GetComponent<CinemachineCamera>();
        if (camera == null)
        {
            camera = FindFirstObjectByType<CinemachineCamera>();
        }
    }

    public void Shake(float intensity, float duration)
    {
        if (camera == null)
        {
            camera = FindFirstObjectByType<CinemachineCamera>();
            if (camera == null) return;
        }

        CinemachineBasicMultiChannelPerlin perlin = camera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        if (perlin != null)
        {
            perlin.AmplitudeGain = intensity;
            shakeTimer = duration;
        }
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                if (camera != null)
                {
                    CinemachineBasicMultiChannelPerlin perlin = camera.GetComponent<CinemachineBasicMultiChannelPerlin>();
                    if (perlin != null)
                    {
                        perlin.AmplitudeGain = 0f;
                    }
                }
            }
        }
    }

}
