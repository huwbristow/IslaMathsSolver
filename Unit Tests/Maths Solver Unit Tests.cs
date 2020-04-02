using Isla_Maths_Solver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Maths_Solver_Unit_Tests
{
    [TestClass]
    public class MathsSolverTest
    {
        [TestMethod]
        public void CheckPathsOnTinyArray()
        {
            double[,] array = new double[1, 1];

            GridSearcher gs = new GridSearcher(array, 1);
            List<Path> paths = gs.Search();
            Assert.IsNotNull(paths);
            Assert.AreEqual(1, paths.Count);
        }

        [TestMethod]
        public void CheckPathsOnTwoByTwo()
        {
            double[,] array = new double[2, 2];

            GridSearcher gs = new GridSearcher(array, 1);
            List<Path> paths = gs.Search();
            Assert.IsNotNull(paths);
            Assert.AreEqual(5, paths.Count);
            Assert.AreEqual(1, paths[0].PathCount);
            Assert.AreEqual(2, paths[1].PathCount);
            Assert.AreEqual(3, paths[2].PathCount);
            Assert.AreEqual(2, paths[3].PathCount);
            Assert.AreEqual(3, paths[4].PathCount);
            Assert.IsFalse(paths[0].IsCompletePath());
            Assert.IsFalse(paths[1].IsCompletePath());
            Assert.IsTrue(paths[2].IsCompletePath());
            Assert.IsFalse(paths[3].IsCompletePath());
            Assert.IsTrue(paths[4].IsCompletePath());
        }

        [TestMethod]
        public void CheckGetSum()
        {
            double[,] array = new double[2, 2];
            array[0, 0] = 1;
            array[0, 1] = 2;
            array[1, 0] = 2;
            array[1, 1] = 3;

            GridSearcher gs = new GridSearcher(array, 6);
            List<Path> paths = gs.Search();
            Assert.IsNotNull(paths);
            Assert.AreEqual(5, paths.Count);
            Assert.AreEqual(1, paths[0].GetSum());
            Assert.AreEqual(3, paths[1].GetSum());
            Assert.AreEqual(6, paths[2].GetSum());
            Assert.AreEqual(3, paths[3].GetSum());
            Assert.AreEqual(6, paths[4].GetSum());
        }


        [TestMethod]
        public void CheckPathsOnTheeByThree()
        {
            double[,] array = new double[3, 3];
            array[0, 0] = 1;
            array[0, 1] = 2;
            array[1, 0] = 2;
            array[1, 1] = 3;
            array[0, 2] = 3;
            array[2, 0] = 3;
            array[1, 2] = 4;
            array[2, 1] = 4;
            array[2, 2] = 5;

            GridSearcher gs = new GridSearcher(array, 15);
            List<Path> paths = gs.Search();
            Assert.IsNotNull(paths);
            Assert.AreEqual(19, paths.Count);
            Assert.AreEqual(6, paths.FindAll(item => item.IsCompletePath()).Count);
            Assert.AreEqual(6, paths.FindAll(item => item.IsValidPath()).Count);
            Assert.AreEqual("Coords: (0, 0), Value: 1, Coords: (1, 0), Value: 2, Coords: (2, 0), Value: 3, Coords: (2, 1), Value: 4, Coords: (2, 2), Value: 5, Sum: 15", paths.FindAll(item => item.IsValidPath())[0].ToString());
            Assert.AreEqual("Coords: (0, 0), Value: 1, Coords: (1, 0), Value: 2, Coords: (1, 1), Value: 3, Coords: (2, 1), Value: 4, Coords: (2, 2), Value: 5, Sum: 15", paths.FindAll(item => item.IsValidPath())[1].ToString());
            Assert.AreEqual("Coords: (0, 0), Value: 1, Coords: (1, 0), Value: 2, Coords: (1, 1), Value: 3, Coords: (1, 2), Value: 4, Coords: (2, 2), Value: 5, Sum: 15", paths.FindAll(item => item.IsValidPath())[2].ToString());
            Assert.AreEqual("Coords: (0, 0), Value: 1, Coords: (0, 1), Value: 2, Coords: (1, 1), Value: 3, Coords: (2, 1), Value: 4, Coords: (2, 2), Value: 5, Sum: 15", paths.FindAll(item => item.IsValidPath())[3].ToString());
            Assert.AreEqual("Coords: (0, 0), Value: 1, Coords: (0, 1), Value: 2, Coords: (1, 1), Value: 3, Coords: (1, 2), Value: 4, Coords: (2, 2), Value: 5, Sum: 15", paths.FindAll(item => item.IsValidPath())[4].ToString());
            Assert.AreEqual("Coords: (0, 0), Value: 1, Coords: (0, 1), Value: 2, Coords: (0, 2), Value: 3, Coords: (1, 2), Value: 4, Coords: (2, 2), Value: 5, Sum: 15", paths.FindAll(item => item.IsValidPath())[5].ToString());
        }


        [TestMethod]
        public void CheckPathsOnActualProblem()
        {
            double[,] array = GetArray();
            GridSearcher gs = new GridSearcher(array, 1);
            List<Path> paths = gs.Search();
            Assert.IsNotNull(paths);
            Assert.AreEqual(1, paths.FindAll(item => item.IsValidPath()).Count);
            Assert.AreEqual("Coords: (0, 0), Value: 0.02, Coords: (1, 0), Value: 0.2, Coords: (2, 0), Value: 0.005, Coords: (2, 1), Value: 0.04, Coords: (2, 2), Value: 0.2, Coords: (3, 2), Value: 0.05, Coords: (4, 2), Value: 0.001, Coords: (4, 3), Value: 0.05, Coords: (4, 4), Value: 0.015, Coords: (5, 4), Value: 0.199, Coords: (5, 5), Value: 0.01, Coords: (6, 5), Value: 0.08, Coords: (6, 6), Value: 0.02, Coords: (6, 7), Value: 0.02, Coords: (7, 7), Value: 0.09, Sum: 1", paths.FindAll(item => item.IsValidPath())[0].ToString());
        }

        [TestMethod]
        public void CheckPathsOnActualProblemIncludingDiagonals()
        {
            double[,] array = GetArray();
            GridSearcher gs = new GridSearcher(array, 1);
            List<Path> paths = gs.Search(true);
            Assert.IsNotNull(paths);
            Assert.AreEqual(10, paths.FindAll(item => item.IsValidPath()).Count);
            Assert.AreEqual("Coords: (0, 0), Value: 0.02, Coords: (1, 0), Value: 0.2, Coords: (2, 1), Value: 0.04, Coords: (2, 2), Value: 0.2, Coords: (3, 2), Value: 0.05, Coords: (4, 2), Value: 0.001, Coords: (4, 3), Value: 0.05, Coords: (5, 4), Value: 0.199, Coords: (5, 5), Value: 0.01, Coords: (6, 5), Value: 0.08, Coords: (6, 6), Value: 0.02, Coords: (7, 6), Value: 0.04, Coords: (7, 7), Value: 0.09, Sum: 1", paths.FindAll(item => item.IsValidPath())[0].ToString());
            Assert.AreEqual("Coords: (0, 0), Value: 0.02, Coords: (1, 0), Value: 0.2, Coords: (2, 1), Value: 0.04, Coords: (2, 2), Value: 0.2, Coords: (3, 2), Value: 0.05, Coords: (4, 2), Value: 0.001, Coords: (4, 3), Value: 0.05, Coords: (5, 3), Value: 0.03, Coords: (5, 4), Value: 0.199, Coords: (6, 5), Value: 0.08, Coords: (7, 6), Value: 0.04, Coords: (7, 7), Value: 0.09, Sum: 1", paths.FindAll(item => item.IsValidPath())[1].ToString());
            Assert.AreEqual("Coords: (0, 0), Value: 0.02, Coords: (1, 0), Value: 0.2, Coords: (2, 1), Value: 0.04, Coords: (2, 2), Value: 0.2, Coords: (3, 2), Value: 0.05, Coords: (4, 2), Value: 0.001, Coords: (4, 3), Value: 0.05, Coords: (5, 3), Value: 0.03, Coords: (5, 4), Value: 0.199, Coords: (6, 5), Value: 0.08, Coords: (6, 6), Value: 0.02, Coords: (6, 7), Value: 0.02, Coords: (7, 7), Value: 0.09, Sum: 1", paths.FindAll(item => item.IsValidPath())[2].ToString());
            Assert.AreEqual("Coords: (0, 0), Value: 0.02, Coords: (1, 0), Value: 0.2, Coords: (2, 0), Value: 0.005, Coords: (2, 1), Value: 0.04, Coords: (2, 2), Value: 0.2, Coords: (3, 2), Value: 0.05, Coords: (4, 2), Value: 0.001, Coords: (4, 3), Value: 0.05, Coords: (4, 4), Value: 0.015, Coords: (5, 4), Value: 0.199, Coords: (5, 5), Value: 0.01, Coords: (6, 5), Value: 0.08, Coords: (7, 6), Value: 0.04, Coords: (7, 7), Value: 0.09, Sum: 1", paths.FindAll(item => item.IsValidPath())[3].ToString());
            Assert.AreEqual("Coords: (0, 0), Value: 0.02, Coords: (1, 0), Value: 0.2, Coords: (2, 0), Value: 0.005, Coords: (2, 1), Value: 0.04, Coords: (2, 2), Value: 0.2, Coords: (3, 2), Value: 0.05, Coords: (4, 2), Value: 0.001, Coords: (4, 3), Value: 0.05, Coords: (4, 4), Value: 0.015, Coords: (5, 4), Value: 0.199, Coords: (5, 5), Value: 0.01, Coords: (6, 5), Value: 0.08, Coords: (6, 6), Value: 0.02, Coords: (6, 7), Value: 0.02, Coords: (7, 7), Value: 0.09, Sum: 1", paths.FindAll(item => item.IsValidPath())[4].ToString());
            Assert.AreEqual("Coords: (0, 0), Value: 0.02, Coords: (1, 0), Value: 0.2, Coords: (1, 1), Value: 0.06, Coords: (2, 2), Value: 0.2, Coords: (3, 2), Value: 0.05, Coords: (4, 2), Value: 0.001, Coords: (5, 3), Value: 0.03, Coords: (5, 4), Value: 0.199, Coords: (5, 5), Value: 0.01, Coords: (6, 5), Value: 0.08, Coords: (6, 6), Value: 0.02, Coords: (7, 6), Value: 0.04, Coords: (7, 7), Value: 0.09, Sum: 1", paths.FindAll(item => item.IsValidPath())[5].ToString());
            Assert.AreEqual("Coords: (0, 0), Value: 0.02, Coords: (1, 0), Value: 0.2, Coords: (1, 1), Value: 0.06, Coords: (2, 2), Value: 0.2, Coords: (3, 2), Value: 0.05, Coords: (4, 2), Value: 0.001, Coords: (4, 3), Value: 0.05, Coords: (5, 4), Value: 0.199, Coords: (5, 5), Value: 0.01, Coords: (6, 5), Value: 0.08, Coords: (7, 6), Value: 0.04, Coords: (7, 7), Value: 0.09, Sum: 1", paths.FindAll(item => item.IsValidPath())[6].ToString());
            Assert.AreEqual("Coords: (0, 0), Value: 0.02, Coords: (1, 0), Value: 0.2, Coords: (1, 1), Value: 0.06, Coords: (2, 2), Value: 0.2, Coords: (3, 2), Value: 0.05, Coords: (4, 2), Value: 0.001, Coords: (4, 3), Value: 0.05, Coords: (5, 4), Value: 0.199, Coords: (5, 5), Value: 0.01, Coords: (6, 5), Value: 0.08, Coords: (6, 6), Value: 0.02, Coords: (6, 7), Value: 0.02, Coords: (7, 7), Value: 0.09, Sum: 1", paths.FindAll(item => item.IsValidPath())[7].ToString());
            Assert.AreEqual("Coords: (0, 0), Value: 0.02, Coords: (1, 0), Value: 0.2, Coords: (1, 1), Value: 0.06, Coords: (2, 2), Value: 0.2, Coords: (3, 2), Value: 0.05, Coords: (4, 2), Value: 0.001, Coords: (4, 3), Value: 0.05, Coords: (5, 3), Value: 0.03, Coords: (5, 4), Value: 0.199, Coords: (6, 5), Value: 0.08, Coords: (6, 6), Value: 0.02, Coords: (7, 7), Value: 0.09, Sum: 1", paths.FindAll(item => item.IsValidPath())[8].ToString());
            Assert.AreEqual("Coords: (0, 0), Value: 0.02, Coords: (1, 0), Value: 0.2, Coords: (1, 1), Value: 0.06, Coords: (2, 1), Value: 0.04, Coords: (2, 2), Value: 0.2, Coords: (3, 2), Value: 0.05, Coords: (4, 2), Value: 0.001, Coords: (5, 3), Value: 0.03, Coords: (5, 4), Value: 0.199, Coords: (5, 5), Value: 0.01, Coords: (6, 5), Value: 0.08, Coords: (6, 6), Value: 0.02, Coords: (7, 7), Value: 0.09, Sum: 1", paths.FindAll(item => item.IsValidPath())[9].ToString());
        }

        private double[,] GetArray()
        {
            double[,] array = new double[8, 8];
            array[0, 0] = 0.02;
            array[0, 1] = 0.01;
            array[0, 2] = 0.05;
            array[0, 3] = 0.08;
            array[0, 4] = 0.3;
            array[0, 5] = 0.04;
            array[0, 6] = 0;
            array[0, 7] = 0.001;

            array[1, 0] = 0.2;
            array[1, 1] = 0.06;
            array[1, 2] = 0.07;
            array[1, 3] = 0.09;
            array[1, 4] = 0.001;
            array[1, 5] = 0.004;
            array[1, 6] = 0.02;
            array[1, 7] = 0.04;

            array[2, 0] = 0.005;
            array[2, 1] = 0.04;
            array[2, 2] = 0.2;
            array[2, 3] = 0.02;
            array[2, 4] = 0.05;
            array[2, 5] = 0.06;
            array[2, 6] = 0.07;
            array[2, 7] = 0.6;

            array[3, 0] = 0.5;
            array[3, 1] = 0.005;
            array[3, 2] = 0.05;
            array[3, 3] = 0.02;
            array[3, 4] = 0.03;
            array[3, 5] = 0.017;
            array[3, 6] = 0.006;
            array[3, 7] = 0.06;

            array[4, 0] = 0.009;
            array[4, 1] = 0.8;
            array[4, 2] = 0.001;
            array[4, 3] = 0.05;
            array[4, 4] = 0.015;
            array[4, 5] = 0.01;
            array[4, 6] = 0.008;
            array[4, 7] = 0.007;

            array[5, 0] = 0.09;
            array[5, 1] = 0.2;
            array[5, 2] = 0.08;
            array[5, 3] = 0.03;
            array[5, 4] = 0.199;
            array[5, 5] = 0.01;
            array[5, 6] = 0.04;
            array[5, 7] = 0.05;

            array[6, 0] = 0.01;
            array[6, 1] = 0.008;
            array[6, 2] = 0.1;
            array[6, 3] = 0.09;
            array[6, 4] = 0.005;
            array[6, 5] = 0.08;
            array[6, 6] = 0.02;
            array[6, 7] = 0.02;

            array[7, 0] = 0.05;
            array[7, 1] = 0.03;
            array[7, 2] = 0.01;
            array[7, 3] = 0.22;
            array[7, 4] = 0.07;
            array[7, 5] = 0.003;
            array[7, 6] = 0.04;
            array[7, 7] = 0.09;
            return array;
        }
    }
}
