namespace V2
{
    using System;
    using UnityEngine;
    //using Newtonsoft.Json;

    public class EnemyEntity : Entity
    {

        [Header("Enemy File Settings")]
        [SerializeField]
        private bool m_enemyloadFromFile;

        [SerializeField]
        private TextAsset m_enemyDataFile;

        [Header("Enemy Data Settings")]
        [SerializeField]
        private EnemyData m_enemyData = null;

        private EnemyLogicData m_enemyLogicData;

        protected override void Awake()
        {
            base.Awake();
            LoadData();
            BindObjects();

            if (m_enemyData.EnemyType == EnemyType.SEEK)
                InitializeSeek();
        }

        private void LoadData()
        {
            if(m_enemyDataFile != null && m_enemyloadFromFile)
            {
                //EnemyData data = JsonConvert.DeserializeObject<EnemyData>(m_enemyDataFile.text);
                //m_enemyData = data;
                Debug.Log("Enemy Data Loaded From File");
            }
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

                    if (m_entityLogicData.GroundObject != null)
                    {
                        if (m_enemyData.PatrolData.CurrentStatus != PatrolStatus.CHASING)
                        {
                            Detect();
                            Patrol();
                        }
                        else
                        {
                            Chase();
                            //Debug.Log("Chasing");
                        }
                    }
                }
                else
                {
                    if (m_enemyLogicData.Player != null)
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
            if(m_enemyLogicData == null)
                m_enemyLogicData = new EnemyLogicData();

            m_entityLogicData.Bounds = GetComponent<Bounds>();
            m_enemyLogicData.PlayerLayer = LayerMask.GetMask("PLAYER");
            if(m_enemyLogicData.Player == null)
              m_enemyLogicData.Player = GameObject.FindGameObjectWithTag("Player");
            m_enemyData.SeekData.RandomizerData.GenerateRandomizedData();

            if(m_entityLogicData.GM == null)
                m_entityLogicData.GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        #region Patrol
        private void UpdateVelocity()
        {
            //Update Vertical Velocity If Falling
            if (m_entityData.MovementState == EntityMovementState.FALLING)
            {
                //Debug.Log(m_playerData.VerticalSpeed);
                if (m_enemyData.MovementData.VerticalSpeed > -m_enemyData.MovementData.MaxFallSpeed)
                    m_enemyData.MovementData.VerticalSpeed -= m_entityLogicData.GM.GravityScale * Time.deltaTime;
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
            Mathf.Abs(m_entityLogicData.GroundObject.GetComponent<Bounds>().EntityHorizontalBounds[1] - m_entityLogicData.Bounds.EntityHorizontalBounds[1]) : 
            Mathf.Abs(m_entityLogicData.GroundObject.GetComponent<Bounds>().EntityHorizontalBounds[0] - m_entityLogicData.Bounds.EntityHorizontalBounds[0]);
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
            float xPlayerPos = m_enemyLogicData.Player.transform.position.x;
            float xCurrentPos = transform.position.x;
            float distance = Mathf.Abs(xCurrentPos - xPlayerPos);

            GameObject m_playerGround = m_enemyLogicData.Player.GetComponent<PlayerEntity>().LogicData.GroundObject;

            Bounds groundBounds = m_entityLogicData.GroundObject.GetComponent<Bounds>();

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

                if (m_entityLogicData.GroundObject != m_playerGround && m_playerGround != null)
                {
                    m_enemyData.PatrolData.CurrentStatus = PatrolStatus.PATROLLING;
                }

        }

        private bool CheckChaseLedge()
        {
            //Debug.Log(m_entityLogicData.GroundObject.name);
            Bounds groundBounds = m_entityLogicData.GroundObject.GetComponent<Bounds>();
            float boundsDistance = (m_entityData.HDirection == HorizontalDirection.LEFT) ? Mathf.Abs(m_entityLogicData.Bounds.EntityHorizontalBounds[0] - groundBounds.EntityHorizontalBounds[0]) :
            Mathf.Abs(m_entityLogicData.Bounds.EntityHorizontalBounds[1] - groundBounds.EntityHorizontalBounds[1]);

            if(boundsDistance > m_enemyData.PatrolData.LedgeStopDistance)
                return true;
            else
                return false;
        }

        private void Detect()
        {
            Debug.DrawLine(transform.position, transform.position + new Vector3(m_enemyData.PatrolData.DetectionRange, 0, 0), Color.red);
            RaycastHit2D hit = (m_entityData.HDirection == HorizontalDirection.RIGHT) ?
            Physics2D.Raycast(transform.position, Vector2.right, m_enemyData.PatrolData.DetectionRange, m_enemyLogicData.PlayerLayer) :
            Physics2D.Raycast(transform.position, -Vector2.right, m_enemyData.PatrolData.DetectionRange, m_enemyLogicData.PlayerLayer);
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
            m_enemyData.SeekData.StartPos = Vec3.ToVec3(transform.position);
            //m_midPoint = (m_player.transform.position + m_startPos) / 2;
             m_enemyData.SeekData.MidPoint = (new Vector3(m_enemyLogicData.Player.transform.position.x, 0, 0) + m_enemyData.SeekData.StartPos) / 2;
        }

        
        private void Seek()
        {
            Vector3 direction = m_enemyLogicData.Player.transform.position - transform.position;

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
            transform.position = Vector2.MoveTowards(transform.position, m_enemyLogicData.Player.transform.position, deltaSpeed);

            Vector3 heading = m_enemyLogicData.Player.transform.position - transform.position; ;
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
            m_enemyData.SeekData.MidPoint = (m_enemyLogicData.Player.transform.position + m_enemyData.SeekData.StartPos) / 2;
            //m_midPoint = (new Vector3(m_player.transform.position.x, 0, 0) + m_startPos) / 2;
            transform.position = BezierCurve.CalculateBezierPoint(m_enemyData.SeekData.CurrentDuration / m_enemyData.SeekData.Duration, m_enemyData.SeekData.StartPos.ToVector3(), m_enemyData.SeekData.MidPoint.ToVector3(), m_enemyLogicData.Player.transform.position);
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

        public string SerializedEnemyData()
        {
            //return JsonConvert.SerializeObject(m_enemyData);
            return "NULL";
        }
    }
}