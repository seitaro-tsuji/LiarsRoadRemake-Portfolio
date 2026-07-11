using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    public void Activate()
    {
        _gameObject.SetActive(true);
    }
}
