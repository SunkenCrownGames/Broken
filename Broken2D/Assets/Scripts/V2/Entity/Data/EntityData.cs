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

        //The sprite bounds
        protected Bounds m_bounds;
        //The layer that all the ground tiles are on
        protected int m_groundLayerMask;
        //Current Ground Reference
        protected GameObject m_ground;

        protected static GameManager m_gm;

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

        public GameManager GM
        {
            get { return m_gm; }
            set { m_gm = value; }
        }

        public Bounds EntityBounds
        {
            get { return m_bounds; }
            set { m_bounds = value; }
        }

        public int GroundLayerMask
        {
            get { return m_groundLayerMask; }
            set { m_groundLayerMask = value; }
        }

        public GameObject Ground
        {
            get { return m_ground; }
            set { m_ground = value; }
        }

        public Bounds Bounds
        {
            get { return m_bounds; }
            set { m_bounds = value; }
        }
    }
}