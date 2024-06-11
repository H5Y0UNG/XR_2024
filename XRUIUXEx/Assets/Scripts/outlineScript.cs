using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outlineScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent.GetComponent<Outline>().enabled = false;
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.transform.tag == "Player")
        {
            Debug.Log("Enter Player");
            outlineOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        if (other.transform.tag == "Player")
        {
            Debug.Log("Exit Player");
            outlineOff();
        }
    }

    public void outlineOn()
    {
        Debug.Log(gameObject.transform.transform.parent.name);
        gameObject.transform.parent.GetComponent<Outline>().enabled = true;
    }

    public void outlineOff()
    {
        gameObject.transform.parent.GetComponent<Outline>().enabled = false;
    }
}
