using JetBrains.Annotations;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public float gravityscale;
    [Header("Lateral Movement")]
    //public float maxFallSpeedE;
    public float maxSpeedE;
    public float runAccelE;
    public float runDecelE;
    public float directionChangeInt = 4f;


}
