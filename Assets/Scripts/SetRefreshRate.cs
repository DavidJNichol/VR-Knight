using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SetRefreshRate : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Start()
    {
        if (OVRManager.display.displayFrequenciesAvailable.Contains(90f))
        {
            OVRManager.display.displayFrequency = 90f;
        }       
    }
}
