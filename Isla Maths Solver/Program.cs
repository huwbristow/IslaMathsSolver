using System;
using System.Collections.Generic;
using System.Text;

namespace Isla_Maths_Solver
{

    public class Path
    {
        private readonly double _desiredTotal;

        public Path(double[,] values, double desiredTotal)
        {
            DataArray = values;
            Paths = new List<Coords>();
            _desiredTotal = desiredTotal;
        }

        public void AddCoord(Coords coord)
        {
            Paths.Add(coord);
        }

        public double GetSum()
        {
            double tempSum = 0;
            Paths.ForEach(co =>
             {
                 tempSum += DataArray[co.X, co.Y];
             });
            return tempSum;
        }

        public bool IsCompletePath()
        {
            int rowCount = DataArray.GetLength(0);
            int colCount = DataArray.GetLength(1);
            bool isComplete = false;
            Paths.ForEach(item =>
            {
                if (item.X == rowCount - 1 && item.Y == colCount - 1) isComplete = true;
            });
            return isComplete;
        }

        public bool IsValidPath()
        {
            if (GetSum() == _desiredTotal && IsCompletePath())
            {
                return true;
            }
            return false;
        }

        public int PathCount
        {
            get { return Paths.Count; }
        }

        public double[,] DataArray { get; }

        public List<Coords> Paths { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            Paths.ForEach(coord =>
            {
                sb.AppendFormat("Coords: {0}, Value: {1}, ", coord.ToString(), DataArray[coord.X, coord.Y]);
            }
            );
            sb.AppendFormat("Sum: {0}", GetSum());
            return sb.ToString();
        }

        public string ToShortString()
        {
            StringBuilder sb = new StringBuilder();

            Paths.ForEach(coord =>
            {
                sb.AppendFormat("{0}, ", DataArray[coord.X, coord.Y]);
            }
            );
            sb.AppendFormat("Sum: {0}", GetSum());
            return sb.ToString();
        }
    }

    public struct Coords
    {
        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public override string ToString() => $"({X}, {Y})";
    }

    public class GridSearcher
    {
        int _rowCount;
        int _colCount;
        private double _desiredTotal;
        double[,] _array;
        List<Path> _paths = new List<Path>();

        public GridSearcher(double[,] array, double desiredTotal)
        {
            _array = array;
            _rowCount = array.GetLength(0);
            _colCount = array.GetLength(1);
            _desiredTotal = desiredTotal;
        }

        public List<Path> Search()
        {
            Coords startingPoint = new Coords(0, 0);
            Path path = new Path(_array, _desiredTotal);
            path.AddCoord(startingPoint);
            _paths.Add(path);
            Move(startingPoint, path);
            return _paths;
        }
        private void Move(Coords currentPoint, Path currentPath)
        {
            Path nextRightPath = new Path(currentPath.DataArray, _desiredTotal);
            currentPath.Paths.ForEach(path => { nextRightPath.AddCoord(path); });

            Coords nextPointRight = new Coords(currentPoint.X + 1, currentPoint.Y);
            Path nextDownPath = new Path(currentPath.DataArray, _desiredTotal);
            currentPath.Paths.ForEach(path => { nextDownPath.AddCoord(path); });
            Coords nextPointDown = new Coords(currentPoint.X, currentPoint.Y + 1);

            if (nextPointRight.X + 1 <= _rowCount)
            {
                nextRightPath.AddCoord(nextPointRight);
                if (nextRightPath.GetSum() <= _desiredTotal)
                {
                    _paths.Add(nextRightPath);
                    Move(nextPointRight, nextRightPath);
                }
            }

            if (nextPointDown.Y + 1 <= _colCount)
            {
                nextDownPath.AddCoord(nextPointDown);
                if (nextDownPath.GetSum() <= _desiredTotal)
                {
                    _paths.Add(nextDownPath);
                    Move(nextPointDown, nextDownPath);
                }
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            double[,] array = new double[8, 8];
            InitaliseArray(array);
            GridSearcher gs = new GridSearcher(array, 1);
            List<Path> validPaths = gs.Search().FindAll(path => path.IsValidPath());
            validPaths.ForEach(path => Console.WriteLine(path.ToShortString()));
        }

        static void InitaliseArray(double[,] array)
        {
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
        }
    }
}
