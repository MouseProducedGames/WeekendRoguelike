using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeekendRoguelike.MapSystem;

namespace WeekendRoguelike.AI.PlanningSystem
{
    public class PathFindOnMap
    {
        #region Private Fields

        private readonly Map map;

        #endregion Private Fields

        #region Public Constructors

        public PathFindOnMap(Map map)
        {
            this.map = map;
        }

        #endregion Public Constructors

        #region Public Methods

        public Queue<Point> GetPath(Point start, Point end)
        {
            Queue<Point> path =
                new Queue<Point>();
            Point current = start;
            while (current != end)
            {
                Displacement disp = end - current;
                disp.X = Math.Sign(disp.X);
                disp.Y = Math.Sign(disp.Y);
                path.Enqueue(current);
                current += disp;
            }
            path.Enqueue(end);
            return path;
        }

        public Point GetSingleStep(Point start, Point end)
        {
            Displacement disp = end - start;
            disp.X = Math.Sign(disp.X);
            disp.Y = Math.Sign(disp.Y);

            return start + disp;
        }

        #endregion Public Methods
    }
}
