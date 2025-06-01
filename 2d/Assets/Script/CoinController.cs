using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    
    [SerializeField] float coinroteyt;

    private void Update()
    {
        transform.Rotate(new Vector3(0f, coinroteyt, 0f));
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Level Manager").GetComponent<leval_manager>().addScore(50);
            Destroy(gameObject);
        }
    }
}
