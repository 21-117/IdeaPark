using UnityEngine;

public class FCP_ExampleScript : MonoBehaviour {

    public bool getStartingColorFromMaterial;
    public FlexibleColorPicker fcp;
    public Material material;
    //public bub

    private void Start() {
        if(getStartingColorFromMaterial)
            fcp.color = material.color;

        fcp.onColorChange.AddListener(OnChangeColor);
    }

    private void OnChangeColor(Color co) {
        
    }
}
