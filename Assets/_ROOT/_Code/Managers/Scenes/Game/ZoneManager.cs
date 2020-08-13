using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ZoneManager : MonoBehaviour
{
    public E_ZoneType zoneType;
    public int zoneTierValue;
    private int zoneTierMaxValue = 5;

    [Header("UI References")]
    public List<GameObject> zoneDotsList;

    [Header("Zone Status List")]
    public List<GameObject> tierGroupsList;


    #region [-----     BEHAVIOURS     -----]

    public void InitZoneTier(int p_index)
    {
        zoneTierValue = p_index;

        ModifyZoneTierUI();
        ModifyZoneVisuals();
    }

    public void ModifyZoneTier(int p_index)
    {
        zoneTierValue += p_index;

        if (zoneTierValue <= 0)
            zoneTierValue = 0;
        if (zoneTierValue >= zoneTierMaxValue)
            zoneTierValue = zoneTierMaxValue;

        ModifyZoneTierUI();
        ModifyZoneVisuals();
    }

    private void ModifyZoneVisuals()
    {
        for (int i = 0; i < tierGroupsList.Count; i++)
            tierGroupsList[i].SetActive(false);

        if(zoneTierValue != 0)
            tierGroupsList[zoneTierValue - 1].SetActive(true);
        else if (zoneTierValue == 0)
            tierGroupsList[0].SetActive(true); // Always show the tier 1 when zoneTier Reaches 0
    }

    #endregion

    #region [-----     USER INTERFACE     -----]

    private void ModifyZoneTierUI()
    {
        for (int i = 0; i < zoneDotsList.Count; i++)
            zoneDotsList[i].SetActive(false);

        if (zoneTierValue == 0)
            return;

        for (int i = 0; i < zoneTierValue; i++)
            zoneDotsList[i].SetActive(true);
    }

    #endregion
}
