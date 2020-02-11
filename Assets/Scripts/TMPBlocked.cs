using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMPBlocked : MonoBehaviour
{
    public void DisableObject() { gameObject.SetActive(false); } // called by TMPBlocked animation event
}