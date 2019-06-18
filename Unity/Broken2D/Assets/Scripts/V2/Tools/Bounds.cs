using UnityEngine;

namespace V2
{
    public class Bounds : MonoBehaviour
    {
        [SerializeField]
        private bool m_updateEveryFrame = false;
        private SpriteRenderer m_sprite;
        float[] m_hBounds = new float[2];
        float[] m_vBounds = new float[2];

        private void Awake() 
        {
            BindObjects();
            UpdateBounds();     
        }

        private void Update()
        {
            if(m_updateEveryFrame)
                UpdateBounds();
        }
        
        private void BindObjects()
        {
            m_sprite = GetComponent<SpriteRenderer>();
        }

        private void UpdateBounds()
        {
            m_hBounds[0] = transform.position.x - m_sprite.bounds.extents.x;
            m_hBounds[1] = transform.position.x + m_sprite.bounds.extents.x;

            m_vBounds[0] = transform.position.y - m_sprite.bounds.extents.y;
            m_vBounds[1] = transform.position.y + m_sprite.bounds.extents.y;
        }

        public float[] EntityVerticalBounds
        {
            get { return m_vBounds; }
            set { m_vBounds = value; }
        }

        public float[] EntityHorizontalBounds
        {
            get { return m_hBounds; }
            set { m_hBounds = value; }
        }
    }
}