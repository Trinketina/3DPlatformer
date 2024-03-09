using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] int damageCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        player.Hurt(damageCount);


        Vector3 impulse = collision.impulse*2 + Vector3.up;
        collision.rigidbody.AddForce(impulse, ForceMode.Impulse);
    }
}
