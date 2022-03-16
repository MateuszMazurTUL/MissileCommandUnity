using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public delegate void thisDelegate(GameObject obj);
    public event thisDelegate notify;

    //if meteor hit home
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Meteor"))
        {
            this.destroy();
        }
    }

    //then destroy and notify observers
    internal void destroy()
    {
        gameObject.SetActive(false);
        if (notify != null)
        {
            notify(this.gameObject);
        }
    }
}
