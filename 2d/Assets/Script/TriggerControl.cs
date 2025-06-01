using UnityEngine;

public class TriggerControl : MonoBehaviour
{

    [SerializeField] GameObject Player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("trigger girdi");
        Player.GetComponent<PlayerController>().onGround = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Debug.Log("trigger çýktý");
        Player.GetComponent<PlayerController>().onGround = false;
    }
}
