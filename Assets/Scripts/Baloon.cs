using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite sprite;
    public static event Action<Sprite> BaloonPop;
    public void Interact()
    {
        BaloonPop(sprite);
        Destroy(gameObject);
    }
}