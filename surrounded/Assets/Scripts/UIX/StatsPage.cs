using System.Collections;
using UnityEngine;
using TMPro;

public class StatsPage : MonoBehaviour
{
    // Array of item prefabs (one prefab per item)
    public GameObject[] itemPrefabs; 
    // Parent object where item UI elements will be added (Content of the Scroll View)
    public Transform content;

    // Array to keep track of item quantities
    private int[] itemQuantities = new int[8];

    // Array to store the instantiated item UI elements
    private GameObject[] itemUIElements = new GameObject[8];
    public void Setup(){
        gameObject.SetActive(true);
    }

    public void Resume(){
        gameObject.SetActive(false);
    }

    public void AddItem(int itemIndex)
{
    // Check if the item UI already exists in the Scroll View
    if (itemUIElements[itemIndex] == null)
    {
        // Instantiate prefab for this item and place it in Scroll View content
        GameObject newItemUI = Instantiate(itemPrefabs[itemIndex], content);

        // Store prefab in array
        itemUIElements[itemIndex] = newItemUI;

        // Initialize num to 1 for the first instance
        itemQuantities[itemIndex] = 1;
    }
    else
    {
        // Increment the quantity of item
        itemQuantities[itemIndex]++;
    }

    // Update num of item in the UI
    TextMeshProUGUI x = itemUIElements[itemIndex].transform.Find("numOf").GetComponent<TextMeshProUGUI>();
    x.text = "x" + itemQuantities[itemIndex].ToString();
}

}