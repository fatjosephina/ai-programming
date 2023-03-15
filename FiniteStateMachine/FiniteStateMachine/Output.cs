using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    public class Output
    {
        protected string name;
        protected float price;

        public string Name { get { return name; } }
        public float Price { get { return price; } set { price = value; } }
    }
}
