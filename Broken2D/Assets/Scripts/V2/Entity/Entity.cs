namespace V2
{
    using UnityEngine;

    public class Entity : MonoBehaviour
    {
        [SerializeField]
        protected EntityData m_entityData;

        public EntityData Data
        {
            get { return m_entityData; }
            set { m_entityData = value; }
        }

        protected virtual void Awake()
        {
            BindObjects();
        }

        protected virtual void Update()
        {
            CheckGrounded();
            UpdateMovementState();
        }

        private void BindObjects()
        {
            if(m_entityData.GM == null)
                m_entityData.GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            
            m_entityData.EntityBounds = GetComponent<Bounds>();
            m_entityData.GroundLayerMask = LayerMask.GetMask("ENV");
        }

        private void Grounded(GameObject p_object)
        {
            m_entityData.Ground = p_object;
        }

        //This Function Checks If The Player Has Hit The Ground By Firing a Ray Downwards
        private void CheckGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, m_entityData.GroundLayerMask);

            if (hit.collider != null && transform.position.y > hit.point.y)
            {
                GameObject hitObject = hit.collider.gameObject;
                Bounds objectBounds = hitObject.GetComponent<Bounds>();
                float distance = m_entityData.EntityBounds.EntityVerticalBounds[0] - objectBounds.EntityVerticalBounds[1];

                if(m_entityData.EntityBounds.EntityVerticalBounds[1] > objectBounds.EntityVerticalBounds[0])
                {
                    if(hit.distance < m_entityData.GroundedDistance && m_entityData.MovementState == EntityMovementState.FALLING && m_entityData.VDirection == VerticalDirection.DOWN)
                    {
                        //Debug.Log("SET GROUNDED");
                        if(hitObject.CompareTag("Ground"))
                        {
                            Grounded(hitObject);
                        }
                    }
                    else if(hit.distance > m_entityData.GroundedDistance)
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

        //This Function Updates the Movement State Depending on the Air State Of the Entity
        private void UpdateMovementState()
        {
                //If we are not grounded
                if(m_entityData.Ground == null)
                {
                    //If we are not jumping
                    if(m_entityData.MovementState != EntityMovementState.JUMPING)
                        m_entityData.MovementState = EntityMovementState.FALLING;
                }
                //if we are not currently jumping
                else if(m_entityData.MovementState == EntityMovementState.FALLING)
                {
                    m_entityData.MovementState = EntityMovementState.GROUNDED;
                }
        }
        

        public EntityData EntityData
        {
            get { return m_entityData; }
            set { m_entityData = value; }
        }
    }
}