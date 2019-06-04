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



    }
}