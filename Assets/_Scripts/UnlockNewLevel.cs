using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockNewLevel : MonoBehaviour
{
    [SerializeField] private AddressableSampleArray addressableSampleArray;

    private void UnLock()
    {
        if (addressableSampleArray.currentLevel >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", addressableSampleArray.currentLevel +1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

}
