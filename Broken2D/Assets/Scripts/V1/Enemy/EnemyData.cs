namespace V1
{
    using UnityEngine;
    
    [System.Serializable]
    public class EnemyData
    {
        [Header("Enemy Combat")]
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

        [Header("Enemy Seek Data")]
        [SerializeField]
        private float m_frequency;
        [SerializeField]
        private float m_magnitude;
        
        private float m_elapsedTime;

        
        [Header("Enemy Energy Value")]
        [SerializeField]
        private EnemyOrbData m_orbData;

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

        public EnemyType EnemyType
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

        public EnemyOrbData OrbData
        {
            get { return m_orbData; }
            set { m_orbData = value; }
        }

        public float ElapsedTime
        {
            get { return m_elapsedTime; }
            set { m_elapsedTime = value; }
        }

        public float Frequency
        {
            get { return m_frequency; }
            set { m_frequency = value; }
        }

        public float Magnitude
        {
            get { return m_magnitude; }
            set { m_magnitude = value; }
        }
    }
}