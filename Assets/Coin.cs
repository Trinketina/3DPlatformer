using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] int scoreValue = 1;
    [SerializeField] GameObject parent;

    bool collected = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!collected)
        {
            other.GetComponent<Player>().AddToScore(scoreValue);
            anim.SetBool("Collected", true);
        }
        collected = true;
        //add to score, then
        //run animation, and animation has a scheduled event that calls DisableCoin() at the end
    }

    public void DisableCoin()
    {
        parent.SetActive(false);
    }
}
