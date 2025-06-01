using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private bool onGround; // ongrond yerine onGround
    [SerializeField] private LayerMask engel;
    [SerializeField] float speed;
    private float width;
    private Rigidbody2D myBody;
    private static int  totalenemy;
    private void Start()
    {
        totalenemy++;
        Debug.Log(totalenemy);
        width = GetComponent<SpriteRenderer>().bounds.extents.x;
        myBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 rayOrigin = transform.position + (transform.right * width); // I��n ba�lang�� noktas� d�zeltildi
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 2f, engel);

        onGround = hit.collider != null;

        if (!onGround)
        {
            transform.eulerAngles += new Vector3(0, 180f, 0);
        }

        myBody.linearVelocity = new Vector2(transform.right.x * speed, 0); // velocity hatas� d�zeltildi
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 rayOrigin = transform.position + (transform.right * width);
        Gizmos.DrawLine(rayOrigin, rayOrigin + new Vector3(0, -2f, 0)); // Parantez hatas� d�zeltildi ve y�n d�zeltildi
    }
}
