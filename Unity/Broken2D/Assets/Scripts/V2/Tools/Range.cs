namespace V2
{
    using UnityEngine;
    [System.Serializable]
    public class Range
    {
        [SerializeField]
        private float m_minRange = 0;
        [SerializeField]
        private float m_maxRange = 0;

        public float MinRange
        {
            get { return m_minRange; }
            set { m_minRange = value; }
        }

        public float MaxRange
        {
            get { return m_maxRange; }
            set { m_maxRange = value; }
        }
    }
}