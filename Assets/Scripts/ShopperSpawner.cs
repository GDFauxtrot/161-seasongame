using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopperSpawner : MonoBehaviour
{
    public int amountOfShoppers;
    public GameObject prefab;
    

    void Awake()
    {
        StartCoroutine(InputCoroutine());
    }

    IEnumerator InputCoroutine()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.P))
            {
                GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
                obj.GetComponent<ShopperNPC>().rb2d.AddForce(Random.insideUnitCircle * Random.Range(0, 750));
            }
            yield return new WaitForSecondsRealtime(0.25f);
        }
        
    }
}
