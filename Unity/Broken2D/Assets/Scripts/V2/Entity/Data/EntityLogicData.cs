namespace V2
{
    using UnityEngine;
    
    public class EntityLogicData
    {
        //The sprite bounds
        protected Bounds m_bounds;

        //The layer that all the ground tiles are on
        protected int m_groundLayerMask;

        //Current Ground Reference
        protected GameObject m_ground;

        protected static GameManager m_gm;

        public int GroundLayerMask
        {
            get { return m_groundLayerMask; }
            set { m_groundLayerMask = value; }
        }

        public GameObject GroundObject
        {
            get { return m_ground; }
            set { m_ground = value; }
        }

        public Bounds Bounds
        {
            get { return m_bounds; }
            set { m_bounds = value; }
        }

        public GameManager GM
        {
            get { return m_gm; }
            set { m_gm = value; }
        }
    }
}