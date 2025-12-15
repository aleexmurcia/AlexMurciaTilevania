using UnityEngine;

public class CameraCinematic : MonoBehaviour
{
    public Transform player;       // jugador
    public Vector3 startOffset;    // offset inicial respecto al jugador
    public float targetSize = 10f; // tamaño final de cámara
    public float duration = 3f;    // duración de la cinemática

    Camera cam;
    float startSize;
    Vector3 startPosition;
    Vector3 targetPosition;
    bool cinematicDone = false;

    void Start()
    {
        cam = GetComponent<Camera>();
        startSize = cam.orthographicSize;
        startPosition = player.position + startOffset;

        // Calcula el centro del mapa (puede ser un objeto vacío)
        targetPosition = Vector3.zero; // Cambia por el centro real de tu mapa
        transform.position = startPosition;

        // Inicia la cinemática
        StartCoroutine(PlayCinematic());
    }

    System.Collections.IEnumerator PlayCinematic()
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            // Posición suavizada
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // Tamaño de cámara suavizado
            cam.orthographicSize = Mathf.Lerp(startSize, targetSize, t);

            yield return null;
        }

        // Asegurar que quede exactamente en el objetivo
        transform.position = targetPosition;
        cam.orthographicSize = targetSize;

        cinematicDone = true;

        // Aquí puedes activar el script de seguimiento del jugador
        // ejemplo: GetComponent<CameraFollow>().enabled = true;
    }
}
