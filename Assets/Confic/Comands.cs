using UnityEngine;
  
namespace NumericalControl
{
    
    [CreateAssetMenu(fileName = "New Comands Scenario", menuName = "Comands Scenario", order = 51)]
    internal class Comands : ScriptableObject
    {
        [SerializeField]
        internal ElementsComand[] elementsComand;
    }
}