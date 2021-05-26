using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePrefab : MonoBehaviour
{
    private void Start()
    {
        GetComponent<ParticleSystem>().Emit(2);

        Destroy(gameObject, GetComponent<ParticleSystem>().startLifetime);
    }
}
