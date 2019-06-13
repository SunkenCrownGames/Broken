namespace V2
{
    using UnityEngine;
    
    [System.Serializable]
    public class EntityData
    {
        public EntityType m_entityType = EntityType.NONE;

        [Header("Action and Movement States")]
        [SerializeField]
        protected EntityMovementState m_movementState = EntityMovementState.NONE;

        [SerializeField]
        protected EntityActionState m_actionState = EntityActionState.NONE;


        [Header("Movement Data")]
        [SerializeField]
        protected HorizontalDirection m_horizontalDirection = HorizontalDirection.RIGHT;

        [SerializeField]
        protected VerticalDirection m_verticalDirection = VerticalDirection.UP;

        [SerializeField]
        [Tooltip("Distance Between Entity and Ground To Be Considered Grounded")]
        protected float m_groundedDistance;

        [SerializeField]
        protected HorizontalDirection m_hitDirection;

        [Header("KnockBack Data")]
        [SerializeField]
        private EntityKnockBackData m_hitKnockbackData;

        [Header("ScreenShake Data")]
        [SerializeField]
        private ScreenShakeData m_hitScreenShakeData;
        [SerializeField]
        private ScreenShakeData m_deathScreenShakeData;

        public EntityMovementState MovementState
        {
            get { return m_movementState; }
            set { m_movementState = value; }
        }
        
        public EntityActionState ActionState
        {
            get { return m_actionState; }
            set { m_actionState = value; }
        }

        public HorizontalDirection HDirection
        {
            get { return m_horizontalDirection; }
            set { m_horizontalDirection = value; }
        }

        public VerticalDirection VDirection
        {
            get { return m_verticalDirection; }
            set { m_verticalDirection = value; }
        }

        public float GroundedDistance
        {
            get { return m_groundedDistance; }
            set { m_groundedDistance = value; }
        }

        public EntityKnockBackData KnockBackData
        {
            get { return m_hitKnockbackData; }
            set { m_hitKnockbackData = value; }
        }


        public ScreenShakeData HitScreenShakeData
        {
            get { return m_hitScreenShakeData; }
            set { m_hitScreenShakeData = value; }
        }

        public ScreenShakeData DeathScreenShakeData
        {
            get { return m_deathScreenShakeData; }
            set { m_deathScreenShakeData = value; }
        }

        public HorizontalDirection HitDirection
        {
            get { return m_hitDirection; }
            set { m_hitDirection = value; }
        }
    }
}