using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public GameObject InventoryMenu;
    private bool menuActive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActive){
            Debug.Log("stuff");
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActive = false;
        }
        else if (Input.GetButtonDown("Inventory") && !menuActive){
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActive = true;
        }
    }

    public void AddItem(string itemName, int quantity, Sprite sprite){
        Debug.Log("itemName = " + itemName + " Quantity = " + quantity);
    }
}
