namespace V2
{
    using UnityEngine;
    
    [System.Serializable]
    public class EntityKnockBackData
    {
        [SerializeField]
        private float m_horizontalKnockBack;

        [SerializeField]
        private float m_verticalKnockback;
        [SerializeField]
        private float m_currentDuration;
        [SerializeField]
        private float m_duration;
        
        [SerializeField]
        private bool m_verticalKnockbackTrigger;

        public float HorizontalKnockBack
        {
            get { return m_horizontalKnockBack; }
            set { m_horizontalKnockBack = value; }
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

        public bool VerticalKnockbackTrigger
        {
            get { return m_verticalKnockbackTrigger; }
            set { m_verticalKnockbackTrigger = value; }
        }

        public float VerticalKnockBack
        {
            get { return m_verticalKnockback; }
            set { m_verticalKnockback = value; }
        }
    }
}