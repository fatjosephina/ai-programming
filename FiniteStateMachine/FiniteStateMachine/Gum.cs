using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    public class Gum : Output
    {
        private static Gum instance = null;

        private Gum()
        {
            name = "GUM";
            price = 0.50f;
        }

        public static Gum Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Gum();
                }
                return instance;
            }
        }
    }
}
