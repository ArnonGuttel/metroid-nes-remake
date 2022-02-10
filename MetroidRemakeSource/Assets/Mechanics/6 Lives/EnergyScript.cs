using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnergyScript : MonoBehaviour
{
  public TextMeshProUGUI playerEnergy;
  private int currentEnergy = 100;

  private void Start()
  {
    playerEnergy.text = currentEnergy.ToString();
  }

  private void OnMouseDown()
  {
    currentEnergy -= 8;
    if (currentEnergy <= 0)
    {
      currentEnergy = 0;
      print("Player Dead");
      Destroy(gameObject);
    }
    playerEnergy.text = currentEnergy.ToString();
  }
}
