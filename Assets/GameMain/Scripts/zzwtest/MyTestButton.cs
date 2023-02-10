using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyTestButton : MonoBehaviour
{
    public Button TestButton;
    // Start is called before the first frame update
    void Start()
    {
        TestButton.onClick.AddListener(() =>
        {
            Debug.Log("#####zzw");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
