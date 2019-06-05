namespace V1
{
    using UnityEngine;
    
    public class OrbController : MonoBehaviour
    {
        [SerializeField]
        private OrbSpawnData m_orbData;

        private static GameObject m_player;

        private void Awake() 
        {
            BindObjects();
        }

        private void BindObjects()
        {
            m_player = GameObject.FindGameObjectWithTag("Player");
            m_orbData.Type = OrbType.STATIONARY;
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