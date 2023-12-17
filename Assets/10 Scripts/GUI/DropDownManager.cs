using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropDownManager : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    private TextMeshProUGUI labelText;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        labelText = dropdown.GetComponentInChildren<TextMeshProUGUI>();

        //Initialize TMPdropdown value by default
        labelText.text = dropdown.options[0].text;
    }

    public void UpdateValue()
    {
        int index = dropdown.value;
        labelText.text = dropdown.options[index].text;
    }

}
