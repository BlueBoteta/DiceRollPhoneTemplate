using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float targetOrthoSize = 10f;
    [SerializeField] private float zoomDuration    = 2f;
    [SerializeField] private float followSpeed     = 4f;
    [SerializeField] private Vector3 targetPosition = new Vector3(0f, 0f, -10f);

    private Camera           cam;
    private Transform        player;
    private bool             isFollowing;

    private void Awake() => cam = GetComponent<Camera>();

    private void Start()
    {
        var pc = FindFirstObjectByType<PlayerController>();
        if (pc != null) player = pc.transform;

        Vector3 startPos = player != null
            ? new Vector3(player.position.x, player.position.y, -10f)
            : targetPosition;

        transform.position   = startPos;
        cam.orthographicSize = targetOrthoSize * 0.05f;

        StartCoroutine(ZoomOutIntro(startPos));
    }

    private void LateUpdate()
    {
        if (!isFollowing || player == null) return;

        Vector3 target = new Vector3(player.position.x, player.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, target, followSpeed * Time.deltaTime);
    }

    private IEnumerator ZoomOutIntro(Vector3 startPos)
    {
        float startSize = targetOrthoSize * 0.05f;
        float elapsed   = 0f;

        while (elapsed < zoomDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / zoomDuration);
            cam.orthographicSize = Mathf.Lerp(startSize, targetOrthoSize, t);
            transform.position   = Vector3.Lerp(startPos, targetPosition, t);
            yield return null;
        }

        cam.orthographicSize = targetOrthoSize;
        transform.position   = targetPosition;
        isFollowing          = true;
    }
}
