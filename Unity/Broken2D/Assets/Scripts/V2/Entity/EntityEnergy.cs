namespace V2
{
    using UnityEngine;
    
    public class EntityEnergy : MonoBehaviour
    {
        [SerializeField]
        private float m_energyValue;

        public float EnergyValue
        {
            get { return m_energyValue; }
            set { m_energyValue = value; }
        }
    }
}