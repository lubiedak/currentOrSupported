using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCVRPClient
{
    class Permutation
    {
        public int[][] tab;

        public int PermSize(int n)
        {
            int size = 1;
            for (int i = 2; i <= n; i++)
            {
                size *= i;
            }
            return size/2;
        }

        public Permutation()
        {
            tab = new int[][]{
            new int[]{0, 1, 2, 3, 4},
		    new int[]{0, 2, 1, 3, 4},
		    new int[]{1, 0, 2, 3, 4},
		    new int[]{0, 1, 3, 2, 4},
		    new int[]{0, 2, 3, 1, 4},
		    new int[]{1, 0, 3, 2, 4},
		    new int[]{0, 3, 1, 2, 4},
		    new int[]{0, 3, 2, 1, 4},
		    new int[]{1, 3, 0, 2, 4},
		    new int[]{3, 0, 1, 2, 4},
		    new int[]{3, 0, 2, 1, 4},
		    new int[]{3, 1, 0, 2, 4},
		    new int[]{2, 3, 1, 0, 4},
		    new int[]{1, 3, 2, 0, 4},
		    new int[]{2, 3, 0, 1, 4},
		    new int[]{1, 2, 3, 0, 4},
		    new int[]{3, 2, 1, 0, 4},
		    new int[]{3, 2, 0, 1, 4},
		    new int[]{1, 2, 0, 3, 4},
		    new int[]{2, 1, 3, 0, 4},
		    new int[]{3, 1, 2, 0, 4},
		    new int[]{2, 1, 0, 3, 4},
		    new int[]{2, 0, 3, 1, 4},
		    new int[]{2, 0, 1, 3, 4},
		    new int[]{2, 1, 0, 4, 3},
		    new int[]{2, 1, 4, 0, 3},
		    new int[]{2, 0, 1, 4, 3},
		    new int[]{2, 0, 4, 1, 3},
		    new int[]{2, 4, 0, 1, 3},
		    new int[]{2, 4, 1, 0, 3},
		    new int[]{1, 0, 2, 4, 3},
		    new int[]{1, 0, 4, 2, 3},
		    new int[]{1, 2, 0, 4, 3},
		    new int[]{1, 2, 4, 0, 3},
		    new int[]{1, 4, 2, 0, 3},
		    new int[]{1, 4, 0, 2, 3},
		    new int[]{0, 1, 2, 4, 3},
		    new int[]{0, 1, 4, 2, 3},
		    new int[]{0, 2, 1, 4, 3},
		    new int[]{0, 2, 4, 1, 3},
		    new int[]{0, 4, 2, 1, 3},
		    new int[]{0, 4, 1, 2, 3},
		    new int[]{1, 0, 3, 4, 2},
		    new int[]{1, 0, 4, 3, 2},
		    new int[]{1, 3, 0, 4, 2},
		    new int[]{1, 3, 4, 0, 2},
		    new int[]{1, 4, 3, 0, 2},
		    new int[]{1, 4, 0, 3, 2},
		    new int[]{0, 1, 3, 4, 2},
		    new int[]{0, 1, 4, 3, 2},
		    new int[]{0, 3, 1, 4, 2},
		    new int[]{0, 3, 4, 1, 2},
		    new int[]{0, 4, 3, 1, 2},
		    new int[]{0, 4, 1, 3, 2},
		    new int[]{0, 2, 3, 4, 1},
		    new int[]{0, 2, 4, 3, 1},
		    new int[]{0, 3, 2, 4, 1},
		    new int[]{0, 3, 4, 2, 1},
		    new int[]{0, 4, 2, 3, 1},
		    new int[]{0, 4, 3, 2, 1}};
        }
    }
}
