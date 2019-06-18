namespace V2
{
    using UnityEngine;

    [System.Serializable]
    public class PlayerJumpData
    {
        [SerializeField]
        private float m_jumpDestination = 0.0f;

        [SerializeField]
        private float m_initialPosition = 0.0f;


        
        public float JumpDestination
        {
            get { return m_jumpDestination; }
            set { m_jumpDestination = value; }
        }

        public float InitialPosition
        {
            get { return m_initialPosition; }
            set { m_initialPosition = value; }
        }
    } 
}