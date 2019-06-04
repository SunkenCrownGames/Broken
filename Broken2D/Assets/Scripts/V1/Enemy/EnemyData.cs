namespace V1
{
    using UnityEngine;
    
    [System.Serializable]
    public class EnemyData
    {
        [SerializeField]
        private EnemyType m_enemyType;

        [SerializeField]
        private float m_movementSpeed;

        [SerializeField]
        private float m_maxMovementSpeed;

        [SerializeField]
        private float m_verticalSpeed;

        [SerializeField]
        private float m_horizontalDirection = 1;

        [SerializeField]
        private float m_maxFallSpeed;

        [SerializeField]
        private float m_detectionRange;

        [SerializeField]
        private float m_attackRange;

        public float MovementSpeed
        {
            get { return m_movementSpeed; }
            set { m_movementSpeed = value; }
        }

        public float MaxMovementSpeed
        {
            get { return m_maxMovementSpeed; }
            set { m_maxMovementSpeed = value; }
        }

        public float VerticalSpeed
        {
            get { return m_verticalSpeed; }
            set { m_verticalSpeed = value; }
        }

        public float Direction
        {
            get { return m_horizontalDirection; }
            set { m_horizontalDirection = value; }
        }

        public float MaxFallSpeed
        {
            get { return m_maxFallSpeed; }
            set { m_maxFallSpeed = value; }
        }

        public EnemyType EnemyInfo
        {
            get { return m_enemyType; }
            set { m_enemyType = value; }
        }

        public float DetectionRange
        {
            get { return m_detectionRange; }
            set { m_detectionRange = value; }
        }
        

        public float AttackRange
        {
            get { return m_attackRange; }
            set { m_attackRange = value; }
        }


    }
}