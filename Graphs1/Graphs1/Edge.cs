using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs1
{
    class Edge
    {
        public int Value;
        public int Row;
        public int Column;

        public Edge(int value, int row, int column)
        {
            Value = value;
            Row = row;
            Column = column;
        }
    }
}
