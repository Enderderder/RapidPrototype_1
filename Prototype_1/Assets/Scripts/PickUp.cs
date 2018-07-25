using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.gameObject.tag == "Slime1")
        {
            _other.gameObject.GetComponent<SlimeLogic>().HealthUp();
            Destroy(this.gameObject);
            return;
        }
        else if (_other.gameObject.tag == "Player")
        {
            if (!_other.gameObject.GetComponentInParent<PlayerLogic>().pickedUp)
            {
                _other.gameObject.GetComponentInParent<PlayerLogic>().GainPickUp();
                Destroy(this.gameObject);
            }
            return;
        }
    }
}
