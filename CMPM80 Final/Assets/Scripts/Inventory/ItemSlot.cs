using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{

    //Item Data
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField] private int maxNumber;

    //Item Slot
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    //Item Description
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionName;
    public TMP_Text itemDescriptionText;

    public GameObject selectedShader;
    public bool itemSelected;

    private InventoryManager inventoryManager;

    private void Start(){
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription){
        //check if slot is full
        if (isFull){
            return quantity;
        }
        //update Name, Quantity, Image, and Desc
        this.itemName = itemName;
        
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        itemImage.sprite = itemSprite;

        this.quantity += quantity;
        if (this.quantity >= maxNumber){
            quantityText.text = maxNumber.ToString();
            quantityText.enabled = true;
            isFull = true;
        

            int extraItems =  this.quantity - maxNumber;
            this.quantity = maxNumber;
            return extraItems;
        }

        //if not overfill 
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData){
        if (eventData.button == PointerEventData.InputButton.Left){
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right){
            OnRightClick();
        }
    }

    public void OnLeftClick(){
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        itemSelected = true;
        itemDescriptionName.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
        if (itemDescriptionImage == null){
            itemDescriptionImage.sprite = emptySprite;
        }
    }

    public void OnRightClick(){

    }

}
