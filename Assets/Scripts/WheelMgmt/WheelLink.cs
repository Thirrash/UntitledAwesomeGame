using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AwesomeGame.WheelMgmt
{
    public static class WheelLink
    {
        static readonly float sqrt2by2 = (float)Math.Sqrt( 2.0 ) / 2.0f;
        public static readonly float MaximumDistance = 2.0f * 1.18f;

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
            { WheelPosition.InnerLeft, new Vector2( 0.55f, 0f) },
            { WheelPosition.OuterBottom, new Vector2( 0f, 1.18f) },
            { WheelPosition.OuterLeftBottom, new Vector2( 0.8344f, 0.8344f) },
            { WheelPosition.Center, new Vector2( 0f, 0f) },
            { WheelPosition.InnerRightBottom, new Vector2( -0.389f, 0.389f) },
            { WheelPosition.OuterLeft, new Vector2( 1.18f, 0f) },
            { WheelPosition.OuterLeftTop, new Vector2( 0.8344f, -0.8344f) },
            { WheelPosition.InnerLeftTop, new Vector2( 0.389f, -0.389f) },
            { WheelPosition.OuterRight, new Vector2( -1.18f, 0f) },
            { WheelPosition.InnerRightTop, new Vector2( -0.389f, -0.389f) },
            { WheelPosition.OuterRightBottom, new Vector2( -0.8344f, 0.8344f) },
            { WheelPosition.InnerTop, new Vector2( 0f, -0.55f) },
            { WheelPosition.InnerLeftBottom, new Vector2( 0.389f, 0.389f) },
            { WheelPosition.InnerBottom, new Vector2( 0f, 0.55f) },
            { WheelPosition.OuterTop, new Vector2( 0f, -1.18f) },
            { WheelPosition.InnerRight, new Vector2( -0.55f, 0f) },
            { WheelPosition.OuterRightTop, new Vector2( -0.8344f, -0.8344f) }
        };

        /// <summary>
        /// Gets the distance between WheelPositions
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second. Will be mirrored</param>
        /// <returns></returns>
        public static float GetDistance( WheelPosition first, WheelPosition second ) {
            return Vector2.Distance( fragmentPosition[first], new Vector2( -fragmentPosition[second].x, fragmentPosition[second].y ) );
        }

        public static List<WheelPosition> GetAvailableTransitions( WheelPosition from ) {
            return adjacent[from];
        }
    }
}
