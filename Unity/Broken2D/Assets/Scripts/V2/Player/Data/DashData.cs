namespace V2
{
    using UnityEngine;
    
    [System.Serializable]
    public class DashData
    {
        [SerializeField]
        private float m_dashSpeed;
        [SerializeField]
        private float m_dashLength;

        [SerializeField]
        private float m_dashDestination;


        public float DashSpeed
        {
            get { return m_dashSpeed; }
            set { m_dashSpeed = value; }
        }

        public float DashLength
        {
            get { return m_dashLength; }
            set { m_dashLength = value; }
        }
        
        public float DashDestination
        {
            get { return m_dashDestination; }
            set { m_dashDestination = value; }
        }
    }
}