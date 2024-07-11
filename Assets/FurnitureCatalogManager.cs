using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureCatalogManager : MonoBehaviour
{
    public List<FurnitureItem> furnitureItems; // List to hold all furniture items

    void Start()
    {
        foreach (var item in furnitureItems)
        {
            Button button = item.button.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => SpawnFurniture(item.prefab));
            }
        }
    }

    void SpawnFurniture(GameObject prefab)
    {
        // Spawn the selected prefab at a default position 
        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}

[System.Serializable]
public class FurnitureItem
{
    public string name; 
    public GameObject prefab; 
    public GameObject button; 
}
