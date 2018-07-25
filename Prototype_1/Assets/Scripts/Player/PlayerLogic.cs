using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public bool pickedUp = false;

    public void GainPickUp()
    {
        pickedUp = true;
    }

    public void GivePickUp()
    {
        pickedUp = false;
    }
}
