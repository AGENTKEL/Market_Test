using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Truck : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fruitsCount;
    
    private bool isBeingHeld = false;
    
    private int currentCount = 0;
    private int maxCount = 4;

    void Start()
    {
        UpdateItemCountUI(); // Initialize text as 0/4
    }

    public void IncrementCount()
    {
        if (currentCount < maxCount)
        {
            currentCount++;
            UpdateItemCountUI();
        }
    }

    private void UpdateItemCountUI()
    {
        fruitsCount.text = $"{currentCount}/{maxCount}";
    }
}
