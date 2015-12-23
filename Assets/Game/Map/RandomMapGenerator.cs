using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomMapGenerator {
    class Devider
    {
        enum DevideDirectionType
        {
            Horizon, Vertical
        }
        public int depth = 0;
        public int id = 0;
        public int neighbor = 0;
        public int left = 0;
        public int right = 0;
        public int top = 0;
        public int bottom = 0;
        public int width { get { return right - left; } }
        public int height { get { return bottom - top; } }
        public int area { get { return width * height; } }
        public DevideDirectionType direction = DevideDirectionType.Vertical

        public void Devide(RandomMapGenerator dungeon)
        {
            if (dungeon.roomCount <= Mathf.Pow(2, depth))
            {
                return;
            }

            Devider child_1 = null;
            Devider child_2 = null;
            float weight = Random.Range(0.3f, 0.7f);
            if (DevideDirectionType.Vertical == direction)
            {
                int leftWidth = (int)((float)width * weight);
                int rightWidth = width - leftWidth;

                child_1 = new Devider() { id = this.id * 2, depth = this.depth + 1, left = this.left, right = this.left + leftWidth, top = this.top, bottom = this.bottom, direction = DevideDirectionType.Horizon };
                child_2 = new Devider() { id = this.id * 2 + 1, depth = this.depth + 1, left = this.left + leftWidth, right = this.right, top = this.top, bottom = this.bottom, direction = DevideDirectionType.Horizon };
            }
            else if (DevideDirectionType.Horizon == direction)
            {
                int topHeight = (int)((float)height * weight);
                int bottomHeight = height - topHeight;

                child_1 = new Devider() { id = this.id * 2, depth = this.depth + 1, left = this.left, right = this.right, top = this.top, bottom = this.top + topHeight, direction = DevideDirectionType.Vertical };
                child_2 = new Devider() { id = this.id * 2 + 1, depth = this.depth + 1, left = this.left, right = this.right, top = this.top + topHeight, bottom = this.bottom, direction = DevideDirectionType.Vertical };
            }

            child_1.Devide(dungeon);
            child_2.Devide(dungeon);

            if (dungeon.roomCount <= Mathf.Pow(2, depth + 1))
            {
                if (child_1.width > 2 && child_1.height > 2 && child_2.width > 2 && child_2.height > 2)
                {
                    //dungeon.deviders.Add(child_1);
                    //dungeon.deviders.Add(child_2);
                }
                else
                {
                    //dungeon.deviders.Add(this);
                }
                return;
            }
        }
    }

    public int roomCount;
    public Tile[] Create(int width, int height, int roomCount)
    {
        Tile [] tiles = new Tile[width * height];
        Devider devider = new Devider();
        return tiles;
    }
}
