namespace V1
{
    using UnityEngine;

    public class EnemyMovementController : MonoBehaviour
    {
        private Entity m_entity;

        [SerializeField]
        private EnemyData m_enemyData = null;

        [SerializeField]
        private PatrolData m_patrolData = null;

        private Bounds m_bounds;
        
        
        private void Awake() 
        {
            BindObjects();
        }

        private void Update()
        {
            UpdateVelocity();

            if(m_entity.Ground != null)
            {
                Patrol();
                Detect();
            }

            Move();
        }

        private void BindObjects()
        {
            m_entity = GetComponent<Entity>();
            m_bounds = GetComponent<Bounds>();
        }

        private void UpdateVelocity()
        {
            //Update Vertical Velocity If Falling
            if(m_entity.MovementState == EntityMovementState.FALLING)
            {
                //Debug.Log(m_playerData.VerticalSpeed);
                if(m_enemyData.VerticalSpeed > -m_enemyData.MaxFallSpeed)
                    m_enemyData.VerticalSpeed -= m_entity.GameManagerRef.GravityScale * Time.deltaTime;
            }
            else if(m_entity.MovementState != EntityMovementState.JUMPING)
            {
                m_enemyData.VerticalSpeed  = 0.0f;
            }

            //Update Velocity Direction in entity depending on the speed
            if(m_enemyData.VerticalSpeed > 0)
                m_entity.VerticalDirection = EntityVerticalDirection.RISING;
            else if(m_enemyData.VerticalSpeed < 0)
                m_entity.VerticalDirection = EntityVerticalDirection.FALLING;
            else
                m_entity.VerticalDirection = EntityVerticalDirection.NONE;

        }
        
        private void Patrol()
        {
            float distance = (m_entity.HorizontalDirection == EntityHorizontalDirection.RIGHT) ? Mathf.Abs(m_entity.Ground.GetComponent<Bounds>().EntityHorizontalBounds[1] - m_bounds.EntityHorizontalBounds[1]) : Mathf.Abs(m_entity.Ground.GetComponent<Bounds>().EntityHorizontalBounds[0] - m_bounds.EntityHorizontalBounds[0]); 
            m_enemyData.Direction = (m_entity.HorizontalDirection == EntityHorizontalDirection.RIGHT) ? 1.0f : -1.0f;
            //Control Direction
            if(distance < m_patrolData.LedgeDistance)
            {
                if(m_entity.HorizontalDirection == EntityHorizontalDirection.RIGHT)
                {
                    m_entity.HorizontalDirection = EntityHorizontalDirection.LEFT;
                }
                else
                {
                    m_entity.HorizontalDirection = EntityHorizontalDirection.RIGHT;
                }
            }
        }

        private void Detect()
        {
            
        }

        private void Move()
        {
            transform.position += new Vector3(m_enemyData.MovementSpeed * m_enemyData.Direction * Time.deltaTime, m_enemyData.VerticalSpeed  * Time.deltaTime);
        }
    }
}