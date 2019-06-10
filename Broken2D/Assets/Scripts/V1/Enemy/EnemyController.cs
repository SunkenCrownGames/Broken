namespace V1
{
    using UnityEngine;

    public class EnemyController : MonoBehaviour
    {
        private Entity m_entity;

        [SerializeField]
        private EnemyData m_enemyData = null;

        [SerializeField]
        private EnemyPatrolData m_patrolData = null;

        [SerializeField]
        private EnemySeekData m_seekData = null;


        private int m_playerLayer = 0;

        private Bounds m_bounds;

        [SerializeField]
        private static GameObject m_player;


        private void Awake()
        {
            BindObjects();

            if(m_enemyData.EnemyType == EnemyType.SEEK)
                InitializeSeek();
        }

        private void Update()
        {
            if (m_enemyData.EnemyType == EnemyType.PATROL)
            {
                UpdateVelocity();

                if (m_entity.Ground != null)
                {
                    if (m_patrolData.CurrentStatus != PatrolStatus.CHASING)
                    {
                        Detect();
                        Patrol();
                    }
                    else
                    {
                        Chase();
                        Debug.Log("Chasing");
                    }
                }
            }
            else
            {
                if(m_entity.PlayerRef != null)
                    Seek();
            }

            Move();
        }

        private void BindObjects()
        {
            m_entity = GetComponent<Entity>();
            m_bounds = GetComponent<Bounds>();
            m_playerLayer = LayerMask.GetMask("PLAYER");
            m_player = GameObject.FindGameObjectWithTag("Player");
            m_seekData.RandomizerData.GenerateRandomizedData();
        }

        #region Patrol
        private void UpdateVelocity()
        {
            //Update Vertical Velocity If Falling
            if (m_entity.MovementState == EntityMovementState.FALLING)
            {
                //Debug.Log(m_playerData.VerticalSpeed);
                if (m_enemyData.VerticalSpeed > -m_enemyData.MaxFallSpeed)
                    m_enemyData.VerticalSpeed -= m_entity.GameManagerRef.GravityScale * Time.deltaTime;
            }
            else if (m_entity.MovementState != EntityMovementState.JUMPING)
            {
                m_enemyData.VerticalSpeed = 0.0f;
            }

            //Update Velocity Direction in entity depending on the speed
            if (m_enemyData.VerticalSpeed > 0)
                m_entity.VerticalDirection = EntityVerticalDirection.RISING;
            else if (m_enemyData.VerticalSpeed < 0)
                m_entity.VerticalDirection = EntityVerticalDirection.FALLING;
            else
                m_entity.VerticalDirection = EntityVerticalDirection.NONE;

        }

        private void Patrol()
        {
            float distance = (m_entity.HorizontalDirection == EntityHorizontalDirection.RIGHT) ? Mathf.Abs(m_entity.Ground.GetComponent<Bounds>().EntityHorizontalBounds[1] - m_bounds.EntityHorizontalBounds[1]) : Mathf.Abs(m_entity.Ground.GetComponent<Bounds>().EntityHorizontalBounds[0] - m_bounds.EntityHorizontalBounds[0]);
            m_enemyData.Direction = (m_entity.HorizontalDirection == EntityHorizontalDirection.RIGHT) ? 1.0f : -1.0f;
            
            
            //Control Direction
            if (distance < m_patrolData.LedgeDistance)
            {
                if (m_entity.HorizontalDirection == EntityHorizontalDirection.RIGHT)
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

            float range = (m_patrolData.EnemyAttackType == EnemyPatrolAttackType.MELEE) ? m_patrolData.MeleeAttackRange : m_patrolData.RangedAttackRange;

            Debug.Log("The Current Distance Is: " + distance + "\t Range = " + range);

            if (distance > range)
            {
                m_entity.HorizontalDirection = (xCurrentPos < xPlayerPos) ? EntityHorizontalDirection.RIGHT : EntityHorizontalDirection.LEFT;
                m_enemyData.Direction = (xCurrentPos < xPlayerPos) ? 1 : -1;

            }
            else
            {
                m_enemyData.Direction = 0;
            }

            if (m_entity.Ground != m_playerGround && m_playerGround != null)
            {
                m_patrolData.CurrentStatus = PatrolStatus.PATROLLING;
            }
        }

        private void Detect()
        {
            Debug.DrawLine(transform.position, transform.position + new Vector3(m_enemyData.DetectionRange, 0, 0), Color.red);
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
                transform.position += new Vector3(m_enemyData.MovementSpeed * m_enemyData.Direction * Time.deltaTime, m_enemyData.VerticalSpeed * Time.deltaTime);
        }
        #endregion


        private void InitializeSeek()
        {
            if(m_seekData.SeekType == EnemySeekMovementType.BEZIER)
                InitializeBezierSeek();
        }

        private void InitializeBezierSeek()
        {
            m_seekData.StartPos = transform.position;
            //m_midPoint = (m_player.transform.position + m_startPos) / 2;
            m_seekData.MidPoint = (new Vector3(m_player.transform.position.x, 0, 0) + m_seekData.StartPos) / 2;
        }

        private void Seek()
        {
            Vector3 direction = m_player.transform.position - transform.position;

            switch(m_seekData.SeekType)
            {
                case EnemySeekMovementType.STRAIGHT:
                    StraightMovement();
                break;
                case EnemySeekMovementType.BEZIER:
                    BezierMovement();
                break;
            }


            if(m_seekData.TrigWaveToggle)
            {
                float val = (m_seekData.Type == WaveType.SIN) ? Mathf.Sin(Time.time * m_seekData.RandomizerData.SinWaveSpeedRandomized) : Mathf.Cos(Time.time * m_seekData.RandomizerData.SinWaveSpeedRandomized);
                if(m_seekData.RandomizedStatus)
                    transform.position += transform.up * val * m_enemyData.Magnitude;
                else
                    transform.position += transform.up * val * m_enemyData.Magnitude;
            }
        }
        private void StraightMovement()
        {
            float deltaSpeed = m_enemyData.MovementSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, m_player.transform.position, deltaSpeed);

            Vector3 heading =  m_player.transform.position - transform.position;;
            Vector3 direction = heading / heading.magnitude;
            //Increase the Vertical Speed 

            if(m_seekData.RandomizedStatus)
                transform.position += new Vector3(direction.x * m_seekData.RandomizerData.HorinzontalSpeedRandomized * Time.deltaTime, direction.y * m_seekData.RandomizerData.VerticalSpeedRandomized * Time.deltaTime, 0);
            else
                transform.position += new Vector3(direction.x * m_seekData.HorizontalSpeed * Time.deltaTime, direction.y * m_seekData.VerticalSpeed * Time.deltaTime, 0);
        }

        private void BezierMovement()
        {
            m_seekData.CurrentDuration += Time.deltaTime;
            m_seekData.MidPoint = (m_player.transform.position + m_seekData.StartPos) / 2;
            //m_midPoint = (new Vector3(m_player.transform.position.x, 0, 0) + m_startPos) / 2;
            transform.position = BezierCurve.CalculateBezierPoint(m_seekData.CurrentDuration / m_seekData.Duration, m_seekData.StartPos, m_seekData.MidPoint, m_player.transform.position);
        }

        



        public EnemyData EnemyInfo
        {
            get { return m_enemyData; }
            set { m_enemyData = value; }
        }

        public EnemyPatrolData PatrolData
        {
            get { return m_patrolData; }
            set { m_patrolData = value; }
        }
    }
}