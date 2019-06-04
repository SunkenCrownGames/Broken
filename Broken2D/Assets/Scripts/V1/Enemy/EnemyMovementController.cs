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


        private int m_playerLayer = 0;

        private Bounds m_bounds;

        [SerializeField]
        private static GameObject m_player;
        
        
        private void Awake() 
        {
            BindObjects();
        }

        private void Update()
        {
            UpdateVelocity();

            if(m_entity.Ground != null)
            {
                if(m_enemyData.EnemyInfo == EnemyType.PATROL)
                {
                    if(m_patrolData.CurrentStatus != PatrolStatus.CHASING)
                        Patrol();
                    else
                        Chase();

                    Detect();
                }
            }

            Move();
        }

        private void BindObjects()
        {
            m_entity = GetComponent<Entity>();
            m_bounds = GetComponent<Bounds>();
            m_playerLayer = LayerMask.GetMask("PLAYER");
            m_player = GameObject.FindGameObjectWithTag("Player");
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

        private void Chase()
        {
            float xPlayerPos = m_player.transform.position.x;
            float xCurrentPos = transform.position.x;
            float distance = Mathf.Abs(xCurrentPos - xPlayerPos);

            GameObject m_playerGround = m_player.GetComponent<Entity>().Ground;

            if(distance > m_enemyData.AttackRange)
            {
                m_entity.HorizontalDirection = (xCurrentPos < xPlayerPos) ? EntityHorizontalDirection.RIGHT : EntityHorizontalDirection.LEFT;
                m_enemyData.Direction = (xCurrentPos < xPlayerPos) ? 1 : -1;
            }
            else
            {
                m_enemyData.Direction = 0;
            }

            if(m_entity.Ground == m_playerGround && m_playerGround != null)
            {
                m_patrolData.CurrentStatus = PatrolStatus.PATROLLING;
            }
        }

        private void Detect()
        {
            Debug.DrawLine(transform.position, transform.position + new Vector3(m_enemyData.DetectionRange, 0 ,0), Color.red);
            RaycastHit2D hit = (m_entity.HorizontalDirection == EntityHorizontalDirection.RIGHT) ? 
            Physics2D.Raycast(transform.position, Vector2.right, m_enemyData.DetectionRange, m_playerLayer) :
            Physics2D.Raycast(transform.position, -Vector2.right, m_enemyData.DetectionRange, m_playerLayer);
            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                m_patrolData.CurrentStatus = PatrolStatus.CHASING;
            }
        }

        private void Move()
        {
            transform.position += new Vector3(m_enemyData.MovementSpeed * m_enemyData.Direction * Time.deltaTime, m_enemyData.VerticalSpeed  * Time.deltaTime);
        }
    }
}