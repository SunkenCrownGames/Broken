namespace V1
{
    using UnityEngine;

    [System.Serializable]    
    public class PatrolData
    {
        [SerializeField]
        private float m_ledgeDistance;


        public float LedgeDistance
        {
            get { return m_ledgeDistance; }
            set { m_ledgeDistance = value; }
        }
    }
}