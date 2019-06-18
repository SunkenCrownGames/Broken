namespace V2
{
    using UnityEngine;
    
    public class OrbSizeScaler : MonoBehaviour
    {
        [SerializeField]
        private bool m_debug = false;

        [SerializeField]
        private float m_initialScale = 1;

        [SerializeField]
         private float m_minScale = 0.5f;

        [SerializeField]
        private float m_maxScale = 2;

        [SerializeField]
        private float m_energyToScaleRatio = 500;

        private float m_currentScale = 0;

        private EntityEnergy m_entityEnergy;
        
        private void Awake() 
        {
            BindObjects();
            CalculateScale();
            SetScale();
        }

        private void Update()
        {
            if(m_debug)
            {
                CalculateScale();
                SetScale();
            }
        }

        private void BindObjects()
        {
            m_entityEnergy = GetComponent<EntityEnergy>();
        }

        private void CalculateScale()
        {
            m_currentScale = m_initialScale * m_entityEnergy.EnergyValue / m_energyToScaleRatio;

            if(m_currentScale >= m_maxScale)
                m_currentScale = m_maxScale;
            else if(m_currentScale <= m_minScale)
                m_currentScale = m_minScale;

            //Debug.Log("Current Scale: " + m_currentScale);
        }

        private void SetScale()
        {
            transform.localScale = new Vector3(m_currentScale, m_currentScale, transform.localScale.z);
        }

        public EntityEnergy EntityEnergy 
        {
            get { return m_entityEnergy; }
            set { m_entityEnergy = value; }
        }
    }
}