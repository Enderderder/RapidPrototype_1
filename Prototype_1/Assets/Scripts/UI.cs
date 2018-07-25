using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    [Header("Characters:")]
    public SlimeLogic slime;
    private PlayerLogic player;

    [Header("UI:")]
    public Image SlimeIndicator;
    public Image pickupIndicator;

    [Header("Slime Health Indicatior Sprites:")]
    public Sprite slimeHealth1Sprite;
    public Sprite slimeHealth2Sprite;
    public Sprite slimeHealth3Sprite;

    [Header("Pickup Indicator Sprites:")]
    public Sprite pickedUpSprite;
    public Sprite notPickedUpSprite;

    private void Start()
    {
        player = gameObject.GetComponent<PlayerLogic>();
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
            pickupIndicator.sprite = notPickedUpSprite;
        }
        else
        {
            pickupIndicator.sprite = pickedUpSprite;
        }
    }
}
