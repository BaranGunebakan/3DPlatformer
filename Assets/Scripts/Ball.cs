using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject Player;
    public void Interact()
    {
        GetComponent<Rigidbody>().AddForce(Player.transform.forward * 20, ForceMode.Impulse);
    }
}
