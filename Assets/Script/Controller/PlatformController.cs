using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private bool _damage;
#pragma warning restore 0649
    public bool damage => _damage;
}
