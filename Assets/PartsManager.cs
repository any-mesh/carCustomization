using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // For UI elements like Buttons

public class PartsManager : MonoBehaviour
{
    public PartType[] partTypes;
    public GameObject[] carParts;  // Array to hold all the car parts to change color
    public Button[] colorButtons;  // Array of buttons for color selection

    // Assign the colors to the buttons (you can assign these in the Inspector too)
    public Color[] colors;  // List of colors you want to assign to the car parts

    void Start()
    {
        SetAllParts();

        // Assigning button functions dynamically
        for (int i = 0; i < colorButtons.Length; i++)
        {
            int index = i;  // Local copy of index for the button click handler
            colorButtons[i].onClick.AddListener(() => OnColorButtonClick(colors[index]));
        }
    }

    public PartType GetPartType(string partName)
    {
        for (int i = 0; i < partTypes.Length; i++)
        {
            if (partTypes[i].partName == partName)
            {
                return partTypes[i];
            }
        }
        Debug.LogWarning("Part name could not be found");
        return null;
    }

    public void SetPartFromName(string partName, int select)
    {
        PartType tempPart = GetPartType(partName);
        if (tempPart == null) return;
        tempPart.selected = select;
        PlayerPrefs.SetInt("Parts1" + tempPart.partName, select);
        for (int i = 0; i < tempPart.parts.Length; i++)
        {
            foreach (GameObject gb in tempPart.parts[i].partsObjects)
            {
                if (i == select)
                {
                    gb.SetActive(true);
                }
                else
                {
                    gb.SetActive(false);
                }
            }
        }
    }

    public void SetPartFromId(PartType partType, int select)
    {
        partType.selected = select;
        PlayerPrefs.SetInt("Parts1" + partType.partName, select);
        for (int i = 0; i < partType.parts.Length; i++)
        {
            foreach (GameObject gb in partType.parts[i].partsObjects)
            {
                if (i == select)
                {
                    gb.SetActive(true);
                }
                else
                {
                    gb.SetActive(false);
                }
            }
        }
    }

    private void SetAllParts()
    {
        foreach (PartType pt in partTypes)
        {
            int selectedItem = PlayerPrefs.GetInt("Parts1" + pt.partName, 0);
            SetPartFromId(pt, selectedItem);
        }
    }

    // New method to change the color of all car parts
    public void ChangeCarColor(Color color)
    {
        foreach (GameObject part in carParts)
        {
            Renderer renderer = part.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;  // Change the color of the material
            }
        }
    }

    // Method to handle button click event for changing car color
    public void OnColorButtonClick(Color color)
    {
        ChangeCarColor(color);
    }
}

[System.Serializable]
public class PartType
{
    public string partName;
    public Part[] parts;
    public int selected;
}

[System.Serializable]
public class Part
{
    public GameObject[] partsObjects;
}
