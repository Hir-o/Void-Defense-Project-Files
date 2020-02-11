using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretController : MonoBehaviour
{
    private void Awake() { UpdateTurretStats(); }

    public abstract void UpdateTurretStats();
    
    public abstract void Overcharge();
}