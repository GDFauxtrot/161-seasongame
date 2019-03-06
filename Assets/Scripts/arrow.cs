using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    private GameObject seasoning;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay(){
        yield return new WaitForSeconds(1f);
        seasoning = GameObject.FindGameObjectWithTag("seasoning");
    }
    // Update is called once per frame
    void Update()
    {
        rotateArrow();
    }

    void rotateArrow()
    {
        //take difference in position so we know where to point
        Vector3 pointing = seasoning.transform.position - this.transform.position;
        pointing.Normalize();
        float rotateZ = Mathf.Atan2(pointing.y, pointing.x) * Mathf.Rad2Deg;

        //rotate the arrow using the differences saved
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
    }
}
