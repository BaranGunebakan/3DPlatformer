using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
