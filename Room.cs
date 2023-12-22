using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    public class Room
    {
            public string Id { get; private set; }          
            public int Floor { get; private set; }
            public double XCoordinate { get; private set; }
            public double YCoordinate { get; private set; }
            public bool RequiresSpecialAccess { get; private set; }

            public Room(string id, int floor, double xCoordinate, double yCoordinate, bool requiresSpecialAccess = false)
            {
                Id = id;
                Floor = floor;
                XCoordinate = xCoordinate;
                YCoordinate = yCoordinate;
                RequiresSpecialAccess = requiresSpecialAccess;
            }

            public void UpdateCoordinates(double newX, double newY)
            {
                XCoordinate = newX;
                YCoordinate = newY;
            }
            public void SetSpecialAccessRequirement(bool requiresAccess)
            {
                RequiresSpecialAccess = requiresAccess;
            }
            public override string ToString()
            {
                return $" (ID: {Id}, Floor: {Floor}) at Coordinates ({XCoordinate}, {YCoordinate})";
            }
        }
 }
