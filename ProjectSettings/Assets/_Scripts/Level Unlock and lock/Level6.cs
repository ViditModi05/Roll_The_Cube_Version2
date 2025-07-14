using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6 : MonoBehaviour
{

 public GameObject lockupDice2 , unlockupDice2 , lockLeftDice4 , unlockLeftDice4 , sparkleDice2Blue , Sparkledice4Green;
    // Start is called before the first frame update
    void Start()
    {
      lockupDice2.SetActive(true);
      unlockupDice2.SetActive(false);
      unlockLeftDice4.SetActive(false);
      lockLeftDice4.SetActive(true);
      sparkleDice2Blue.SetActive(false);
      Sparkledice4Green.SetActive(false);
    }

    public void LockLvl6Dice4()
    {
      Sparkledice4Green.SetActive(true);
      StartCoroutine(Enableleft());
    }
    public void lockDice2Lvl6()
    {
      sparkleDice2Blue.SetActive(true);
      StartCoroutine(EnableUp());
    }
       IEnumerator EnableUp()
        {
           yield return new WaitForSeconds(2f);
           lockupDice2.SetActive(false);
           unlockupDice2.SetActive(true);

        }
             IEnumerator Enableleft()
        {
           yield return new WaitForSeconds(2f);
      unlockLeftDice4.SetActive(true);
      lockLeftDice4.SetActive(false);

        }
}
