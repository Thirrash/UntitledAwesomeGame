using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AwesomeGame.WheelMgmt
{
    public static class WheelLink
    {
        static readonly float innerRadius = 1.2f;
        static readonly float outerRadius = 2.2f;
        static readonly float sqrt2by2 = (float)Math.Sqrt( 2.0 ) / 2.0f;
        public static readonly float MaximumDistance = 2.0f * outerRadius;

        static readonly Dictionary<WheelPosition, List<WheelPosition>> adjacent = new Dictionary<WheelPosition, List<WheelPosition>> {
            { WheelPosition.Center, new List<WheelPosition> { WheelPosition.InnerBottom, WheelPosition.InnerLeft, WheelPosition.InnerLeftBottom,
                WheelPosition.InnerLeftTop, WheelPosition.InnerRight, WheelPosition.InnerRightBottom, WheelPosition.InnerRightTop, WheelPosition.InnerTop } },

            { WheelPosition.InnerBottom, new List<WheelPosition> { WheelPosition.Center, WheelPosition.InnerLeftBottom, WheelPosition.InnerRightBottom, WheelPosition.OuterBottom } },
            { WheelPosition.InnerLeft, new List<WheelPosition> { WheelPosition.Center, WheelPosition.InnerLeftBottom, WheelPosition.InnerLeftTop, WheelPosition.OuterLeft } },
            { WheelPosition.InnerLeftBottom, new List<WheelPosition> { WheelPosition.Center, WheelPosition.InnerBottom, WheelPosition.InnerLeft, WheelPosition.OuterLeftBottom } },
            { WheelPosition.InnerLeftTop, new List<WheelPosition> { WheelPosition.Center, WheelPosition.InnerLeft, WheelPosition.InnerTop, WheelPosition.OuterLeftTop } },
            { WheelPosition.InnerRight, new List<WheelPosition> { WheelPosition.Center, WheelPosition.InnerRightBottom, WheelPosition.InnerRightTop, WheelPosition.OuterRight } },
            { WheelPosition.InnerRightBottom, new List<WheelPosition> { WheelPosition.Center, WheelPosition.InnerRight, WheelPosition.InnerBottom, WheelPosition.OuterRightBottom } },
            { WheelPosition.InnerRightTop, new List<WheelPosition> { WheelPosition.Center, WheelPosition.InnerRight, WheelPosition.InnerTop, WheelPosition.OuterRightTop } },
            { WheelPosition.InnerTop, new List<WheelPosition> { WheelPosition.Center, WheelPosition.InnerRightTop, WheelPosition.InnerLeftTop, WheelPosition.OuterTop } },

            { WheelPosition.OuterBottom, new List<WheelPosition> { WheelPosition.InnerBottom, WheelPosition.OuterLeftBottom, WheelPosition.OuterRightBottom } },
            { WheelPosition.OuterLeft, new List<WheelPosition> { WheelPosition.InnerLeft, WheelPosition.OuterLeftBottom, WheelPosition.OuterLeftTop } },
            { WheelPosition.OuterLeftBottom, new List<WheelPosition> { WheelPosition.InnerLeftBottom, WheelPosition.OuterLeft, WheelPosition.OuterBottom } },
            { WheelPosition.OuterLeftTop, new List<WheelPosition> { WheelPosition.InnerLeftTop, WheelPosition.OuterLeft, WheelPosition.OuterTop } },
            { WheelPosition.OuterRight, new List<WheelPosition> { WheelPosition.InnerRight, WheelPosition.OuterRightBottom, WheelPosition.OuterRightTop } },
            { WheelPosition.OuterRightBottom, new List<WheelPosition> { WheelPosition.InnerRightBottom, WheelPosition.OuterRight, WheelPosition.OuterBottom } },
            { WheelPosition.OuterRightTop, new List<WheelPosition> { WheelPosition.InnerRightTop, WheelPosition.OuterRight, WheelPosition.OuterTop } },
            { WheelPosition.OuterTop, new List<WheelPosition> { WheelPosition.InnerTop, WheelPosition.OuterLeftTop, WheelPosition.OuterRightTop } }
        };

        static readonly Dictionary<WheelPosition, Vector2> fragmentPosition = new Dictionary<WheelPosition, Vector2> {
            { WheelPosition.Center, new Vector2( 0.0f, 0.0f ) },

            { WheelPosition.InnerTop, new Vector2( 0.0f, innerRadius ) },
            { WheelPosition.InnerRightTop, new Vector2( sqrt2by2 * innerRadius, sqrt2by2 * innerRadius ) },
            { WheelPosition.InnerRight, new Vector2( innerRadius, 0.0f ) },
            { WheelPosition.InnerRightBottom, new Vector2( sqrt2by2 * innerRadius, -sqrt2by2 * innerRadius ) },
            { WheelPosition.InnerBottom, new Vector2( 0.0f, -innerRadius ) },
            { WheelPosition.InnerLeftBottom, new Vector2( -sqrt2by2 * innerRadius, -sqrt2by2 * innerRadius ) },
            { WheelPosition.InnerLeft, new Vector2( -innerRadius, 0.0f ) },
            { WheelPosition.InnerLeftTop, new Vector2( -sqrt2by2 * innerRadius, sqrt2by2 * innerRadius ) },

            { WheelPosition.OuterTop, new Vector2( 0.0f, outerRadius ) },
            { WheelPosition.OuterRightTop, new Vector2( sqrt2by2 * outerRadius, sqrt2by2 * outerRadius ) },
            { WheelPosition.OuterRight, new Vector2( outerRadius, 0.0f ) },
            { WheelPosition.OuterRightBottom, new Vector2( sqrt2by2 * outerRadius, -sqrt2by2 * outerRadius ) },
            { WheelPosition.OuterBottom, new Vector2( 0.0f, -outerRadius ) },
            { WheelPosition.OuterLeftBottom, new Vector2( -sqrt2by2 * outerRadius, -sqrt2by2 * outerRadius ) },
            { WheelPosition.OuterLeft, new Vector2( -outerRadius, 0.0f ) },
            { WheelPosition.OuterLeftTop, new Vector2( -sqrt2by2 * outerRadius, sqrt2by2 * outerRadius ) }
        };

        public static float GetDistance( WheelPosition first, WheelPosition second ) {
            return Vector2.Distance( fragmentPosition[first], fragmentPosition[second] );
        }

        public static List<WheelPosition> GetAvailableTransitions( WheelPosition from ) {
            return adjacent[from];
        }
    }
}
