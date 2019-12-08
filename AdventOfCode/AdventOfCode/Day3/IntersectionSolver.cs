using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day3
{
    class IntersectionSolver
    {
        public IntersectionSolver(string line1, string line2)
        {
            var lineOneMoves = new List<string>(line1.Split(','));
            var lineTwoMoves = new List<string>(line2.Split(','));
            this.segments1 = _getSegments(null, lineOneMoves);
            this.segments2 = _getSegments(null, lineTwoMoves);
            this.lineOne = _getAllPoints(segments1);
            this.lineTwo = _getAllPoints(segments2);
            this.intersections = _getIntersections();
        }

        LinkedList<Tuple<Point, Point>> segments1 = new LinkedList<Tuple<Point, Point>>();
        LinkedList<Tuple<Point, Point>> segments2 = new LinkedList<Tuple<Point, Point>>();
        HashSet<Point> lineOne = new HashSet<Point>();
        HashSet<Point> lineTwo = new HashSet<Point>();
        List<Point> intersections = new List<Point>();


        public int shortestIntersectionPathSum()
        {
            var shortestDistanceSum = 0;
            var one = new List<Point>(lineOne);
            var two = new List<Point>(lineTwo);
            foreach (Point p in intersections)
            {
                var onePoint = one.Find(point => point.x == p.x && point.y == p.y);
                var twoPoint = two.Find(point => point.x == p.x && point.y == p.y);
                if (onePoint == null || twoPoint == null)
                {
                    throw new Exception("Shortest Intersection point is null!");
                }
                var distanceSum = onePoint.distanceFromStart + twoPoint.distanceFromStart;
                if (shortestDistanceSum == 0 || shortestDistanceSum > distanceSum)
                {
                    shortestDistanceSum = distanceSum;
                }
            }
            return shortestDistanceSum;
        }
        public int closestIntersectionDistance()
        {
            var center = new Point(0, 0);
            var closest = closestIntersection();
            return _getDistance(center, closest);
        }

        public Point closestIntersection()
        {
            if (intersections.Count < 1)
            {
                return null;
            }
            Point closest = intersections[0];
            var center = new Point(0, 0);
            foreach (Point point in intersections)
            {
                if (_getDistance(center, point) < _getDistance(center, closest))
                {
                    closest = point;
                }
            }
            return closest;
        }

        private int _getDistance(Point one, Point two)
        {
            return Math.Abs(one.x - two.x) + Math.Abs(one.y - two.y);
        }

        public List<Point> getIntersections()
        {
            return new List<Point>(this.intersections);
        }

        private List<Point> _getIntersections()
        {
            var intersections = new List<Point>();
            foreach (Point point1 in lineOne)
            {
                foreach (Point point2 in lineTwo)
                {
                    if (point1.x == point2.x && point1.y == point2.y)
                    {
                        intersections.Add(point1);
                    }
                }
            }

            return intersections;
        }

        private HashSet<Point> _getAllPoints(LinkedList<Tuple<Point, Point>> segments)
        {
            var allPoints = new HashSet<Point>();
            int lastDistance = 0;
            foreach (Tuple<Point, Point> segment in segments)
            {
                segment.Item1.distanceFromStart = lastDistance;
                var segmentPoints = _getMovesBetweenPoints(segment.Item1, segment.Item2);
                lastDistance = segmentPoints[segmentPoints.Count - 1].distanceFromStart;
                segmentPoints.ForEach(point => allPoints.Add(point));
            }
            return allPoints;
        }

        private List<Point> _getMovesBetweenPoints(Point one, Point two)
        {
            var startingDistance = one.distanceFromStart;
            String direction = "";
            direction = one.y < two.y ? "UP" : direction;
            direction = one.y > two.y ? "DOWN" : direction;
            direction = one.x > two.x ? "LEFT" : direction;
            direction = one.x < two.x ? "RIGHT" : direction;
            List<Point> linePoints = new List<Point>();
            switch (direction)
            {
                case "UP":
                    while (one.y != two.y)
                    {
                        one.y++;
                        var nextPoint = new Point(one.x, one.y);
                        nextPoint.distanceFromStart = ++startingDistance;
                        linePoints.Add(nextPoint);
                    }
                    break;
                case "DOWN":
                    while (one.y != two.y)
                    {
                        one.y--;
                        var nextPoint = new Point(one.x, one.y);
                        nextPoint.distanceFromStart = ++startingDistance;
                        linePoints.Add(nextPoint);
                    }
                    break;
                case "LEFT":
                    while (one.x != two.x)
                    {
                        one.x--;
                        var nextPoint = new Point(one.x, one.y);
                        nextPoint.distanceFromStart = ++startingDistance;
                        linePoints.Add(nextPoint);
                    }
                    break;
                case "RIGHT":
                    while (one.x != two.x)
                    {
                        one.x++;
                        var nextPoint = new Point(one.x, one.y);
                        nextPoint.distanceFromStart = ++startingDistance;
                        linePoints.Add(nextPoint);
                    }
                    break;
            }
            return linePoints;
        }

        private LinkedList<Tuple<Point, Point>> _getSegments(LinkedList<Tuple<Point, Point>> segments, List<string> lineMoves)
        {
            if (segments == null)
            {
                segments = new LinkedList<Tuple<Point, Point>>();
            }
            foreach (string s in lineMoves)
            {
                _move(segments, s);
            }
            return segments;
        }


        private void _move(LinkedList<Tuple<Point, Point>> segments, string moveAsString)
        {
            Point move = _getMoveAsPoint(moveAsString);
            var currentPosition = new Point(0, 0);
            try
            {
                currentPosition = segments.Last.Value.Item2;
            }
            catch (Exception e) { }
            var newSegment = new Tuple<Point, Point>(currentPosition, new Point(currentPosition.x+move.x, currentPosition.y + move.y));
            segments.AddLast(newSegment);
        }
        private Point _getMoveAsPoint(string move)
        {
            var direction = move.Substring(0, 1);
            var value = Int32.Parse(move.Substring(1));
            switch (direction)
            {
                case "R":
                    return new Point(value, 0);
                case "D":
                    return new Point(0, value*-1);
                case "L":
                    return new Point(value*-1, 0);
                case "U":
                    return new Point(0, value);
                default:
                    return null;
            }
        }


    }

    class Point
    {
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x { get; set; }
        public int y { get; set; }
        public int distanceFromStart { get; set; }
        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }

}
