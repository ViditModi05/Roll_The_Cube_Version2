using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level7 : MonoBehaviour
{
    public GameObject UnlockupDice3 , lockupDice3 ,Unlockup1Dice3 , lockup1Dice3 ,sparkleDice3Blue;
    public GameObject UnlockupDice2 , lockupDice2 ,sparkleDice2Green;
    public GameObject UnlockupDice5 , lockupDice5 ,UnlockRightDice5 , lockRightDice5, lockLeft1Dice5 , unlockLeft1Dice5 , sparkleDice5Yellow;

    // Start is called before the first frame update
    void Start()
    {
        Unlockup1Dice3.SetActive(false);
        UnlockupDice3.SetActive(false);
        lockup1Dice3.SetActive(true);
        lockupDice3.SetActive(true);  
        sparkleDice3Blue.SetActive(false); 
        UnlockupDice2.SetActive(false);
        lockupDice2.SetActive(true);
        sparkleDice2Green.SetActive(false);

        UnlockupDice5.SetActive(true);
        lockupDice5.SetActive(false);
        UnlockRightDice5.SetActive(false);
        lockRightDice5.SetActive(true);

        lockLeft1Dice5.SetActive(true);
        unlockLeft1Dice5.SetActive(false);
        sparkleDice5Yellow.SetActive(false);

    }
    public void LockDice3()
    {
        sparkleDice3Blue.SetActive(true); 
        StartCoroutine(UnlockupDice03());
    }
     public void LockDice2()
    {
        sparkleDice2Green.SetActive(true); 
        StartCoroutine(UnlockupDice02());
    }
    public void LockDice5()
    {
        sparkleDice5Yellow.SetActive(true);
        StartCoroutine(UnlockDice05());

    }

          IEnumerator UnlockupDice03()
        {
            yield return new WaitForSeconds(2f);
          Unlockup1Dice3.SetActive(true);
        UnlockupDice3.SetActive(true);
        lockup1Dice3.SetActive(false);
        lockupDice3.SetActive(false);  
        }
               IEnumerator UnlockupDice02()
        {
            yield return new WaitForSeconds(2f);
        lockupDice2.SetActive(false);
        UnlockupDice2.SetActive(true);

         UnlockupDice5.SetActive(false);
        lockupDice5.SetActive(true);
        UnlockRightDice5.SetActive(true);
        lockRightDice5.SetActive(false);

        lockLeft1Dice5.SetActive(false);
        unlockLeft1Dice5.SetActive(true);
        }
                    IEnumerator UnlockDice05()
        {
            yield return new WaitForSeconds(2f);
         UnlockupDice5.SetActive(true);
        lockupDice5.SetActive(false);
        }

}
