using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private int health = 20;
    public int Health {  get { return health; } set {  health = value<maxHealth ? value: maxHealth ; } }

    [SerializeField] private int maxHealth = 20;
    public int MaxHealth {  get { return maxHealth; } }

    public int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
