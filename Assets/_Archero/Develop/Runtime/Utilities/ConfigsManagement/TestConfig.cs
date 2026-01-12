using UnityEngine;

namespace _Archero.Develop.Runtime.Utilities.ConfigsManagement
{
    [CreateAssetMenu(menuName = "Test", fileName = "TestConfig")]
    public class TestConfig : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; private set; }
    }
}
