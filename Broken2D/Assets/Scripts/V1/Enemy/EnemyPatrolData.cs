namespace V1
{
    using UnityEngine;

    [System.Serializable]    
    public class EnemyPatrolData
    {
        [SerializeField]
        private float m_ledgeDistance;

        [SerializeField]
        private PatrolStatus m_patrolStatus;


        public float LedgeDistance
        {
            get { return m_ledgeDistance; }
            set { m_ledgeDistance = value; }
        }

        public PatrolStatus CurrentStatus
        {
            get { return m_patrolStatus; }
            set { m_patrolStatus = value; }
        }
    }
}