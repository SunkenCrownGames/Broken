namespace V1
{
    using UnityEngine;
    
    public class OrbController : MonoBehaviour
    {
        [SerializeField]
        private OrbSpawnData m_orbData;

        [SerializeField]
        private float m_currentDuration;
        [SerializeField]
        private float m_duration = 0;
        private Vector3 m_startPos;

        private Vector3 m_midPoint;

        private static GameObject m_player;

        private void Awake() 
        {
            BindObjects();
        }

        private void BindObjects()
        {
            m_player = GameObject.FindGameObjectWithTag("Player");
            m_startPos = transform.position;
            //m_midPoint = (m_player.transform.position + m_startPos) / 2;
            m_midPoint = (new Vector3(m_player.transform.position.x, 0, 0) + m_startPos) / 2;
        }

        private void Update()
        {
            if(m_orbData.Type == OrbType.TRACKING)
                Track();
        }

        private void Track()
        {
            switch(m_orbData.OrbTrackType)
            {
                case TrackType.STRAIGHT:
                    StraightTracking();
                break;
                case TrackType.BEZIER:
                    BezierTracking();
                break;
            }
        }

        private void StraightTracking()
        {
            float step = m_orbData.SeekSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, m_player.transform.position, step);
        }

        private void BezierTracking()
        {
            m_currentDuration += Time.deltaTime;
            //m_midPoint = (m_player.transform.position + m_startPos) / 2;
            m_midPoint = (new Vector3(m_player.transform.position.x, 0, 0) + m_startPos) / 2;
            transform.position = BezierCurve.CalculateBezierPoint(m_currentDuration / m_duration, m_startPos, m_midPoint, m_player.transform.position);
        }

        public OrbSpawnData OrbData
        {
            get { return m_orbData; }
            set { m_orbData = value; }
        }
    }

    public enum TrackType
    {
        STRAIGHT,
        BEZIER,
        NONE
    }
}