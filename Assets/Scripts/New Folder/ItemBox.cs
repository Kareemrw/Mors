using UnityEngine;

public class ItemBox : MonoBehaviour
{
    /*public GameObject player;
    public GameObject river;
    public GameObject trough;*/
    public GameObject bucket;
    public GameObject emptyBucket;
    /*public GameObject filledBucket;

    public bool troughFill;

    [SerializeField] Trough filled;*/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        emptyBucket.SetActive(false);
        //filledBucket.SetActive(false);
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (bucket.active == true)
        {
            PlayerTouchesBucket();
        }
    }
    


    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "River")
        {
                emptyBucket.SetActive(false);
                filledBucket.SetActive(true);
                //Debug.Log("F key pressed");
            
        }
    }*/

    public void PlayerTouchesBucket()
    {
        if(Input.GetKey(KeyCode.F))
        {
            print("enters keycode");
            if(bucket.CompareTag("Bucket"))
            {
                emptyBucket.SetActive(true);
                print("enters 2nd if");
            }
        }
    }

    /*public void PlayerTouchesTrough()
    {
        if(Input.GetKey(KeyCode.F))
        {
            if(trough.tag == "Trough")
            {
                filledBucket.SetActive(false);
                //print("ye");
            }
        }
    }*/
}
