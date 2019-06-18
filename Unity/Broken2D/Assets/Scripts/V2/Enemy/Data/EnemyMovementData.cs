namespace V2
{
    using UnityEngine;

    [System.Serializable]
    public class EnemyMovementData
    {
        [Header("HorizontalDirection")]
        [SerializeField]
        private float m_horizontalDirection = 1;

        [Header("Horizontal Data")]
        [SerializeField]
        private float m_movementSpeed;

        [SerializeField]
        private float m_horizontalAccelerationSpeed;
        

        [SerializeField]
        private float m_maxMovementSpeed;

        [Header("Vertical Data")]
        [SerializeField]
        private float m_verticalSpeed;


        [SerializeField]
        private float m_verticalAccelerationSpeed;

        [SerializeField]
        private float m_maxVerticalSpeed;

        [SerializeField]
        private float m_maxFallSpeed;

        public float HorizontalDirection
        {
            get { return m_horizontalDirection; }
            set { m_horizontalDirection = value; }
        }

        public float MovementSpeed
        {
            get { return m_movementSpeed; }
            set { m_movementSpeed = value; }
        }

        public float HorizontalAcceleration
        {
            get { return m_horizontalAccelerationSpeed; }
            set { m_horizontalAccelerationSpeed = value; }
        }

        public float VerticalSpeed
        {
            get { return m_verticalSpeed; }
            set { m_verticalSpeed = value; }
        }

        public float VerticalAccelerationSpeed
        {
            get { return m_verticalAccelerationSpeed; }
            set {m_verticalAccelerationSpeed = value; }
        }

        public float MaxVerticalSpeed
        {
            get { return m_maxVerticalSpeed; }
            set { m_maxVerticalSpeed = value; }
        }

        public float MaxFallSpeed
        {
            get { return m_maxFallSpeed; }
            set { m_maxFallSpeed = value; }
        }

        public float MaxMovementSpeed
        {
            get { return m_maxMovementSpeed; }
            set { m_maxMovementSpeed = value; }
        }

    }
}