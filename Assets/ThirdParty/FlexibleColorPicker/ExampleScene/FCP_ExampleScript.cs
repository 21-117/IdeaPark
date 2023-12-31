using UnityEngine;

public class FCP_ExampleScript : MonoBehaviour {

    public bool getStartingColorFromMaterial;
    public FlexibleColorPicker fcp;
    public Material material;
    private bool findLocalPlayer;
    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (PlayerInfo.localPlayer != null)
        {
            if (!findLocalPlayer)
            {
                fcp = PlayerInfo.localPlayer.fcp;
                if (getStartingColorFromMaterial)
                    fcp.color = material.GetColor("Color_cf12b49411d94583a269f83e6981abd1");

                fcp.onColorChange.AddListener(OnChangeColor);
                findLocalPlayer = true;
            }
        }
    }

    private void OnChangeColor(Color co) {
        //material.color = co;
        material.SetColor("Color_cf12b49411d94583a269f83e6981abd1", co);


    }
}
