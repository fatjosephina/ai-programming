using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    public class Quarter : Output
    {
        private static Quarter instance = null;

        private Quarter()
        {
            name = "QUARTER";
            price = 0.00f;
        }

        public static Quarter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Quarter();
                }
                return instance;
            }
        }
    }
}
