using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    [SerializeField] GameObject effect;
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!(collision.gameObject.tag == "Player"))
        {
            Destroy(gameObject);
            if(collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject);
                GameObject.Find("Level Manager").GetComponent<leval_manager>().addScore(100);
                Instantiate(effect, collision.transform.position, Quaternion.identity);
            }
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
