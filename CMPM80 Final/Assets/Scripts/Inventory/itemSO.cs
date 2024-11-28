using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class itemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public SpeedToChange speedToChange = new SpeedToChange();
    public int amountToChangeSpeed;
    

    public bool UseItem(){
        if(statToChange == StatToChange.health){
            Debug.Log("healed");
            PlayerHealth playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
            playerHealth.AddHealth(amountToChangeStat);
            
            return true;
        }
        if(statToChange == StatToChange.attack){
            //GameObject.Find("Square(Sword)").GetComponent<PlayerAttack>().attackDamage += (float)amountToChangeStat;
            GameObject.Find("Square(Sword)").GetComponent<PlayerAttack>().buffAttack((float)amountToChangeStat);
            
            return true;
        }
        return false;
    }




    public enum StatToChange{
        none, 
        health, 
        attack
    };

    public enum SpeedToChange{
        none, 
        attackSpeed, 
        movementSpeed
    };
}
