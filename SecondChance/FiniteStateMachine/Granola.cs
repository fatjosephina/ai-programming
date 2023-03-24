using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    public class Granola : Output
    {
        private static Granola instance = null;

        private Granola()
        {
            name = "GRANOLA";
            price = 0.75f;
        }

        public static Granola Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Granola();
                }
                return instance;
            }
        }
    }
}
