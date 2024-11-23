using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    [SerializeField] private Sprite sprite;

    private InventoryManager inventoryManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    // void onCollisionEnter2D(Collision2D col){
    //     Debug.Log(col.gameObject.tag);

    //     if (col.gameObject.tag == "PlayerHitBox"){
    //         inventoryManager.AddItem(itemName, quantity, sprite);
    //         Destroy(gameObject);
    //     }
    //     Debug.Log("OnCollisionEnter2D");
    // }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "PlayerTag"){
            inventoryManager.AddItem(itemName, quantity, sprite);
            Destroy(gameObject);
        }
    }
}
