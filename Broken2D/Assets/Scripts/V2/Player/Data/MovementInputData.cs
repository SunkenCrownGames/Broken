namespace V2
{
    using UnityEngine;

    [System.Serializable]
    public class MovementInputData
    {
        private float m_horizontalInput = 0.0f;

        [SerializeField]
        private float m_movementDeadZone = 0.5f;

        public float HorizontalInput
        {
            get { return m_horizontalInput; }
            set { m_horizontalInput = value; }
        }

        public float MovemetnDeadZone
        {
            get { return m_movementDeadZone; }
            set { m_movementDeadZone = value; }
        }
    }
}