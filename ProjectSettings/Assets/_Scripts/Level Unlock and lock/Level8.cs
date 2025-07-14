using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8 : MonoBehaviour
{
public GameObject UnlockRightGreen , LockRightGreen , lockleftGreen , UnlockLeftGreen;
public GameObject lockupYellow , UnlockupYellow , lockRightYellow , UnlockRightYellow , LockleftYellow , UnlockLeftYellow;
public GameObject unlockleftYellow1 , unlockRightYellow1 , unlockUpYellow1 , unlockDownYellow1 , LockLeftYellow1 , LockRightYellow1 , LockupYellow1 , LockDownYellow1;
public GameObject unlockleftYellow2 , unlockRightYellow2 , unlockUpYellow2 , unlockDownYellow2 , LockLeftYellow2 , LockRightYellow2 , LockupYellow2 , LockDownYellow2;
public GameObject unlockleftYellow3 , unlockRightYellow3 , unlockUpYellow3 , unlockDownYellow3 , LockLeftYellow3 , LockRightYellow3 , LockupYellow3 , LockDownYellow3;

public GameObject LockLeftPink , UnlockLeftpink;
public GameObject SparkleBlue , sparkleGreen, SparkleYellow , SparklePink ; 
void Start()
{
    UnlockRightGreen.SetActive(false);
    UnlockLeftGreen.SetActive(false);
    lockleftGreen.SetActive(true);
    LockRightGreen.SetActive(true);

    lockupYellow.SetActive(false);
    UnlockupYellow.SetActive(true);
    lockRightYellow.SetActive(true);
    UnlockRightYellow.SetActive(false);
    LockleftYellow.SetActive(true);
    UnlockLeftYellow.SetActive(false);

    LockupYellow1.SetActive(false);
    unlockUpYellow1.SetActive(true);
    LockRightYellow1.SetActive(true);
    unlockRightYellow1.SetActive(false);
    LockLeftYellow1.SetActive(true);
    unlockleftYellow1.SetActive(false);
    unlockDownYellow1.SetActive(true);
    LockDownYellow1.SetActive(false);

    LockupYellow2.SetActive(false);
    unlockUpYellow2.SetActive(true);
    LockRightYellow2.SetActive(true);
    unlockRightYellow2.SetActive(false);
    LockLeftYellow2.SetActive(true);
    unlockleftYellow2.SetActive(false);
    unlockDownYellow2.SetActive(true);
    LockDownYellow2.SetActive(false);

    LockupYellow3.SetActive(true);
    unlockUpYellow3.SetActive(false);
    LockRightYellow3.SetActive(true);
    unlockRightYellow3.SetActive(false);
    LockLeftYellow3.SetActive(true);
    unlockleftYellow3.SetActive(false);
    unlockDownYellow3.SetActive(true);
    LockDownYellow3.SetActive(false);

    LockLeftPink.SetActive(true);
    UnlockLeftpink.SetActive(false);


    SparkleBlue.SetActive(false);
    sparkleGreen.SetActive(false);
    SparkleYellow.SetActive(false);
    SparklePink.SetActive(false);

}
public void lockDice4Lvl8()
{
    SparkleBlue.SetActive(true);
}
public void lockDice2Lvl8()
{
    sparkleGreen.SetActive(true);
    UnlockRightGreen.SetActive(true);
    UnlockLeftGreen.SetActive(true);
    lockleftGreen.SetActive(false);
    LockRightGreen.SetActive(false);
}
public void lockDice5Lvl8()
{
    SparkleYellow.SetActive(true);
    lockupYellow.SetActive(true);
    UnlockupYellow.SetActive(false);
    lockRightYellow.SetActive(false);
    UnlockRightYellow.SetActive(true);
    LockleftYellow.SetActive(false);
    UnlockLeftYellow.SetActive(true);


    LockupYellow1.SetActive(true);
    unlockUpYellow1.SetActive(false);
    LockRightYellow1.SetActive(false);
    unlockRightYellow1.SetActive(true);
    LockLeftYellow1.SetActive(false);
    unlockleftYellow1.SetActive(true);
    unlockDownYellow1.SetActive(false);
    LockDownYellow1.SetActive(true);

    
     LockupYellow2.SetActive(true);
    unlockUpYellow2.SetActive(false);
    LockRightYellow2.SetActive(false);
    unlockRightYellow2.SetActive(true);
    LockLeftYellow2.SetActive(false);
    unlockleftYellow2.SetActive(true);
    unlockDownYellow2.SetActive(false);
    LockDownYellow2.SetActive(true);

    LockupYellow3.SetActive(true);
    unlockUpYellow3.SetActive(false);
    LockRightYellow3.SetActive(false);
    unlockRightYellow3.SetActive(true);
    LockLeftYellow3.SetActive(false);
    unlockleftYellow3.SetActive(true);
    unlockDownYellow3.SetActive(false);
    LockDownYellow3.SetActive(true);
    
}

public void LockDice3Lvl8()
{
    SparklePink.SetActive(true);
     LockLeftPink.SetActive(false);
    UnlockLeftpink.SetActive(true);

}
}
