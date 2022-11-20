using UnityEngine;

namespace NumericalControl
{
    internal class ControllerToglgleSwitch : MonoBehaviour
    {     
        [SerializeField]
        internal ToggleAndLigth[] toggleAndLigths;

        void Awake()
        {
            InitToggleAndLigths();
        }

        private void InitToggleAndLigths()
        {
            foreach (ToggleAndLigth toggleLigthin in toggleAndLigths)
                toggleLigthin.InitStart();
        }  
    }
}
