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
        {
            return Math.Sqrt(Math.Pow(a.XCoordinate - b.XCoordinate, 2) + Math.Pow(a.YCoordinate - b.YCoordinate, 2));
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

                if (current.Equals(goal))
                {
                    break;
                }

                foreach (var edge in Graph.GetNeighbors(current.Id))
                {
                    var newCost = CostSoFar[current] + edge.Weight;
                    if (!CostSoFar.ContainsKey(edge.To) || newCost < CostSoFar[edge.To])
                    {
                        CostSoFar[edge.To] = newCost;
                        double priority = newCost + Heuristic(edge.To, goal);
                        frontier.Enqueue(edge.To, priority);
                        CameFrom[edge.To] = current;
                    }
                }
            }

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
    }
}
