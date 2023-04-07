using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Ability/ShootAbility")]
public class ShootAbility : Ability
{
    [SerializeField] private GameObject _bullet;
    public override void Use(PlayerMovement player)
    {
        Instantiate(_bullet, player.FireballFirePoint.transform.position, player.transform.rotation);
    }
}