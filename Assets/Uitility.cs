using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class Uitility
    {
        static float solve_qadratic_equation(float a, float b, float c, bool pos)
        {
            var preRoot = b * b - 4 * a * c;
            if (preRoot < 0)
            {
                return float.NaN;
            }
            else
            {
                var sgn = pos ? 1.0 : -1.0;
                return (float)(sgn * Mathf.Sqrt(preRoot) - b) / (2.0f * a);
            }
        }
    }
}
