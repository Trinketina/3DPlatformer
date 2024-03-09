using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] Image health;
    [SerializeField] TMP_Text score;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        health.fillAmount = (float)stats.Health / (float)stats.MaxHealth;
        score.text = stats.Score.ToString();
    }
}
