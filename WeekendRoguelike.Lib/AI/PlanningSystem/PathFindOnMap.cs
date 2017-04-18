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

        public Stack<Point> GetPath(Point start, Point end)
        {
            Stack<Point> path =
                new Stack<Point>();
            HashSet<Node> closed = new HashSet<Node>();
            Dictionary<Point, Node> open = new Dictionary<Point, Node>();
            PriorityQueue<Node> queue = new PriorityQueue<Node>();
            Node current =
                new Node(start, start, 0, (end - start).MagnitudeSquared);
            queue.Enqueue(current);

            void Explore(Node fromNode)
            {
                for (int y = -1; y <= 1; ++y)
                {
                    int yp = fromNode.Position.Y;
                    for (int x = -1; x <= 1; ++x)
                    {
                        if (y == 0 && x == 0)
                            continue;
                        int xp = fromNode.Position.X;
                        Point nextPos = new Point(xp, yp);
                        Node nextNode = new Node(
                            fromNode.Position,
                            nextPos,
                            (nextPos - fromNode.Position).MagnitudeSquared,
                            (end - nextPos).MagnitudeSquared);
                        if (closed.Contains(nextNode) == false &&
                            map.TestMove(fromNode.Position, nextPos) == true &&
                            (open.TryGetValue(nextPos, out var openNode) == false ||
                            openNode.F > nextNode.F))
                        {
                            open[nextPos] = nextNode;
                            queue.Enqueue(nextNode);
                        }
                    }
                }
                closed.Add(fromNode);
            }

            while (queue.Count > 0)
            {
                while (queue.Count > 0 &&
                    closed.Contains(current = queue.Dequeue()) == true) ;
                // We weren't able to find a new non-closed node.
                if (closed.Contains(current))
                    return path;

                if (current.Position == end)
                {
                    while (current.Position != current.Parent)
                    {
                        path.Push(current.Position);
                        current = open[current.Parent];
                    }
                    path.Push(start);
                }

                if (open.TryGetValue(current.Position, out var useInstead) == true)
                    current = useInstead;

                Explore(current);
            }
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

        #region Private Structs

        private struct Node : INode, IEquatable<Node>
        {
            #region Private Fields

            private readonly int g;
            private readonly int h;
            private readonly Point parent;
            private readonly Point position;

            #endregion Private Fields

            #region Public Constructors

            public Node(Point parent, Point position, int g, int h)
            {
                this.parent = parent;
                this.position = position;
                this.g = g;
                this.h = h;
            }

            #endregion Public Constructors

            #region Public Properties

            public int F => G + H;

            public int G { get => g; }
            public int H { get => h; }
            public Point Parent { get => parent; }
            public Point Position { get => position; }

            #endregion Public Properties

            #region Public Methods

            public static bool operator !=(Node left, Node right)
            {
                return (left == right) == false;
            }

            public static bool operator ==(Node left, Node right)
            {
                return left.Equals(right);
            }

            public override bool Equals(object obj)
            {
                return obj is Node n && Equals(n);
            }

            public bool Equals(Node other)
            {
                return Position == other.Position;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override string ToString()
            {
                return string.Format("{0} F: {1}, G: {2}, H: {3}", Position, F, G, H);
            }

            #endregion Public Methods
        }

        #endregion Private Structs
    }
}
