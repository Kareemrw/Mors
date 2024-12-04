using UnityEngine;

public class ItemBox : MonoBehaviour
{
    //public GameObject player;
    public GameObject river;
    public GameObject trough;
    public GameObject bucket;
    public GameObject emptyBucket;
    public GameObject filledBucket;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        emptyBucket.SetActive(false);
        filledBucket.SetActive(false);
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

    /*private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.otherCollider.tag == "River")
        {
            emptyBucket.SetActive(false);
            filledBucket.SetActive(true);
            //Debug.Log("F key pressed");
        }
    }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "River")
        {
                emptyBucket.SetActive(false);
                filledBucket.SetActive(true);
                //Debug.Log("F key pressed");
            
        }
    }


    public void PlayerTouchesBucket()
    {
        if(Input.GetKey(KeyCode.F))
        {
            print("enters keycode");
            if(bucket.tag == "Bucket")
            {
                emptyBucket.SetActive(true);
                print("enters 2nd if");
            }
        }
    }

    public void PlayerTouchesTrough()
    {
        if(Input.GetKey(KeyCode.F))
        {
            if(trough.tag == "Trough")
            {
                filledBucket.SetActive(false);
                //print("ye");
            }
        }
    }

    /*private void OnTriggerExit2D(Collider2D other)
    {
        print("enters ontrigger");
        if (other.CompareTag("River"))
        {
            emptyBucket.SetActive(false);
                filledBucket.SetActive(true);
                //Debug.Log("F key pressed");
            
        }
    }*/
}
