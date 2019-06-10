using UnityEngine;

namespace V1
{
    public class Entity : MonoBehaviour
    {
        public EntityType m_entityType;
        public static float m_speedDeadZone = 0.5f;

        [SerializeField]
        private EntityMovementState m_movementState;
        [SerializeField]
        private EntityActionState m_actionState;
        [SerializeField]
        private EntityVerticalDirection m_verticalDirection;
        [SerializeField]
        private EntityHorizontalDirection m_horizontalDirection = EntityHorizontalDirection.RIGHT;

        [SerializeField]

        private float m_groundedDistance = 0;

        private Bounds m_bounds;
        private int m_groundLayerMask;
        private static GameManager m_gm;

        //The ground we are currently on
        private GameObject m_ground;

        private static GameObject m_playerRef;

        private void Awake() 
        {
            BindObjects();
        }

        private void Update()
        {
            CheckGrounded();
            UpdateMovementState();
        }
        

        private void BindObjects()
        {
            if(m_gm == null)
                m_gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            m_bounds = GetComponent<Bounds>();
            m_groundLayerMask = LayerMask.GetMask("ENV");
        }

        private void Grounded(GameObject p_object)
        {
            m_ground = p_object;
        }

        private void CheckGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, m_groundLayerMask);

            if (hit.collider != null && transform.position.y > hit.point.y)
            {
                GameObject hitObject = hit.collider.gameObject;
                Bounds objectBounds = hitObject.GetComponent<Bounds>();
                float distance = m_bounds.EntityVerticalBounds[0] - objectBounds.EntityVerticalBounds[1];

                if(m_bounds.EntityVerticalBounds[1] > objectBounds.EntityVerticalBounds[0])
                {
                    if(hit.distance < m_groundedDistance && m_movementState == EntityMovementState.FALLING && m_verticalDirection == EntityVerticalDirection.FALLING)
                    {
                        //Debug.Log("SET GROUNDED");
                        if(hitObject.CompareTag("Ground"))
                        {
                            Grounded(hitObject);
                        }
                    }
                    else if(hit.distance > m_groundedDistance)
                    {
                        Grounded(null);
                    }
                }
            }
            else
            {
                Grounded(null);
            }
        }

        private void UpdateMovementState()
        {
                //If we are not grounded
                if(m_ground == null)
                {
                    //If we are not jumping
                    if(m_movementState != EntityMovementState.JUMPING)
                        m_movementState = EntityMovementState.FALLING;
                }
                //if we are not currently jumping
                else if(m_movementState == EntityMovementState.FALLING)
                {
                    m_movementState = EntityMovementState.GROUNDED;
                }
        }
        public EntityMovementState MovementState
        {
            get { return m_movementState; }
            set { 
                    if(m_movementState != value)
                    {
                        m_movementState = value; 
                        //Fire Change Animation
                    }
                }
        }

        public EntityActionState ActionState
        {
            get { return m_actionState; }
            set { 
                    if(m_actionState != value)
                    {
                        m_actionState = value; 
                        //Fire Change Animation
                    }
                }
        }

        public EntityVerticalDirection VerticalDirection
        {
            get { return m_verticalDirection; }
            set {
                    if(m_verticalDirection != value)
                    { 
                        m_verticalDirection = value; 
                    }
                }
        }

        public GameManager GameManagerRef
        {
            get { return m_gm; }
            set { m_gm = value; }
        }

        public GameObject Ground
        {
            get { return m_ground; }
            set { m_ground = value; }
        }
        
        public EntityHorizontalDirection HorizontalDirection
        {
            get { return m_horizontalDirection; }
            set { 
                    if(m_horizontalDirection != value)
                    {
                        m_horizontalDirection = value; 
                    }
                }
        }
        public GameObject PlayerRef
        {
            get { return m_playerRef; }
            set { m_playerRef = value; }
        }
    }
}