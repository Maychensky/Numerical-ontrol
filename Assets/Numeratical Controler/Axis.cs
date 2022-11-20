using System;
using UnityEngine;

namespace NumericalControl
{
    internal enum Axis
    {
        X = 0,
        Y = 1,
        Z = 2,
    }

    internal enum DirectionVector
    {
        Forward,
        Backward,
    }

    internal static class ExtensionAxis
    {
        private static int _lengthAxis = Enum.GetNames(typeof(Axis)).Length;
        internal static Axis NextAxis(this Axis axis) 
        {
            int curretNumberAxis = (int)axis;
            return (curretNumberAxis == _lengthAxis - 1) ? GetAxis(0) : GetAxis(curretNumberAxis + 1);
        }

        internal static Axis PreviousAxis(this Axis axis) 
        {
            int curretNumberAxis = (int)axis;
            return (curretNumberAxis == 0) ? GetAxis(_lengthAxis - 1) : GetAxis(curretNumberAxis - 1);
        }

        private static Axis GetAxis(int numberEnum)
        {
            return (Axis)Enum.GetValues(typeof(Axis)).GetValue(numberEnum);
        }

        internal static float GetAxis(this Vector3 vector, Axis axis)
        {
            switch (axis)
            {
                case Axis.X: return vector.x;
                case Axis.Y: return vector.y; 
                case Axis.Z: return vector.z; 
                default: throw new StackOverflowException();
            }
        }
    }
}