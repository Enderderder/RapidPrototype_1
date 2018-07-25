using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    [Header("Characters:")]
    private SlimeLogic slime;
    private PlayerLogic player;

    [Header("UI:")]
    public Image SlimeIndicator;
    public Image pickupIndicator;

    [Header("Slime Health Indicator Sprites:")]
    public Sprite slimeHealth1Sprite;
    public Sprite slimeHealth2Sprite;
    public Sprite slimeHealth3Sprite;

    private void Start()
    {
        player = gameObject.GetComponent<PlayerLogic>();
        slime = GameObject.FindGameObjectWithTag("Slime1").GetComponent<SlimeLogic>();
    }

    private void Update()
    {
        switch (slime.slimeHealth)
        {
            case 1:
                SlimeIndicator.sprite = slimeHealth1Sprite;
                break;
            case 2:
                SlimeIndicator.sprite = slimeHealth2Sprite;
                break;
            case 3:
                SlimeIndicator.sprite = slimeHealth3Sprite;
                break;
            default: break;
        }

        if (!player.pickedUp)
        {
            pickupIndicator.color = new Vector4(0.3f, 0.3f, 0.3f, 1.0f);
        }
        else
        {
            pickupIndicator.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
