namespace V2
{
    using UnityEngine;
    
    public class PlayerEntity : Entity
    {
        [SerializeField]
        private PlayerData m_playerData;

        protected override void Awake() 
        {
            base.Awake();

            if(m_playerData == null)
                m_playerData = new PlayerData();
        }

        protected override void Update()
        {
            base.Update();
            UpdateInput();
            UpdateVelocity();

            Jump();
            DashTrigger();

            if(m_entityData.ActionState != EntityActionState.HIT)
            {
                if(m_playerData.MovementState != PlayerMovementState.DASHING)
                    Move();
                else
                    Dash();
            }
            else
            {
                KnockBack();
            }
        }

        private void UpdateInput()
        {
            m_playerData.InputData.HorizontalInput = Input.GetAxisRaw("Horizontal");
        }

        private void UpdateVelocity()
        {
            //Update Vertical Velocity If Falling
            if(m_entityData.MovementState == EntityMovementState.FALLING)
            {
                //Debug.Log(m_playerData.VerticalSpeed);
                if(m_playerData.MovementData.VerticalSpeed > -m_playerData.MovementData.MaxFallSpeed)
                    m_playerData.MovementData.VerticalSpeed -= m_entityData.GM.GravityScale * Time.deltaTime;
            }
            else if(m_entityData.MovementState != EntityMovementState.JUMPING)
            {
                m_playerData.MovementData.VerticalSpeed  = 0.0f;
            }

            //Update Vertical Velocity If Jumping
            if(m_entityData.MovementState == EntityMovementState.JUMPING)
            {
                float yPos = transform.position.y;
                if(yPos > m_playerData.JumpData.JumpDestination)
                    m_entityData.MovementState = EntityMovementState.FALLING;
            }

            //Update Velocity Direction in entity depending on the speed
            if(m_playerData.MovementData.VerticalSpeed > 0)
                m_entityData.VDirection = VerticalDirection.UP;
            else if(m_playerData.MovementData.VerticalSpeed < 0)
                m_entityData.VDirection = VerticalDirection.DOWN;
            else
                m_entityData.VDirection = VerticalDirection.NONE;


            //Update Horizontal Velocity
            if(m_playerData.InputData.HorizontalInput == 1)
            {
                if(m_playerData.MovementData.MovementSpeed < m_playerData.MovementData.MaxMovementSpeed)
                    m_playerData.MovementData.MovementSpeed += m_playerData.MovementData.HorizontalAccelerationSpeed * Time.deltaTime;
            }
            else if(m_playerData.InputData.HorizontalInput == -1)
            {
                if(m_playerData.MovementData.MovementSpeed > -m_playerData.MovementData.MaxMovementSpeed)
                    m_playerData.MovementData.MovementSpeed -= m_playerData.MovementData.HorizontalAccelerationSpeed * Time.deltaTime;
            }
            else
            {
                if(m_playerData.MovementData.MovementSpeed > m_playerData.InputData.MovemetnDeadZone)
                    m_playerData.MovementData.MovementSpeed -= m_playerData.MovementData.HorizontalDecelerationSpeed * Time.deltaTime;
                else if(m_playerData.MovementData.MovementSpeed < -m_playerData.InputData.MovemetnDeadZone)
                    m_playerData.MovementData.MovementSpeed += m_playerData.MovementData.HorizontalDecelerationSpeed * Time.deltaTime;
                else
                    m_playerData.MovementData.MovementSpeed = 0.0f;
            }
        }


        private void Jump()
        {
            if(Input.GetKeyDown(KeyCode.Space) && m_entityData.MovementState != EntityMovementState.JUMPING)
            {
                m_entityData.MovementState = EntityMovementState.JUMPING;
                m_playerData.JumpData.JumpDestination = transform.position.y + m_playerData.MovementData.JumpHeight;
                m_playerData.JumpData.InitialPosition = transform.position.y;
                m_playerData.MovementData.VerticalSpeed = m_playerData.MovementData.JumpSpeed;
                m_entityData.GM.CurrencyEvent.Invoke(-m_playerData.CostData.JumpCost);
            }
        }


        private void DashTrigger()
        {
            if(m_playerData.MovementState != PlayerMovementState.DASHING)
            {
                if(Input.GetKeyDown(KeyCode.LeftShift))
                {
                    m_playerData.MovementState = PlayerMovementState.DASHING;
                    
                    float direction = (m_entityData.HDirection == HorizontalDirection.LEFT) ? -1 : 1.0f;
                    m_playerData.MovementData.DashInfo.DashDestination = transform.position.x + (m_playerData.MovementData.DashInfo.DashLength * direction);
                    m_entityData.GM.CurrencyEvent.Invoke(-m_playerData.CostData.DashCost);
                }
            }
        }

        private void Move() 
        {
                //Debug.Log(m_vVelocity);
                transform.position += new Vector3(m_playerData.MovementData.MovementSpeed * Time.deltaTime, m_playerData.MovementData.VerticalSpeed  * Time.deltaTime);

                UpdateMovementState();

                //If we are moving then deplete from energy
                if(m_playerData.MovementState == PlayerMovementState.RUNNING)
                    m_entityData.GM.CurrencyEvent.Invoke(-m_playerData.CostData.MovementCost);

        }

        private void Dash()
        {
            //Debug.Log("Dashing");
            
            float xPos = transform.position.x;

            float horizontalSpeed = (m_entityData.HDirection == HorizontalDirection.LEFT) ? -m_playerData.MovementData.DashInfo.DashSpeed : m_playerData.MovementData.DashInfo.DashSpeed;

            transform.position += new Vector3(horizontalSpeed * Time.deltaTime, m_playerData.MovementData.VerticalSpeed  * Time.deltaTime);

            if(m_entityData.HDirection == HorizontalDirection.LEFT)
            {
               if(xPos < m_playerData.MovementData.DashInfo.DashDestination)
               {
                   //Debug.Log("Dash Ended");
                   m_playerData.MovementState = (m_playerData.InputData.HorizontalInput != 0) ? PlayerMovementState.RUNNING : PlayerMovementState.IDLE;
               } 
            }
            else
            {
                if(xPos > m_playerData.MovementData.DashInfo.DashDestination)
                {
                    Debug.Log("Dash Ended");
                    m_playerData.MovementState = (m_playerData.InputData.HorizontalInput != 0) ? PlayerMovementState.RUNNING : PlayerMovementState.IDLE;
                } 
            }

        }

        private void UpdateMovementState()
        {
            if(m_playerData.InputData.HorizontalInput != 0)
            {
                m_playerData.MovementState = PlayerMovementState.RUNNING;
                if(m_playerData.InputData.HorizontalInput == 1)
                    m_entityData.HDirection = HorizontalDirection.RIGHT;
                else
                    m_entityData.HDirection = HorizontalDirection.LEFT;
            }
            else
                m_playerData.MovementState = PlayerMovementState.IDLE;  
        }

        private void KnockBack()
        {
            float direction = (m_entityData.HitDirection == HorizontalDirection.LEFT) ? 1 : -1;
            
            if(m_entityData.KnockBackData.CurrentDuration < m_entityData.KnockBackData.Duration)
            {
                m_entityData.KnockBackData.CurrentDuration += Time.deltaTime;
            
                if(!m_entityData.KnockBackData.VerticalKnockbackTrigger)
                    transform.position += new Vector3(m_entityData.KnockBackData.HorizontalKnockBack * direction * Time.deltaTime, 
                    0, 0);
                else
                    transform.position += new Vector3(m_entityData.KnockBackData.HorizontalKnockBack * direction * Time.deltaTime, 
                        m_entityData.KnockBackData.VerticalKnockBack  * Time.deltaTime , 0);
            }
            else
            {
                m_entityData.ActionState = EntityActionState.NONE;
                m_playerData.MovementData.MovementSpeed = m_entityData.KnockBackData.HorizontalKnockBack * direction;
                m_entityData.KnockBackData.CurrentDuration = 0;

                if(m_entityData.KnockBackData.VerticalKnockbackTrigger)
                    m_playerData.MovementData.VerticalSpeed = m_entityData.KnockBackData.VerticalKnockBack;
            }
        }

        public PlayerData PlayerData
        {
            get { return m_playerData; }
        }
    }
}