using UnityEngine;

namespace V1
{
    public class PlayerController : MonoBehaviour 
    {

        [SerializeField]
        private PlayerMovementState m_playerMovementState;

        public PlayerData m_playerData;

        private Entity m_entity;

        private float m_horizontalInput = 0.0f;
        private float m_jumpDestination = 0.0f;
        private float m_initialPosition = 0.0f;
        private void Awake() 
        {
            BindObjects();
        }
        private void Start() 
        {
            
        }

        private void Update() 
        {
            UpdateInput();
            UpdateVelocity();
            Jump();
            Move();
        }

        private void BindObjects()
        {
            m_entity = GetComponent<Entity>();
        }

        private void UpdateInput()
        {
            m_horizontalInput = Input.GetAxisRaw("Horizontal");
        }

        private void UpdateVelocity()
        {
            //Update Vertical Velocity If Falling
            if(m_entity.MovementState == EntityMovementState.FALLING)
            {
                //Debug.Log(m_playerData.VerticalSpeed);
                if(m_playerData.VerticalSpeed > -m_playerData.MaxFallSpeed)
                    m_playerData.VerticalSpeed -= m_entity.GameManagerRef.GravityScale * Time.deltaTime;
            }
            else if(m_entity.MovementState != EntityMovementState.JUMPING)
            {
                m_playerData.VerticalSpeed  = 0.0f;
            }

            //Update Vertical Velocity If Jumping
            if(m_entity.MovementState == EntityMovementState.JUMPING)
            {
                float yPos = transform.position.y;
                if(yPos > m_jumpDestination)
                    m_entity.MovementState = EntityMovementState.FALLING;
            }

            //Update Velocity Direction in entity depending on the speed
            if(m_playerData.VerticalSpeed > 0)
                m_entity.VerticalDirection = EntityVerticalDirection.RISING;
            else if(m_playerData.VerticalSpeed < 0)
                m_entity.VerticalDirection = EntityVerticalDirection.FALLING;
            else
                m_entity.VerticalDirection = EntityVerticalDirection.NONE;


            //Update Horizontal Velocity
            if(m_horizontalInput == 1)
            {
                if(m_playerData.MovementSpeed < m_playerData.MaxMovementSpeed)
                    m_playerData.MovementSpeed += m_playerData.HorizontalAccelerationSpeed * Time.deltaTime;
            }
            else if(m_horizontalInput == -1)
            {
                if(m_playerData.MovementSpeed > -m_playerData.MaxMovementSpeed)
                    m_playerData.MovementSpeed -= m_playerData.HorizontalAccelerationSpeed * Time.deltaTime;
            }
            else
            {
                if(m_playerData.MovementSpeed > Entity.m_speedDeadZone)
                    m_playerData.MovementSpeed -= m_playerData.HorizontalDecelerationSpeed * Time.deltaTime;
                else if(m_playerData.MovementSpeed < -Entity.m_speedDeadZone)
                    m_playerData.MovementSpeed += m_playerData.HorizontalDecelerationSpeed * Time.deltaTime;
                else
                    m_playerData.MovementSpeed = 0.0f;
            }
        }

        private void Jump()
        {
            if(Input.GetKeyDown(KeyCode.Space) && m_entity.MovementState != EntityMovementState.JUMPING)
            {
                m_entity.MovementState = EntityMovementState.JUMPING;
                m_jumpDestination = transform.position.y + m_playerData.JumpHeight;
                m_initialPosition = transform.position.y;
                m_playerData.VerticalSpeed = m_playerData.JumpSpeed;
                m_entity.GameManagerRef.CurrencyEvent.Invoke(-m_playerData.CostData.JumpCost);
            }
        }

        private void Move() 
        {
            //Debug.Log(m_vVelocity);
            transform.position += new Vector3(m_playerData.MovementSpeed * Time.deltaTime, m_playerData.VerticalSpeed  * Time.deltaTime);

            UpdateMovementState();

            //If we are moving then deplete from energy
            if(m_playerMovementState == PlayerMovementState.RUNNING)
                m_entity.GameManagerRef.CurrencyEvent.Invoke(-m_playerData.CostData.MovementCost);

        }

        private void UpdateMovementState()
        {
            if(m_horizontalInput != 0)
                m_playerMovementState = PlayerMovementState.RUNNING;
            else
                m_playerMovementState = PlayerMovementState.IDLE;  
        }

        public PlayerData PlayerData
        {
            get { return m_playerData; }
        }
    }

    public enum PlayerMovementState
    {
        IDLE,
        RUNNING
    }
}