using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    public class Graph
    {
        public Dictionary<string, Room> Rooms { get; private set; }
        private Dictionary<string, List<Edge>> adjacencies;
        public Graph()
        {
            Rooms = new Dictionary<string, Room>();
            adjacencies = new Dictionary<string, List<Edge>>();
        }
        public void AddRoom(Room room)
        {
            if (!Rooms.ContainsKey(room.Id))
            {
                Rooms.Add(room.Id, room);
                adjacencies[room.Id] = new List<Edge>();
            }
        }
        public void AddEdge(Edge edge)
        {
            if (!Rooms.ContainsKey(edge.From.Id) || !Rooms.ContainsKey(edge.To.Id))
            {
                throw new KeyNotFoundException("One or both rooms for this edge do not exist in the graph.");
            }
            adjacencies[edge.From.Id].Add(edge);

            if (edge.IsBidirectional)
            {
                var inverseEdge = new Edge(edge.To, edge.From, edge.Weight, edge.IsAccessible, edge.IsBidirectional);
                adjacencies[edge.To.Id].Add(inverseEdge);
            }
        }
        public List<Edge> GetNeighbors(string roomId)
        {
            if (!adjacencies.ContainsKey(roomId))
            {
                throw new KeyNotFoundException("The room does not exist in the graph.");
            }

            return adjacencies[roomId];
        }
        public bool PathExists(string fromRoomId, string toRoomId)
        {
            if (!Rooms.ContainsKey(fromRoomId) || !Rooms.ContainsKey(toRoomId))
            {
                return false;
            }

            return adjacencies[fromRoomId].Any(edge => edge.To.Id == toRoomId);
        }
        public override string ToString()
        {
            return $"Graph has {Rooms.Count} rooms and {adjacencies.Sum(kvp => kvp.Value.Count)} edges.";
        }
    }



}
