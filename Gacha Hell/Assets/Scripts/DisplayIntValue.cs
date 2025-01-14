using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayIntValue : MonoBehaviour
{
    public TMP_Text m_Text;
    private int valueToDisplay = 100;
    // Start is called before the first frame update
    void Start()
    {
        m_Text.text = valueToDisplay.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeValue(int changeBy)
    {
        valueToDisplay += changeBy;
        m_Text.text = valueToDisplay.ToString();
    }
}
