namespace V2
{
    using UnityEngine;

    [System.Serializable]
    public class PlayerData
    {
        [SerializeField]
        private ShootingData m_shootingData;

        [SerializeField]
        private PlayerMovementState m_playerMovementState;
        
        [SerializeField]
        private PlayerMovementData m_playerMovementData;

        [SerializeField]
        private PlayerJumpData m_jumpData;

        [SerializeField]
        private AbilityCostData m_abilityCostData;

        private MovementInputData m_inputData;


        public PlayerData()
        {
            m_inputData = new MovementInputData();
            m_playerMovementData = new PlayerMovementData();
        }

        public MovementInputData InputData
        {
            get { return m_inputData; }
            set { m_inputData = value; }
        }

        public PlayerMovementState MovementState
        {
            get { return m_playerMovementState; }
            set { m_playerMovementState = value; }
        }

        public PlayerMovementData MovementData
        {
            get { return m_playerMovementData; }
            set { m_playerMovementData = value;}
        }

        public PlayerJumpData JumpData
        {
            get { return m_jumpData; }
            set { m_jumpData = value; }
        }

        public AbilityCostData CostData
        {
            get { return m_abilityCostData; }
            set { m_abilityCostData = value; }
        }
        
        public ShootingData ShootingData
        {
            get { return m_shootingData; }
            set { m_shootingData = value; }
        }
    }
}