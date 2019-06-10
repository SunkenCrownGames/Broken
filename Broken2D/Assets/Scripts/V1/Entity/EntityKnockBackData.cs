namespace V1
{
    using UnityEngine;

    [System.Serializable]
    public class EntityKnockBackData
    {
        [SerializeField]
        private float m_currentDuration;

        [SerializeField]
        private float m_duration = 0.25f;

        [SerializeField]
        private float m_horizontalKnockBackStrength = 5;

        [SerializeField]
        private float m_verticalKnockBackStrength = 2;

        [SerializeField]
        private bool m_knockBackActive;

        public void Reset()
        {
            m_knockBackActive = false;
            m_currentDuration = 0;
        }


        public float CurrentDuration
        {
            get { return m_currentDuration; }
            set { m_currentDuration = value; }
        }

        public float Duration
        {
            get { return m_duration; }
            set { m_duration = value; }
        }

        public float HorizontalKnockBackStrength
        {
            get { return m_horizontalKnockBackStrength; }
            set { m_horizontalKnockBackStrength = value; }
        }

        public float VerticalKnockBackStrength
        {
            get { return m_verticalKnockBackStrength; }
            set { m_verticalKnockBackStrength = value;}
        }

        public bool KnockBackActive
        {
            get { return m_knockBackActive; }
            set { m_knockBackActive = value; }
        }

    }
}