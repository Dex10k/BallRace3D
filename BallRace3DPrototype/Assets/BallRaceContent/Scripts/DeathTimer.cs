using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{

    public float LifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, LifeSpan);
    }
}
