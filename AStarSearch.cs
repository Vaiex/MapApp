using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    public class AStarSearch
    {
        public Graph Graph { get; private set; }
        public Dictionary<Room, Room> CameFrom { get; private set; }
        public Dictionary<Room, double> CostSoFar { get; private set; }

        public AStarSearch(Graph graph)
        {
            Graph = graph;
            CameFrom = new Dictionary<Room, Room>();
            CostSoFar = new Dictionary<Room, double>();
        }
        private double Heuristic(Room a, Room b)
        {return Math.Sqrt(Math.Pow(a.XCoordinate - b.XCoordinate, 2) + Math.Pow(a.YCoordinate - b.YCoordinate, 2));}

        private double Heuristic2(Room a, Room b)
        {
            const double flCC = 10.0; 
            const double openSpaceMl= 1.0; 
            const double corridorMl = 1.2; 
            double floorCost = Math.Abs(a.Floor - b.Floor) * flCC;
            double euclideanDistance = Math.Sqrt(Math.Pow(a.XCoordinate - b.XCoordinate, 2) + Math.Pow(a.YCoordinate - b.YCoordinate, 2));
            double manhattanDistance = Math.Abs(a.XCoordinate - b.XCoordinate) + Math.Abs(a.YCoordinate - b.YCoordinate);
            return floorCost + (euclideanDistance * openSpaceMl) + (manhattanDistance * corridorMl);
        }

        public List<Room> Search(Room start, Room goal)
        {
            var frontier = new PriorityQueue<Room, double>();
            frontier.Enqueue(start, 0);
            CameFrom[start] = start;
            CostSoFar[start] = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (current.Equals(goal)){break;}

                foreach (var edge in Graph.GetNeighbors(current.Id))
                {
                    var newCost = CostSoFar[current] + edge.Weight;
                    if (!CostSoFar.ContainsKey(edge.To) || newCost < CostSoFar[edge.To])
                    {
                        CostSoFar[edge.To] = newCost;
                        double priority = newCost + Heuristic(edge.To, goal);
                        frontier.Enqueue(edge.To, priority);
                        CameFrom[edge.To] = current;
                    }}}

            return ReconstructPath(start, goal);
        }
        private List<Room> ReconstructPath(Room start, Room goal)
        {
            var path = new List<Room>();
            var current = goal;

            while (!current.Equals(start))
            {
                path.Add(current);
                current = CameFrom[current];
            }

            path.Add(start);
            path.Reverse();
            return path;
        }

       
        //        public List<Room> BidirectionalSearch(Room start, Room goal)
        //        {
        //            var frontierStart = new PriorityQueue<Room, double>();
        //            var cameFromStart = new Dictionary<Room, Room>();
        //            var costSoFarStart = new Dictionary<Room, double>();
        //            frontierStart.Enqueue(start, 0);
        //            cameFromStart[start] = start;
        //            costSoFarStart[start] = 0;

        //            var frontierGoal = new PriorityQueue<Room, double>();
        //            var cameFromGoal = new Dictionary<Room, Room>();
        //            var costSoFarGoal = new Dictionary<Room, double>();
        //            frontierGoal.Enqueue(goal, 0);
        //            cameFromGoal[goal] = goal;
        //            costSoFarGoal[goal] = 0;

        //            Room meetingPoint = null;

        //            while (frontierStart.Count > 0 && frontierGoal.Count > 0)
        //            {
        //                if (frontierStart.Count > 0)
        //                {
        //                    var currentStart = frontierStart.Dequeue();
        //                    if (cameFromGoal.ContainsKey(currentStart))
        //                    {
        //                        meetingPoint = currentStart;
        //                        break;
        //                    }
        //                    ExpandSearch(frontierStart, currentStart, goal, cameFromStart, costSoFarStart);
        //                }

        //                if (frontierGoal.Count > 0)
        //                {
        //                    var currentGoal = frontierGoal.Dequeue();
        //                    if (cameFromStart.ContainsKey(currentGoal))
        //                    {
        //                        meetingPoint = currentGoal;
        //                        break;
        //                    }
        //                    ExpandSearch(frontierGoal, currentGoal, start, cameFromGoal, costSoFarGoal);
        //                }
        //            }

        //            if (meetingPoint != null)
        //            {
        //                return ReconstructBidirectionalPath(cameFromStart, cameFromGoal, start, goal, meetingPoint);
        //            }

        //            return new List<Room>(); 
        //        }

        //        private void ExpandSearch(PriorityQueue<Room, double> frontier, Room current, Room target, Dictionary<Room, Room> cameFrom, Dictionary<Room, double> costSoFar)
        //        {
        //            foreach (var edge in Graph.GetNeighbors(current.Id))
        //            {
        //                var newCost = costSoFar[current] + edge.Weight;
        //                if (!costSoFar.ContainsKey(edge.To) || newCost < costSoFar[edge.To])
        //                {
        //                    costSoFar[edge.To] = newCost;
        //                    double priority = newCost + Heuristic(edge.To, target);
        //                    frontier.Enqueue(edge.To, priority);
        //                    cameFrom[edge.To] = current;
        //                }
        //            }
        //        }

        //        private List<Room> ReconstructBidirectionalPath(Dictionary<Room, Room> cameFromStart, Dictionary<Room, Room> cameFromGoal, Room start, Room goal, Room meetingPoint)
        //        {
        //            var pathFromStart = ReconstructPathPartial(cameFromStart, start, meetingPoint);
        //            var pathFromGoal = ReconstructPathPartial(cameFromGoal, goal, meetingPoint);

        //            pathFromGoal.Reverse();
        //            pathFromStart.AddRange(pathFromGoal.Skip(1)); 
        //            return pathFromStart;
        //        }

        //        private List<Room> ReconstructPathPartial(Dictionary<Room, Room> cameFrom, Room start, Room end)
        //        {
        //            var path = new List<Room>();
        //            var current = end;

        //            while (!current.Equals(start))
        //            {
        //                path.Add(current);
        //                current = cameFrom[current];
        //            }

        //            path.Add(start);
        //            path.Reverse();
        //            return path;
        //        }
        //    }
        //}

        public List<Room> CalculateConvexHull()
        {
            var points = Graph.Rooms.Values.ToList();
            points.Sort((a, b) => a.XCoordinate.CompareTo(b.XCoordinate));

            var hull = new List<Room>();

            foreach (var pt in points)
            {
                while (hull.Count >= 2 && !IsCounterClockwise(hull[hull.Count - 2], hull[hull.Count - 1], pt))
                {
                    hull.RemoveAt(hull.Count - 1);
                }
                hull.Add(pt);
            }

            var t = hull.Count + 1;
            for (int i = points.Count - 1; i >= 0; i--)
            {
                var pt = points[i];
                while (hull.Count >= t && !IsCounterClockwise(hull[hull.Count - 2], hull[hull.Count - 1], pt))
                {
                    hull.RemoveAt(hull.Count - 1);
                }
                hull.Add(pt);
            }

            hull.RemoveAt(hull.Count - 1);
            return hull;
        }

        private static bool IsCounterClockwise(Room a, Room b, Room c)
        {
            return (b.XCoordinate - a.XCoordinate) * (c.YCoordinate - a.YCoordinate) -
                   (b.YCoordinate - a.YCoordinate) * (c.XCoordinate - a.XCoordinate) > 0;
        }

    }
}



