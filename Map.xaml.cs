using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FinalProject1
{
    public partial class Map : ContentPage
    {
        private Graph schoolMap;
        private AStarSearch aStarSearch;
        
        public List<Room> Rooms { get; private set; }
        public Map()
        {
            InitializeComponent();
            InitializeSchoolMap();
            aStarSearch = new AStarSearch(schoolMap);
            Rooms = schoolMap.Rooms.Values.ToList();
            this.BindingContext = this; 
        }

        private void InitializeSchoolMap()
        {
            schoolMap = new Graph();

            var room101 = new Room("101", 1, 0.0, 0.0);
            var room102 = new Room("102", 1, 10.0, 0.0);
            var room103 = new Room("103", 1, 20.0, 0.0);
            var room104 = new Room("104", 1, 30.0, 0.0);
            var room105 = new Room("105", 1, 40.0, 0.0);
            var room106 = new Room("106", 1, 50.0, 0.0);
            var room107 = new Room("107", 1, 60.0, 0.0);
            var room108 = new Room("108", 1, 70.0, 0.0);
            var room109 = new Room("109", 1, 80.0, 0.0);  
            var room110 = new Room("110", 1, 90.0, 0.0);
            var room201 = new Room("201", 2, 0.0, 20.0);
            var room202 = new Room("202", 2, 10.0, 20.0);
            var room209 = new Room("209", 2, 80.0, 20.0);
            var room210 = new Room("210", 2, 90.0, 20.0);
            var stairsNear101 = new Room("StairsNear101", 1, 0.5, 0.0);
            var stairsNear110 = new Room("StairsNear110", 1, 90.5, 0.0);

            schoolMap.AddRoom(room101);
            schoolMap.AddRoom(room102);
            schoolMap.AddRoom(room103);
            schoolMap.AddRoom(room104);
            schoolMap.AddRoom(room105);
            schoolMap.AddRoom(room106);
            schoolMap.AddRoom(room107);
            schoolMap.AddRoom(room108);
            schoolMap.AddRoom(room109);
            schoolMap.AddRoom(room110);
            schoolMap.AddRoom(room201);
            schoolMap.AddRoom(room202);
            schoolMap.AddRoom(room209);
            schoolMap.AddRoom(room210);
            schoolMap.AddRoom(stairsNear101);
            schoolMap.AddRoom(stairsNear110);

            var edge101to102 = new Edge(room101, room102, 5.0);
            var edge102to103 = new Edge(room102, room103, 5.0);
            var edge103to104 = new Edge(room103, room104, 5.0);
            var edge104to105 = new Edge(room104, room105, 7.0);
            var edge105to106 = new Edge(room105, room106, 5.0);
            var edge106to107 = new Edge(room106, room107, 5.0);
            var edge107to108 = new Edge(room107, room108, 5.0);
            var edge108to109 = new Edge(room108, room109, 5.0); 
            var edge109to110 = new Edge(room109, room110, 5.0); 
            var edge101to104 = new Edge(room101, room104, 10.0); 
            var edge102to105 = new Edge(room102, room105, 8.0);
            var edge105to107 = new Edge(room105, room107, 7.0);
            var edge103to108 = new Edge(room103, room108, 15.0);
            var edge201to202 = new Edge(room201, room202, 5.0);
            var edge202to209 = new Edge(room202, room209, 20);
            var edge209to210 = new Edge(room209, room210, 5.0);
            var edgeStairs101toNear101 = new Edge(room101, stairsNear101, 2.0);
            var edgeStairs101toNear110 = new Edge(room110, stairsNear110, 2.0);
            var edgeStairs101to201 = new Edge(stairsNear101, room201, 2.0); 
            var edgeStairs110to210 = new Edge(stairsNear110, room210, 2.0);

            schoolMap.AddEdge(edge101to102);
            schoolMap.AddEdge(edge102to103);
            schoolMap.AddEdge(edge103to104);
            schoolMap.AddEdge(edge104to105);
            schoolMap.AddEdge(edge105to106);
            schoolMap.AddEdge(edge106to107);
            schoolMap.AddEdge(edge107to108);
            schoolMap.AddEdge(edge108to109);
            schoolMap.AddEdge(edge109to110);
            schoolMap.AddEdge(edge101to104);
            schoolMap.AddEdge(edge102to105);
            schoolMap.AddEdge(edge103to108);
            schoolMap.AddEdge(edge105to107);
            schoolMap.AddEdge(edge201to202);
            schoolMap.AddEdge(edge209to210);
            schoolMap.AddEdge(edge202to209);
            schoolMap.AddEdge(edgeStairs101to201);
            schoolMap.AddEdge(edgeStairs110to210);
            schoolMap.AddEdge(edgeStairs101toNear101);
            schoolMap.AddEdge(edgeStairs101toNear110);
        
        }
        public void FindAndDisplayPath(string startRoomId, string goalRoomId)
        {
            if (!schoolMap.Rooms.TryGetValue(startRoomId, out var startRoom))
            {
                System.Diagnostics.Debug.WriteLine($"Start room ID {startRoomId} not found.");
                return;
            }

            if (!schoolMap.Rooms.TryGetValue(goalRoomId, out var goalRoom))
            {
                System.Diagnostics.Debug.WriteLine($"Goal room ID {goalRoomId} not found.");
                return;
            }

            var path = aStarSearch.Search(startRoom, goalRoom);

            if (path == null || path.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("No path found or an error occurred.");
                return;
            }

            DisplayPath(path);
        }
        private void OnFindPathClicked(object sender, EventArgs e)
        {
            if (StartRoomPicker.SelectedItem is Room startRoom && EndRoomPicker.SelectedItem is Room endRoom)
            {
                FindAndDisplayPath(startRoom.Id, endRoom.Id);
            }
            else
            {
            }
        }

        private void DisplayPath(List<Room> path)
        {
            System.Diagnostics.Debug.WriteLine("Path found:");
            foreach (var room in path)
            {
                System.Diagnostics.Debug.WriteLine($"{room.Id} ");
            }
        }
    }
}


//private void InitializeSchoolMap()
//{
//    schoolMap = new Graph();

//    XDocument xmlDoc = XDocument.Load("path/to/schoolMap.xml"); // Update path
//    var rooms = xmlDoc.Descendants("Room");
//    var edges = xmlDoc.Descendants("Edge");

//    foreach (var room in rooms)
//    {
//        string id = room.Attribute("id").Value;
//        int floor = int.Parse(room.Attribute("floor").Value);
//        double x = double.Parse(room.Attribute("x").Value);
//        double y = double.Parse(room.Attribute("y").Value);
//        schoolMap.AddRoom(new Room(id, floor, x, y));
//    }

//    foreach (var edge in edges)
//    {
//        string fromId = edge.Attribute("from").Value;
//        string toId = edge.Attribute("to").Value;
//        double weight = double.Parse(edge.Attribute("weight").Value);

//        Room fromRoom = schoolMap.Rooms[fromId];
//        Room toRoom = schoolMap.Rooms[toId];

//        schoolMap.AddEdge(new Edge(fromRoom, toRoom, weight));
//    }
//    aStarSearch = new AStarSearch(schoolMap);
//    Rooms = schoolMap.Rooms.Values.ToList();
//    this.BindingContext = this;
//}