namespace V2
{
    using System;
    using UnityEngine;

    public class EnemyEntity : Entity
    {
        [SerializeField]
        private EnemyData m_enemyData = null;

        protected override void Awake()
        {
            base.Awake();
            BindObjects();

            if (m_enemyData.EnemyType == EnemyType.SEEK)
                InitializeSeek();
        }

        protected override void Update()
        {
            base.Update();
            
            //IF ENEMY WAS NOT HIT
            if(m_entityData.ActionState != EntityActionState.HIT)
            {
                UpdateVelocity();
                if (m_enemyData.EnemyType == EnemyType.PATROL)
                {

                    if (m_entityData.Ground != null)
                    {
                        if (m_enemyData.PatrolData.CurrentStatus != PatrolStatus.CHASING)
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
                    if (m_enemyData.Player != null)
                        Seek();
                }
            }
            else
            {
                KnockBack();
            }

            Move();

        }
        private void BindObjects()
        {
            m_entityData.Bounds = GetComponent<Bounds>();
            m_enemyData.PlayerLayer = LayerMask.GetMask("PLAYER");
            if(m_enemyData.Player == null)
              m_enemyData.Player = GameObject.FindGameObjectWithTag("Player");
            m_enemyData.SeekData.RandomizerData.GenerateRandomizedData();
        }

        #region Patrol
        private void UpdateVelocity()
        {
            //Update Vertical Velocity If Falling
            if (m_entityData.MovementState == EntityMovementState.FALLING)
            {
                //Debug.Log(m_playerData.VerticalSpeed);
                if (m_enemyData.MovementData.VerticalSpeed > -m_enemyData.MovementData.MaxFallSpeed)
                    m_enemyData.MovementData.VerticalSpeed -= m_entityData.GM.GravityScale * Time.deltaTime;
            }
            else if (m_entityData.MovementState != EntityMovementState.JUMPING)
            {
                m_enemyData.MovementData.VerticalSpeed = 0.0f;
            }

            //Update Velocity Direction in entity depending on the speed
            if (m_enemyData.MovementData.VerticalSpeed > 0)
                m_entityData.VDirection = VerticalDirection.UP;
            else if (m_enemyData.MovementData.VerticalSpeed < 0)
                m_entityData.VDirection = VerticalDirection.DOWN;
            else
                m_entityData.VDirection = VerticalDirection.NONE;

            if(m_enemyData.PatrolData.CurrentStatus == PatrolStatus.PATROLLING || 
            (m_enemyData.PatrolData.CurrentStatus == PatrolStatus.CHASING && CheckChaseLedge()))
            {
                if(m_enemyData.MovementData.MovementSpeed >= m_enemyData.MovementData.MaxMovementSpeed)
                {
                    m_enemyData.MovementData.MovementSpeed -= m_enemyData.MovementData.HorizontalAcceleration * Time.deltaTime;
                }
                else
                {
                        m_enemyData.MovementData.MovementSpeed += m_enemyData.MovementData.HorizontalAcceleration * Time.deltaTime;
                }
            }
            else
            {
                m_enemyData.MovementData.MovementSpeed = 0;
            }
        }

        private void Patrol()
        {
            float distance = (m_entityData.HDirection == HorizontalDirection.RIGHT) ? 
            Mathf.Abs(m_entityData.Ground.GetComponent<Bounds>().EntityHorizontalBounds[1] - m_entityData.Bounds.EntityHorizontalBounds[1]) : 
            Mathf.Abs(m_entityData.Ground.GetComponent<Bounds>().EntityHorizontalBounds[0] - m_enemyData.Bounds.EntityHorizontalBounds[0]);
            m_enemyData.MovementData.HorizontalDirection = (m_entityData.HDirection == HorizontalDirection.RIGHT) ? 1.0f : -1.0f;


            //Control Direction
            if (distance < m_enemyData.PatrolData.LedgeDistance)
            {
                if (m_entityData.HDirection == HorizontalDirection.RIGHT)
                {
                    m_entityData.HDirection = HorizontalDirection.LEFT;
                }
                else
                {
                    m_entityData.HDirection = HorizontalDirection.RIGHT;
                }
            }
        }

        private void Chase()
        {
            float xPlayerPos = m_enemyData.Player.transform.position.x;
            float xCurrentPos = transform.position.x;
            float distance = Mathf.Abs(xCurrentPos - xPlayerPos);

            GameObject m_playerGround = m_enemyData.Player.GetComponent<PlayerEntity>().EntityData.Ground;

            Bounds groundBounds = m_entityData.Ground.GetComponent<Bounds>();

            float groundEdge = (m_entityData.HDirection == HorizontalDirection.LEFT) ? groundBounds.EntityHorizontalBounds[0] : groundBounds.EntityHorizontalBounds[1];

            float range = (m_enemyData.PatrolData.EnemyAttackType == EnemyPatrolAttackType.MELEE) ? m_enemyData.PatrolData.MeleeAttackRange : m_enemyData.PatrolData.RangedAttackRange;

            //Debug.Log("The Current Distance Is: " + distance + "\t Range = " + range);


                if (distance > range)
                {
                    m_entityData.HDirection = (xCurrentPos < xPlayerPos) ? HorizontalDirection.RIGHT : HorizontalDirection.LEFT;
                    m_enemyData.MovementData.HorizontalDirection = (xCurrentPos < xPlayerPos) ? 1 : -1;

                }
                else
                {
                    m_enemyData.MovementData.HorizontalDirection = 0;
                }

                if (m_entityData.Ground != m_playerGround && m_playerGround != null)
                {
                    m_enemyData.PatrolData.CurrentStatus = PatrolStatus.PATROLLING;
                }

        }

        private bool CheckChaseLedge()
        {
            Debug.Log(m_entityData.Ground.name);
            Bounds groundBounds = m_entityData.Ground.GetComponent<Bounds>();
            float boundsDistance = (m_entityData.HDirection == HorizontalDirection.LEFT) ? Mathf.Abs(m_entityData.Bounds.EntityHorizontalBounds[0] - groundBounds.EntityHorizontalBounds[0]) :
            Mathf.Abs(m_entityData.Bounds.EntityHorizontalBounds[1] - groundBounds.EntityHorizontalBounds[1]);

            if(boundsDistance > m_enemyData.PatrolData.LedgeStopDistance)
                return true;
            else
                return false;
        }

        private void Detect()
        {
            Debug.DrawLine(transform.position, transform.position + new Vector3(m_enemyData.PatrolData.DetectionRange, 0, 0), Color.red);
            RaycastHit2D hit = (m_entityData.HDirection == HorizontalDirection.RIGHT) ?
            Physics2D.Raycast(transform.position, Vector2.right, m_enemyData.PatrolData.DetectionRange, m_enemyData.PlayerLayer) :
            Physics2D.Raycast(transform.position, -Vector2.right, m_enemyData.PatrolData.DetectionRange, m_enemyData.PlayerLayer);
            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                m_enemyData.PatrolData.CurrentStatus = PatrolStatus.CHASING;
            }
        }
        #endregion

        #region Seeker Logic
        private void InitializeSeek()
        {
            if (m_enemyData.SeekData.SeekType == EnemySeekMovementType.BEZIER)
                InitializeBezierSeek();
        }

        private void InitializeBezierSeek()
        {
            m_enemyData.SeekData.StartPos = transform.position;
            //m_midPoint = (m_player.transform.position + m_startPos) / 2;
             m_enemyData.SeekData.MidPoint = (new Vector3(m_enemyData.Player.transform.position.x, 0, 0) + m_enemyData.SeekData.StartPos) / 2;
        }

        
        private void Seek()
        {
            Vector3 direction = m_enemyData.Player.transform.position - transform.position;

            switch (m_enemyData.SeekData.SeekType)
            {
                case EnemySeekMovementType.STRAIGHT:
                    StraightMovement();
                    break;
                case EnemySeekMovementType.BEZIER:
                    BezierMovement();
                    break;
            }


            if (m_enemyData.SeekData.TrigWaveToggle)
            {
                float val = (m_enemyData.SeekData.Type == WaveType.SIN) ? Mathf.Sin(Time.time * m_enemyData.SeekData.RandomizerData.SinWaveSpeedRandomized) : Mathf.Cos(Time.time * m_enemyData.SeekData.RandomizerData.SinWaveSpeedRandomized);
                if (m_enemyData.SeekData.RandomizedStatus)
                    transform.position += transform.up * val * m_enemyData.SeekData.Magnitude;
                else
                    transform.position += transform.up * val * m_enemyData.SeekData.Magnitude;
            }
        }

        private void StraightMovement()
        {
            float deltaSpeed = m_enemyData.MovementData.MovementSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, m_enemyData.Player.transform.position, deltaSpeed);

            Vector3 heading = m_enemyData.Player.transform.position - transform.position; ;
            Vector3 direction = heading / heading.magnitude;
            //Increase the Vertical Speed 

            if (m_enemyData.SeekData.RandomizedStatus)
                transform.position += new Vector3(direction.x * m_enemyData.SeekData.RandomizerData.HorinzontalSpeedRandomized * Time.deltaTime, direction.y * m_enemyData.SeekData.RandomizerData.VerticalSpeedRandomized * Time.deltaTime, 0);
            else
                transform.position += new Vector3(direction.x * m_enemyData.SeekData.HorizontalSpeed * Time.deltaTime, direction.y * m_enemyData.SeekData.VerticalSpeed * Time.deltaTime, 0);
        }

        private void BezierMovement()
        {
            m_enemyData.SeekData.CurrentDuration += Time.deltaTime;
            m_enemyData.SeekData.MidPoint = (m_enemyData.Player.transform.position + m_enemyData.SeekData.StartPos) / 2;
            //m_midPoint = (new Vector3(m_player.transform.position.x, 0, 0) + m_startPos) / 2;
            transform.position = BezierCurve.CalculateBezierPoint(m_enemyData.SeekData.CurrentDuration / m_enemyData.SeekData.Duration, m_enemyData.SeekData.StartPos, m_enemyData.SeekData.MidPoint, m_enemyData.Player.transform.position);
        }
        

        #endregion


        private void KnockBack()
        {
            float direction = (m_entityData.HitDirection == HorizontalDirection.LEFT) ? 1 : -1;
            
            if(m_entityData.KnockBackData.CurrentDuration < m_entityData.KnockBackData.Duration)
            {
                m_entityData.KnockBackData.CurrentDuration += Time.deltaTime;
                transform.position += new Vector3(m_entityData.KnockBackData.HorizontalKnockBack * direction * Time.deltaTime, 0 , 0);
            }
            else
            {
                m_entityData.ActionState = EntityActionState.NONE;
                m_enemyData.MovementData.MovementSpeed = m_entityData.KnockBackData.HorizontalKnockBack * direction;
                m_entityData.KnockBackData.CurrentDuration = 0;
            }
        }


        private void Move()
        {
                transform.position += new Vector3(m_enemyData.MovementData.MovementSpeed * m_enemyData.MovementData.HorizontalDirection * Time.deltaTime, m_enemyData.MovementData.VerticalSpeed * Time.deltaTime);
        }

        public EnemyData EnemyData
        {
            get { return m_enemyData; }
            set { m_enemyData = value; }
        }
    }
}