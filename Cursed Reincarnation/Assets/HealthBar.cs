using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float healthlength = 280;
    public GameObject healthBar;
    public bool recoverHealth;
    public float weakSpell = 0.1f;
    public float strongSpell = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SimulateHealth());
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(healthlength, 10);

        if(recoverHealth == true)
        {
            if(healthlength >= 280)
            {
                recoverHealth = false;
            }
        
        else
        {
            healthlength += strongSpell;
        }

        }
    }

    IEnumerator SimulateHealth(){
        yield return new WaitForSeconds(5);
        healthlength -= 30;
        yield return new WaitForSeconds(2);
        healthlength -= 20;
        yield return new WaitForSeconds(3);
        healthlength -= 40;
        yield return new WaitForSeconds(3);
        recoverHealth = true;

    }
}
