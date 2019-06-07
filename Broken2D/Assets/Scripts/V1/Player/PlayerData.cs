using UnityEngine;

namespace V1
{
    [System.Serializable]
    public class PlayerData : EntityData
    {
        [SerializeField]
        private float m_movementSpeed;

        [SerializeField]
        private float m_maxMovementSpeed;

        [SerializeField]
        private float m_verticalSpeed;

        [SerializeField]
        private float m_hAccelerationSpeed;

        [SerializeField]
        private float m_hDecelerationSpeed;

        [SerializeField]
        private float m_vDecelerationSpeed;

        [SerializeField]
        private int m_jumpCount; 

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
        private AbilityCostData m_abilityCostData;

        [SerializeField]
        private DashData m_dashData;

        [SerializeField]
        private ScreenShakeData m_screenShakeHitData;

        [SerializeField]
        private ScreenShakeData m_screenShakeDeathData;



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

        public float VerticalDecelerationSpeed
        {
            get { return m_vDecelerationSpeed; }
            set { m_vDecelerationSpeed = value; }
        }


        public int JumpCount
        {
            get { return m_jumpCount; }
            set { m_jumpCount = value; }
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

        public AbilityCostData CostData
        {
            get { return m_abilityCostData; }
            set { m_abilityCostData = value; }
        }

        public DashData DashInfo
        {
            get { return m_dashData; }
            set { m_dashData = value; }
        }

        public ScreenShakeData ShakeHitData
        {
            get { return m_screenShakeHitData; }
            set { m_screenShakeHitData = value; }
        }

        public ScreenShakeData ShakeDeathData
        {
            get { return m_screenShakeDeathData; }
            set { m_screenShakeDeathData = value; }
        }
    }
}