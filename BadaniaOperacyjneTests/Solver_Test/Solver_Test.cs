using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BadaniaOperacyjne.Parser;
using BadaniaOperacyjne.DataType;
using BadaniaOperacyjne.Solver;

namespace BadaniaOperacyjneTests.Solver_Test
{
    [TestClass]
    public class Solver_Test
    {
        private IParser parser = new Parser();

        InputData input;

        public Solver_Test()
        {
            input = parser.ReadBinaryProblemFile(@"C:\Users\Patryk\SkyDrive\Dokumenty\10x10.tspf");
            input.FuelCapacity = 1000;
        }

        [TestMethod]
        public void FindClosestPetrolPlace_Test()
        {
            //BadaniaOperacyjne.Solver.Solver solver = new BadaniaOperacyjne.Solver.Solver();

            int closestToFour_400 = Solver.FindClosestPetrolPlace(input, 4, 400);
            int closestToFour_200 = Solver.FindClosestPetrolPlace(input, 4, 200);
            int closestToSeventh_400 = Solver.FindClosestPetrolPlace(input, 7, 400);

            Assert.AreEqual(2, closestToFour_400);
            Assert.AreEqual(-1, closestToFour_200);
            Assert.AreEqual(5, closestToSeventh_400);
        }

        [TestMethod]
        public void CheckIfPossible_Test()
        {
            List<int> solution1 = new List<int> { 0, 1, 4, 7, 9, 6, 3, 0 };
            List<int> solution2 = new List<int> { 0, 4, 9, 3, 6, 1, 7, 0 };
            List<int> solution3 = new List<int> { 0, 9, 6, 4, 1, 3, 8, 0 };
            List<int> solution4 = new List<int> { 0, 1, 3, 2, 6, 4, 7, 9, 0 };
            List<int> solution5 = new List<int> { 0, 1, 3, 2, 6, 4,2, 7, 9, 0 };
            List<int> solution6 = new List<int> { 0, 4, 6, 3, 7, 9, 1, 0 };

            Solver.ClosestPetrol closest1 = Solver.CheckIfPossible(input, solution1);
            Solver.ClosestPetrol closest2 = Solver.CheckIfPossible(input, solution2);
            Solver.ClosestPetrol closest3 = Solver.CheckIfPossible(input, solution3);
            Solver.ClosestPetrol closest4 = Solver.CheckIfPossible(input, solution4);
            Solver.ClosestPetrol closest5 = Solver.CheckIfPossible(input, solution5);
            Solver.ClosestPetrol closest6 = Solver.CheckIfPossible(input, solution6);

            Assert.AreEqual(7, closest1.LastPlaceBeforeOutOfFuel);
            Assert.AreEqual(119, closest1.FuelLeft);

            Assert.AreEqual(3, closest2.LastPlaceBeforeOutOfFuel);
            Assert.AreEqual(128, closest2.FuelLeft);

            Assert.AreEqual(6, closest3.LastPlaceBeforeOutOfFuel);
            Assert.AreEqual(141, closest3.FuelLeft);

            Assert.AreEqual(7, closest4.LastPlaceBeforeOutOfFuel);
            Assert.AreEqual(168, closest4.FuelLeft);

            Assert.IsNull(closest5);

            Assert.AreEqual(3, closest6.LastPlaceBeforeOutOfFuel);
            Assert.AreEqual(69, closest6.FuelLeft);
        }

        [TestMethod]
        public void PutPetrolPlace_Test()
        {
            List<int> solution1 = new List<int> { 0, 1, 4, 7, 9, 6, 3, 0 };
            List<int> expected1 = new List<int> { 0, 1, 4, 2, 7, 9, 2, 6, 5, 3, 0 };

            List<int> solution2 = new List<int> { 0, 4, 9, 3, 6, 1, 7, 0 };
            List<int> expected2 = new List<int> { 0, 4, 9, 2, 3, 6, 5, 1, 2, 7, 0 };

            List<int> solution3 = new List<int> { 0, 9, 6, 4, 1, 3, 7, 0 };
            List<int> expected3 = new List<int> { 0, 9, 2, 6, 4, 2, 1, 3, 7, 5, 0 };

            List<int> solution4 = new List<int> { 0, 4, 6, 3, 7, 9, 1, 0 };
            List<int> expected4 = new List<int> { 0, 4, 6, 5, 3, 7, 5, 9, 1, 0 };

            Solver.PutPetrolPlaces(input, solution1);
            Solver.PutPetrolPlaces(input, solution2);
            Solver.PutPetrolPlaces(input, solution3);
            Solver.PutPetrolPlaces(input, solution4);

            Compare(solution1, expected1);
            Compare(solution2, expected2);
            Compare(solution3, expected3);
            Compare(solution4, expected4);
        }

        public static void Compare<T>(IEnumerable<T> one, IEnumerable<T> another)
        {
            Assert.AreEqual(one.Count(), another.Count());
            IEnumerator<T> oneIterator = one.GetEnumerator();
            IEnumerator<T> anotherIterator = another.GetEnumerator();
            while (oneIterator.MoveNext() && anotherIterator.MoveNext())
            {
                Assert.AreEqual(oneIterator.Current, anotherIterator.Current);
            }
        }
    }
}
