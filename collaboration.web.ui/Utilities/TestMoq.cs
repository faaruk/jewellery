using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Collaboration.Web.UI.Utilities
{
    public class TestMoq
    {
        public bool IsGreaterThan(int a, int b)
        {
            if (a > b)
                return true;
            else
                return false;
        }
    }
}