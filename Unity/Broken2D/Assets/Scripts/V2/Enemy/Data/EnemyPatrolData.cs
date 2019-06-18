namespace V2
{
    using UnityEngine;

    [System.Serializable]    
    public class EnemyPatrolData
    {
        [Header("Movemnt Logic Data")]
        [SerializeField]
        private float m_ledgeDistance;

        [SerializeField]
        private PatrolStatus m_patrolStatus;
        
         [SerializeField]
        private EnemyPatrolAttackType m_enemyAttackType = EnemyPatrolAttackType.MELEE;

        [Header("Range Data")]
        [SerializeField]
        private float m_detectionRange;

        [SerializeField]
        private float m_ledgeStopDistance = 0.3f;

        [SerializeField]
        private float m_meleeAttackRange;

        [SerializeField]
        private float m_rangedAttackRange;

        public float MeleeAttackRange
        {
            get { return m_meleeAttackRange; }
            set { m_meleeAttackRange = value; }
        }

        public float RangedAttackRange
        {
            get { return m_rangedAttackRange; }
            set { m_rangedAttackRange = value; }
        }

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

        public EnemyPatrolAttackType EnemyAttackType
        {
            get { return m_enemyAttackType; }
            set { m_enemyAttackType = value; }
        }

        public float DetectionRange
        {
            get { return m_detectionRange; }
            set { m_detectionRange = value; }
        }

        public float LedgeStopDistance
        {
            get { return m_ledgeStopDistance; }
            set { m_ledgeStopDistance = value; }
        }
    }
}