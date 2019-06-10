using UnityEngine;

namespace V2
{
    public class BezierCurve
    {
        public static Vector3 CalculateBezierPoint(float p_t, Vector3 p_startPoint, Vector3 p_midPoint, Vector3 p_endPoint)
        {
            float u = 1 - p_t;
            float uu = u * u;

            Vector3 point = new Vector3();
            point = uu * p_startPoint;
            point += 2 * u * p_t * p_midPoint;
            point += p_t * p_t * p_endPoint;

            return new Vector3(point.x, point.y, point.z);
        }


        Vector3 CalculateCubicBezierPoint(float p_t, Vector3 p_point0, Vector3 p_point1, Vector3 p_point2, Vector3 p_point3)
        {
            float u = 1 - p_t;
            float tt = p_t * p_t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * p_t;
            
            Vector3 point = uuu * p_point0; 
            point += 3 * uu * p_t * p_point1; 
            point += 3 * u * tt * p_point2; 
            point += ttt * p_point3; 
            
            return point;
        }
    }
}