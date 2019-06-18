namespace V2
{
    using UnityEngine;

    [System.Serializable]
    public class Vec3
    {
        private float m_x;
        private float m_y;
        private float m_z;

        public Vec3() {}

        public Vec3(float p_x, float p_y, float p_z)
        {
            m_x = p_x;
            m_y = p_y;
            m_z = p_z;
        }

        public Vec3(Vector3 p_vector3)
        {
            m_x = p_vector3.x;
            m_y = p_vector3.y;
            m_z = p_vector3.z;
        }


        public float X
        {
            get { return m_x; }
            set { m_x = value; }
        }

        public float Y
        {
            get { return m_y; }
            set { m_y = value; }
        }

        public float Z
        {
            get { return m_z; }
            set { m_z = value; }
        }

        public static Vec3 operator+ (Vector3 p_vector3, Vec3 p_customVector)
        {
            return new Vec3(p_vector3.x + p_customVector.X, p_vector3.y + p_customVector.Y, p_vector3.z + p_customVector.Z);
        }

        public static Vec3 operator/ (Vec3 p_customVector, float p_val)
        {
            return new Vec3(p_customVector.X / p_val, p_customVector.Y / p_val, p_customVector.Z / p_val);
        }
        

        public Vector3 ToVector3()
        {
            return new Vector3(m_x, m_y, m_z);
        }

        public static Vec3 ToVec3(Vector3 p_vector3)
        {
            return new Vec3(p_vector3);
        }
    }
}