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

        [SerializeField]
        private float m_meleeAttackRange;

        [SerializeField]
        private float m_rangedAttackRange;

        [SerializeField]
        private EnemyPatrolAttackType m_enemyAttackType = EnemyPatrolAttackType.MELEE;



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
    }
}