using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float targetOrthoSize = 10f;
    [SerializeField] private float zoomDuration = 2f;
    [SerializeField] private Vector3 targetPosition = new Vector3(0f, 0f, -10f);

    private Camera cam;

    private void Awake() => cam = GetComponent<Camera>();

    private void Start()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();

        Vector3 startPos = targetPosition;
        if (player != null)
        {
            startPos = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        }

        transform.position = startPos;
        cam.orthographicSize = targetOrthoSize * 0.05f;

        StartCoroutine(ZoomOutIntro(startPos, targetOrthoSize * 0.05f));
    }

    private IEnumerator ZoomOutIntro(Vector3 startPos, float startSize)
    {
        float elapsed = 0f;
        while (elapsed < zoomDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / zoomDuration);
            cam.orthographicSize = Mathf.Lerp(startSize, targetOrthoSize, t);
            transform.position = Vector3.Lerp(startPos, targetPosition, t);
            yield return null;
        }
        cam.orthographicSize = targetOrthoSize;
        transform.position = targetPosition;
    }
}
