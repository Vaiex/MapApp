using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject1
{
    public class Edge
    {
             public Room From { get; private set; }
            public Room To { get; private set; }
            public double Weight { get; private set; }
            public bool IsAccessible { get; private set; }
            public bool IsBidirectional { get; private set; }
            public Edge(Room from, Room to, double weight, bool isAccessible = true, bool isBidirectional = true)
            {
                From = from;
                To = to;
                Weight = weight;
                IsAccessible = isAccessible;
                IsBidirectional = isBidirectional;
            }

            public void UpdateWeight(double newWeight)
            {
                Weight = newWeight;
            }

            public void SetAccessibility(bool accessible)
            {
                IsAccessible = accessible;
            }

            public Edge Inverse()
            {
                if (!IsBidirectional)
                {
                    throw new InvalidOperationException("Cannot inverse a unidirectional edge.");
                }
                return new Edge(To, From, Weight, IsAccessible, IsBidirectional);
            }

            public override string ToString()
            {
                return $"{From.Id} -> {To.Id} | Weight: {Weight}";
            }
        }

        // var edge = new Edge(room1, room2, 5.0);

    }

