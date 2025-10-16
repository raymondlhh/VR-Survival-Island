using UnityEngine;
using UnityEngine.UI;

public class EnergyBarController : MonoBehaviour
{
    public Image energyBar;
    public float maxEnergy = 100f;
    public float currentEnergy = 100f;
    public float energyDecreaseAmount = 10f;
    public float energyIncreaseAmount = 20f;

    void Start()
    {
        currentEnergy = maxEnergy;
        Debug.Log($"Energy initialized: {currentEnergy}");
        UpdateEnergyBar();
    }

    public void DecreaseEnergy()
    {
        currentEnergy -= energyDecreaseAmount;
        if (currentEnergy < 0)
        {
            currentEnergy = 0;
        }
        UpdateEnergyBar();
    }

    public void IncreaseEnergy()
    {
        currentEnergy += energyIncreaseAmount;
        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        UpdateEnergyBar();
    }

    public void UpdateEnergyBar()
    {
        energyBar.fillAmount = currentEnergy / maxEnergy;
        Debug.Log($"Energy Bar Updated: Fill Amount = {energyBar.fillAmount}");
    }

    //void OnGrab(GameObject grabbedObject)
    //{
    //    if (grabbedObject.CompareTag("Wood"))
    //    {
    //        Debug.Log("Wood object grabbed. Decreasing energy by " + energyDecreaseAmount);
    //        DecreaseEnergy(energyDecreaseAmount);
    //    }
    //    else if (grabbedObject.CompareTag("Food"))
    //    {
    //        IncreaseEnergy(energyIncreaseAmount);
    //    }
    //}
}