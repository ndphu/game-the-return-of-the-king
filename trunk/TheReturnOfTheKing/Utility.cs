using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace TheReturnOfTheKing
{
    public class Utility
    {
        public static Color GetPixel(Texture2D tex, int x, int y)
        {
            Rectangle sourceRectangle =
              new Rectangle(x, y, 1, 1);

            Color[] retrievedColor = new Color[1];

            tex.GetData<Color>(0, sourceRectangle, retrievedColor, 0, 1);
            return retrievedColor[0];
        }
        class Vertex
        {
            Point _this;

            public Point This
            {
                get { return _this; }
                set { _this = value; }
            }

            Vertex previous;

            public Vertex Previous
            {
                get { return previous; }
                set { previous = value; }
            }

            public Vertex()
            {

            }
            public Vertex(Point p, Vertex pp)
            {
                _this = p;
                previous = pp;

            }
        }
        public static List<Point> FindPath(List<List<bool>> matrix, Point _start, Point _end)
        {
            List<List<bool>> _matrix = new List<List<bool>>();
            for (int i = 0; i < matrix.Count; ++i)
            {
                List<bool> temp = new List<bool>();
                for (int j = 0; j < matrix[i].Count; ++j)
                {
                    if (matrix[j][i])
                        temp.Add(true);
                    else
                        temp.Add(false);
                }
                _matrix.Add(temp);
            }
            List<Point> ret = new List<Point>();
            Queue<Vertex> Qu = new Queue<Vertex>();
            Vertex startV = new Vertex();
            startV.This = _start;
            startV.Previous = null;
            Qu.Enqueue(startV);
            while (Qu.Count != 0)
            {
                Vertex D1;
                D1 = Qu.Dequeue();
                if (D1.This.X == _end.X && D1.This.Y == _end.Y)
                {
                    ret = new List<Point>();
                    Vertex temp = new Vertex();
                    temp = D1;
                    while (true)
                    {
                        ret.Add(new Point(D1.This.X, D1.This.Y));
                        D1 = D1.Previous;
                        if (D1 == null)
                        {
                            ret.RemoveAt(ret.Count - 1);
                            return ret;
                        }
                    }
                }
                if (Check(D1.This.X - 1, D1.This.Y, _matrix) && _matrix[D1.This.X - 1][D1.This.Y] == true)
                {
                    Vertex D2 = new Vertex(new Point(D1.This.X - 1, D1.This.Y), D1);
                    Qu.Enqueue(D2);
                    _matrix[D1.This.X - 1][D1.This.Y] = false;

                }
                if (Check(D1.This.X + 1, D1.This.Y, _matrix) && _matrix[D1.This.X + 1][D1.This.Y] == true)
                {
                    Vertex D2 = new Vertex(new Point(D1.This.X + 1, D1.This.Y), D1);
                    Qu.Enqueue(D2);
                    _matrix[D1.This.X + 1][D1.This.Y] = false;
                }
                if (Check(D1.This.X, D1.This.Y - 1, _matrix) && _matrix[D1.This.X][D1.This.Y - 1] == true)
                {
                    Vertex D2 = new Vertex(new Point(D1.This.X, D1.This.Y - 1), D1);
                    Qu.Enqueue(D2);
                    _matrix[D1.This.X][D1.This.Y - 1] = false;
                }
                if (Check(D1.This.X, D1.This.Y + 1, _matrix) && _matrix[D1.This.X][D1.This.Y + 1] == true)
                {
                    Vertex D2 = new Vertex(new Point(D1.This.X, D1.This.Y + 1), D1);
                    Qu.Enqueue(D2);
                    _matrix[D1.This.X][D1.This.Y + 1] = false;
                }

                if (Check(D1.This.X - 1, D1.This.Y - 1, _matrix) && _matrix[D1.This.X - 1][D1.This.Y - 1] == true)
                {
                    Vertex D2 = new Vertex(new Point(D1.This.X - 1, D1.This.Y - 1), D1);
                    Qu.Enqueue(D2);
                    _matrix[D1.This.X - 1][D1.This.Y - 1] = false;

                }
                if (Check(D1.This.X + 1, D1.This.Y - 1, _matrix) && _matrix[D1.This.X + 1][D1.This.Y - 1] == true)
                {
                    Vertex D2 = new Vertex(new Point(D1.This.X + 1, D1.This.Y - 1), D1);
                    Qu.Enqueue(D2);
                    _matrix[D1.This.X + 1][D1.This.Y - 1] = false;
                }
                if (Check(D1.This.X - 1, D1.This.Y + 1, _matrix) && _matrix[D1.This.X - 1][D1.This.Y + 1] == true)
                {
                    Vertex D2 = new Vertex(new Point(D1.This.X - 1, D1.This.Y + 1), D1);
                    Qu.Enqueue(D2);
                    _matrix[D1.This.X - 1][D1.This.Y + 1] = false;
                }
                if (Check(D1.This.X + 1, D1.This.Y + 1, _matrix) && _matrix[D1.This.X + 1][D1.This.Y + 1] == true)
                {
                    Vertex D2 = new Vertex(new Point(D1.This.X + 1, D1.This.Y + 1), D1);
                    Qu.Enqueue(D2);
                    _matrix[D1.This.X + 1][D1.This.Y + 1] = false;
                }
            }

            return ret;
        }
        static bool Check(int x, int y, List<List<bool>> _matrix)
        {
            if (x < 0 || y < 0 || x >= _matrix.Count || y >= _matrix[0].Count)
                return false;
            return true;
        }
    }
}
