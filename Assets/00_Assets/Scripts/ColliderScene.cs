using UnityEngine;

public class ColliderScene : SceneLoadManager
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EXIT"))
        {
            LoadRandomLevel();
        }
    }
}
