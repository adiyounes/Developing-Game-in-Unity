using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffects : MonoBehaviour
{
    // Start is called before the first frame update
    public static SpecialEffects specialEffects { get; private set; }
    private GameObject smokeVFX;

    private void Awake()
    {
        if (specialEffects != null && specialEffects != this)
        {
            Destroy(this);
        }
        else
        {
            specialEffects = this;
        }
    }

    private void Start()
    {
        smokeVFX = Resources.Load<GameObject>("SmokeVFX");
    }

    public void CreateSmoke(Transform _transform)
    {
        GameObject smoke = Instantiate(smokeVFX, _transform.position, Quaternion.identity);
        
        Destroy(smoke.gameObject, smoke.GetComponent<ParticleSystem>().main.duration);
    }
}
