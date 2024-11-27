using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    private static GameObject[] persistentObjects = new GameObject[3];
    public int objectIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(persistentObjects[objectIndex] == null){
            persistentObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        } else if (persistentObjects[objectIndex] != gameObject){
            Destroy(gameObject);
        }

        
    }

}
