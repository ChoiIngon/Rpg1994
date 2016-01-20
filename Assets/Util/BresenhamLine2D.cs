﻿using UnityEngine;
using System.Collections;

namespace Util
{
    public class BresenhamLine2D : IEnumerable
    {
        Position start; // http://kukuta.tistory.com/185
        Position end;

        public BresenhamLine2D(Position start, Position end)
        {
            this.start = start;
            this.end = end;
        }

        public IEnumerator GetEnumerator()
        {
            int dx = Mathf.Abs(end.x - start.x); // 시작 점과 끝 점의 각 x 좌표의 거리
            int dy = Mathf.Abs(end.y - start.y); // 시작 점과 끝 점의 각 y 좌표의 거리

            if (dy <= dx)
            {
                int p = 2 * (dy - dx);
                int y = start.y;

                int inc_x = 1;
                if (end.x < start.x)
                {
                    inc_x = -1;
                }
                int inc_y = 1;
                if (end.y < start.y)
                {
                    inc_y = -1;
                }
                for (int x = start.x; (start.x <= end.x ? x <= end.x : x >= end.x); x += inc_x)
                {
                    if (0 >= p)
                    {
                        p += 2 * dy;
                    }
                    else
                    {
                        p += 2 * (dy - dx);
                        y += inc_y;
                    }
                    yield return new Position(x, y);
                }
            }
            else
            {
                int p = 2 * (dx - dy);
                int x = start.x;

                int inc_x = 1;
                if (end.x < start.x)
                {
                    inc_x = -1;
                }
                int inc_y = 1;
                if (end.y < start.y)
                {
                    inc_y = -1;
                }
                for (int y = start.y; (start.y <= end.y ? y <= end.y : y >= end.y); y += inc_y)
                {
                    if (0 >= p)
                    {
                        p += 2 * dx;
                    }
                    else
                    {
                        p += 2 * (dx - dy);
                        x += inc_x;
                    }
                    yield return new Position(x, y);
                }
            }
        }
    }
}