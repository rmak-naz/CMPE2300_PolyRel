using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDIDrawer;
using System.Drawing;
using System.Threading;

namespace Lab3_PolyRel
{
    class Fungus
    {
        private Dictionary<Point, int> visitedPoints = new Dictionary<Point, int>();        
        static Random rnd = new Random();
        private Point pos;
        private FungusColor growColor;
        private Thread fungusThread;

        public enum FungusColor
        {
            Red, Green, Blue
        };

        public Fungus(Point initPos, CDrawer dr, FungusColor color)
        {
            pos = initPos;
            growColor = color;
            lock (visitedPoints)
            {
                visitedPoints.Add(initPos, (int)color);
            }            
            fungusThread = new Thread(GrowFungus);
            fungusThread.IsBackground = true;
            fungusThread.Start(dr);
        }

        private void GrowFungus(object canvasObj)
        {   
            while (true)
            {                
                Dictionary<Point, int> SortDictionary;
                CDrawer canvas = (CDrawer)canvasObj;
                List<Point> freePositions = GeneratePos(pos, canvas);

                if (freePositions.TrueForAll(pos => pos.X > 995 && pos.Y > 995))
                {
                    Console.WriteLine("Glitched!");
                }

                ShufflePositions(freePositions);
                
                SortDictionary = freePositions.ToDictionary(pos => pos, pos => (visitedPoints.Keys.Contains(pos) ? visitedPoints[pos] : 0));
                                
                SortDictionary = SortDictionary.OrderBy(kvp => kvp.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                pos = SortDictionary.First().Key;

               
                if (visitedPoints.Keys.Contains(pos))
                {
                    if (visitedPoints[pos] + 16 >= 255)
                        visitedPoints[pos] = 255;
                    else
                        visitedPoints[pos] += 16;
                }
                else
                {
                    visitedPoints.Add(pos, 32);
                }
               
                
                switch (growColor)
                {
                    case FungusColor.Blue:
                        canvas.SetBBPixel(pos.X, pos.Y, Color.FromArgb(0, 0, visitedPoints[pos]));
                        break;
                    case FungusColor.Green:
                        canvas.SetBBPixel(pos.X, pos.Y, Color.FromArgb(0, visitedPoints[pos], 0));
                        break;
                    default:
                        canvas.SetBBPixel(pos.X, pos.Y, Color.FromArgb(visitedPoints[pos], 0, 0));
                        break;
                }
                
                
                Thread.Sleep(0);
            }         
        }

        private List<Point> GeneratePos(Point basePos, CDrawer dr)
        {
            List<Point> freeMove = new List<Point>();
            freeMove.Add(new Point(basePos.X + 1, basePos.Y));
            freeMove.Add(new Point(basePos.X + 1, basePos.Y + 1));
            freeMove.Add(new Point(basePos.X + 1, basePos.Y - 1));

            freeMove.Add(new Point(basePos.X, basePos.Y + 1));
            freeMove.Add(new Point(basePos.X, basePos.Y - 1));

            freeMove.Add(new Point(basePos.X - 1, basePos.Y));
            freeMove.Add(new Point(basePos.X - 1, basePos.Y + 1));
            freeMove.Add(new Point(basePos.X - 1, basePos.Y - 1));

            freeMove.RemoveAll(newPos => newPos.X < 0 || newPos.X >= dr.ScaledWidth || newPos.Y < 0 || newPos.Y >= dr.ScaledHeight);
            return freeMove;
        }

        private void ShufflePositions(List<Point> positions)
        {            
            for (int i = positions.Count; i > 1; i--)
            {
                int j;             
                lock (rnd)
                {
                    j = rnd.Next(i);
                }                
                Point tmp = positions[j];
                positions[j] = positions[i - 1];
                positions[i - 1] = tmp;
            }
        }
    }
}
