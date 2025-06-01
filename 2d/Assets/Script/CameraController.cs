using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerTransform;
    [SerializeField] float max,min;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(playerTransform.position.x, min, max), transform.position.y, transform.position.z);
    }
}
