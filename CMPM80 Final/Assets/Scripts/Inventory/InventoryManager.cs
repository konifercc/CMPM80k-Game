using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public GameObject InventoryMenu;
    private bool menuActive;
    public ItemSlot[] itemSlot;
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

    public int AddItem(string itemName, int quantity, Sprite sprite, string itemDescription){
        //Debug.Log("itemName = " + itemName + " Quantity = " + quantity);
        for (int i = 0; i < itemSlot.Length; i++){
            if (!itemSlot[i].isFull && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0){
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, sprite, itemDescription);
                if (leftOverItems > 0){
                    leftOverItems = AddItem(itemName, leftOverItems, sprite, itemDescription);

                    //return ;
                }
                return leftOverItems;
            }
        }
        return quantity;

    }

    public void DeselectAllSlots(){
        for (int i = 0; i < itemSlot.Length; i++){
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].itemSelected = false;
        }
    }
}
