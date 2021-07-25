using System;
using System.Collections.Generic;
using Speckle.Core.Api;
using Speckle.Core.Models;
using Objects.BuiltElements;
using Objects.Geometry;
using Objects;


namespace SpeckleAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var @base = Helpers.Receive("https://speckle.xyz/streams/fba4a82cd0/commits/cd51c5bf34").Result;
            var data = (List<object>)@base["@data"];
            var new_stream = new Base(); 
            foreach (Room room in data)
            {
                var outline = (Base)room["outline"];
                var segements = (List<ICurve>)outline["segments"];
                double perimeter = 0;
                foreach (Line line in segements)
                {
                    var length = Convert.ToDouble(line["length"]);
                    perimeter += length;
                }
                room["perimeter"] = perimeter;
            }
            new_stream["@data"] = data;
            var commitId = Helpers.Send("https://speckle.xyz/streams/2b8cc8a828", new_stream, "Rooms With Perimeters").Result;
        }
    }
}
