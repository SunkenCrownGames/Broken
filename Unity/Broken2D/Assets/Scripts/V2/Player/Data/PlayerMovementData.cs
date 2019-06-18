namespace V2
{
    using UnityEngine;

    [System.Serializable]
    public class PlayerMovementData
    {
        [Header("Horizontal Data")]
         [SerializeField]
        private float m_movementSpeed;

        [SerializeField]
        private float m_maxMovementSpeed;

        [SerializeField]
        private float m_hAccelerationSpeed;

        [SerializeField]
        private float m_hDecelerationSpeed;


        [SerializeField]
        private float m_verticalSpeed;

        [Header("Vertical Data")]
        [SerializeField]
        private float m_jumpHeight;

        [SerializeField]
        private float m_jumpSpeed;

        [SerializeField]
        private float m_jumpSlowDownDistance;

        [SerializeField]
        private float m_maxJumpSpeed;

        [SerializeField]
        private float m_maxFallSpeed;

        [SerializeField]
        private DashData m_dashData;

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

        public float HorizontalAccelerationSpeed
        {
            get { return m_hAccelerationSpeed; }
            set { m_hAccelerationSpeed = value; }
        }

        public float HorizontalDecelerationSpeed
        {
            get { return m_hDecelerationSpeed; }
            set { m_hDecelerationSpeed = value; }
        }

        public float JumpHeight
        {
            get { return m_jumpHeight; }
            set { m_jumpHeight = value; }
        }

        public float VerticalSpeed
        {
            get { return m_verticalSpeed; }
            set { m_verticalSpeed = value; }
        }

        public float JumpSpeed
        {
            get { return m_jumpSpeed; }
            set { m_jumpSpeed = value; }
        }

        public float MaxJumpSpeed
        {
            get { return m_maxJumpSpeed; }
            set { m_maxJumpSpeed = value; }
        }

        public float JumpSlowdownDistance
        {
            get { return m_jumpSlowDownDistance; }
            set { m_jumpSlowDownDistance = value; }
        }

        public float MaxFallSpeed
        {
            get { return m_maxFallSpeed; }
            set { m_maxFallSpeed = value; }
        }

        public DashData DashInfo
        {
            get { return m_dashData; }
            set { m_dashData = value; }
        }
    }
}