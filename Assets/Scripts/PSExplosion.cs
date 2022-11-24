using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSExplosion : MonoBehaviour
{
    public GameObject explosion;
    public GameObject ufo;

    private GameObject _ps;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("UFO"))
        {
            _ps = Instantiate(explosion, collision.gameObject.transform.position, ufo.transform.rotation);
            Destroy(_ps, 1.5f);
        }
    }
}
