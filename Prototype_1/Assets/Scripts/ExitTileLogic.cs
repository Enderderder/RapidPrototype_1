using UnityEngine;

public class ExitTileLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Slime")
        {
            GameController.instance.NextLevel();
        }
    }
}
