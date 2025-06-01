using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private float myspeedx;
    private Rigidbody2D mybody;
    private Vector3 defultlcoalscale;
    public bool onGround;
    private bool candoublejump;
    private Animator MyAnimator;
    [SerializeField] float curretattectedtime;
    [SerializeField] float defualtattectedtimer;
    [SerializeField] float speed;
    [SerializeField] float jump;
    [SerializeField] GameObject arrow;
    [SerializeField] float arrowspeedx;
    [SerializeField] float arrowspeedy;
    [SerializeField] bool attected;
    [SerializeField] int arrownumber;
    [SerializeField] Text arrownumbertext;
    [SerializeField] AudioClip diemusic;
    [SerializeField] GameObject winpanel;
    [SerializeField] GameObject losepanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arrownumbertext.text = arrownumber.ToString();
        MyAnimator = GetComponent<Animator>();
        attected = false;
        mybody = GetComponent<Rigidbody2D>();
        defultlcoalscale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Input.GetAxis("Horizontal"));
        myspeedx = Input.GetAxis("Horizontal");
        MyAnimator.SetFloat("speed", Mathf.Abs(myspeedx));
        mybody.linearVelocity = new Vector2(myspeedx * speed, mybody.linearVelocity.y);

        #region player hareket
        if (myspeedx > 0)
        {
            transform.localScale = new Vector3(defultlcoalscale.x, defultlcoalscale.y, defultlcoalscale.z);
        }
        else if (myspeedx < 0)
        {
            transform.localScale = new Vector3(-defultlcoalscale.x, defultlcoalscale.y, defultlcoalscale.z);
        }
        #endregion 

        #region zýplama hareketleri
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Debug.Log("boþluk tuþuna basýldý");
            if (onGround == true)
            {
                mybody.linearVelocity = new Vector2(mybody.linearVelocity.x, jump);
                candoublejump = true;
                MyAnimator.SetTrigger("Jump");
            }
            else
            {
                if (candoublejump == true)
                {
                    mybody.linearVelocity = new Vector2(mybody.linearVelocity.x, jump);
                    candoublejump = false;
                }
            }

        }
        #endregion


        #region player ok atma kontrol 
        if (Input.GetMouseButtonDown(0)&& arrownumber >0)
        {
            if(attected == false)
            {
                attected = true;
                MyAnimator.SetTrigger("Attact");
                Invoke("Fire", 0.5f);
                
            }
            
        }
        #endregion
        if(attected== true)
        {
            curretattectedtime -= Time.deltaTime;

        }
        else
        {
            curretattectedtime = defualtattectedtimer;

        }
        if(curretattectedtime <= 0)
        {
            attected = false;
        }
        
    }
    void Fire()
    {
        GameObject okumuz = Instantiate(arrow, transform.position, Quaternion.identity);
        okumuz.transform.parent = GameObject.Find("Arrows").transform;
        if (transform.localScale.x > 0)
        {
            okumuz.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(arrowspeedx, arrowspeedy);
        }
        else
        {
            Vector3 okscale = okumuz.transform.localScale;
            okumuz.transform.localScale = new Vector3(-okscale.x, okscale.y, okscale.z);
            okumuz.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-arrowspeedx, arrowspeedy);
        }
        arrownumber--;
        arrownumbertext.text = arrownumber.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "diken")
        {
            // Debug.Log("deneme");
            Die();
        }
        else if(collision.gameObject.tag =="Finish")
        {
            Destroy(collision.gameObject);
            StartCoroutine(Wait(true));
           // winpanel.SetActive(true);
        }
    }


    public void Die()
    {
        GameObject.Find("Sounds Controller").GetComponent<AudioSource>().clip = null;
        GameObject.Find("Sounds Controller").GetComponent<AudioSource>().PlayOneShot(diemusic);
        MyAnimator.SetTrigger("Die");
        MyAnimator.SetFloat("speed", 0);
        mybody.constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
        StartCoroutine(Wait(false));
        // losepanel.SetActive(true);
    }
    IEnumerator Wait(bool win)
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0;
        if (win==true)
        {
            winpanel.SetActive(true);
        }
        else
        {
            losepanel.SetActive(true);
        }
    }
}
