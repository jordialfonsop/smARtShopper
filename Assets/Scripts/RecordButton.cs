using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordButton : MonoBehaviour
{
    // Start is called before the first frame update

    public void SetButtonColor(Color color)
    {
        Oculus.Interaction.InteractableColorVisual.ColorState colorState = new Oculus.Interaction.InteractableColorVisual.ColorState() { Color = color };
        this.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Oculus.Interaction.InteractableColorVisual>().InjectOptionalNormalColorState(colorState);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
