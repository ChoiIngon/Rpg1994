using UnityEngine;
using System.Collections;

namespace Map
{
    public class BresenhamCircle2D : IEnumerable
    {
        private Position center;
        private int radious;
        public BresenhamCircle2D(Position center, int radious)
        {
            this.center = center;
            this.radious = radious;
        }
        public IEnumerator GetEnumerator()
        {
            if (0 >= radious)
            {
                yield break;
            }

            int xK = 0;
            int yK = radious;
            int pK = 3 - (radious + radious);

            do
            {
                yield return new Position(center.x + xK, center.y - yK);
                yield return new Position(center.x - xK, center.y - yK);
                yield return new Position(center.x + xK, center.y + yK);
                yield return new Position(center.x - xK, center.y + yK);
                yield return new Position(center.x + yK, center.y - xK);
                yield return new Position(center.x - yK, center.y - xK);
                yield return new Position(center.x + yK, center.y + xK);
                yield return new Position(center.x - yK, center.y + xK);

                xK++;
                if (pK < 0)
                {
                    pK += (xK << 2) + 6;
                }
                else
                {
                    --yK;
                    pK += ((xK - yK) << 2) + 10;
                }
            } while (xK <= yK);
        }
    }
}