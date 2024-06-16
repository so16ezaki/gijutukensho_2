using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] float speed = 60;
    [SerializeField] GameObject effect;
    [SerializeField] int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up *speed, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Instantiate(effect,transform.position,transform.rotation);
        collision.gameObject.GetComponentInParent<IDamageable>()?.AddDamage(damage);
    }
}
