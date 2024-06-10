using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * 60, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Instantiate(effect,transform.position,transform.rotation);
        Debug.Log(collision.gameObject.name);
        collision.gameObject.GetComponentInParent<IDamageable>()?.AddDamage(1);
    }
}
