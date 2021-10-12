using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace GameOfLife
{
    public class GameOfLiveV2_UnitTest_onCodeOfElmar
    {
        public class GameOfLiveV2_UnitTest_HardcodedBoards
        {
            #region Constants and other hardcoded variables

            private enum TestTarget
            {
                All,
            };

            #endregion

            #region Get mock data

            public static IEnumerable<object[]> GetAllMockData()
            {
                yield return new object[]
                {
                    TestData.Sample3x3BoardA,
                    1,
                    TestData.Sample3x3BoardA_After1Iteration
                };

                yield return new object[]
                {
                    TestData.Sample3x3BoardB,
                    2,
                    TestData.Sample3x3BoardB_After3Iterations
                };

                yield return new object[]
                {
                    TestData.Sample5x5BoardA,
                    2,
                    TestData.Sample5x5BoardA_After2Iterations
                };
            }

            #endregion

            #region PartnerBookingSegregatorService related tests

            [Theory]
            [MemberData(nameof(GetAllMockData))]
            [Trait("Type", "Unit Test - Domain Service")]
            public void GameOfLifeV2_ApplyGameOfLife_ShouldProcessCorrectly(
                bool[,] inputMatrix,
                int numberOfIterations,
                bool[,] expectedMatrix)
            {
                // Findings:
                // NbMatrix has no handling for value of 2?
                // lifeMatrix in code looks good, only NbMatrix might need updates, hence failing test

                var outputMatrix = Program.EvaluateGameOfLife(inputMatrix, numberOfIterations);

                ValidateOutputMatrix(
                    outputMatrix, expectedMatrix, TestTarget.All);
            }

            #endregion

            #region Private assertion helper methods

            private void ValidateOutputMatrix(
                int[,] outputMatrix,
                bool[,] expectedMatrix,
                TestTarget target = TestTarget.All)
            {
                // Note: Simple test only. We did not attempt to test each of the smaller methods individually.
                if (target == TestTarget.All)
                {
                    outputMatrix.ShouldNotBeNull();
                    outputMatrix.GetLength(0).ShouldBe(expectedMatrix.GetLength(0));
                    outputMatrix.GetLength(1).ShouldBe(expectedMatrix.GetLength(1));

                    // Check for values on each cell.
                    for (int horizontalCtr = 0; horizontalCtr < outputMatrix.GetLength(0); horizontalCtr++)
                    {
                        for (int verticalCtr = 0; verticalCtr < outputMatrix.GetLength(1); verticalCtr++)
                        {
                            var outputMatrixResult = outputMatrix[horizontalCtr, verticalCtr];

                            if(outputMatrixResult == 0 || 
                               outputMatrixResult == 1 ||
                               outputMatrixResult >= 4)
                                expectedMatrix[horizontalCtr, verticalCtr].ShouldBe(false);

                           else if(outputMatrixResult == 3)
                                expectedMatrix[horizontalCtr, verticalCtr].ShouldBe(true);
                        }
                    }
                }
            }

            #endregion

            #region Test data

            private static class TestData
            {
                #region 3x3 boards

                public static bool[,] Sample3x3BoardA = new bool[,]
                {
                    { false, false, false },
                    { true, true, true },
                    { false, false, false }
                };

                public static bool[,] Sample3x3BoardA_After1Iteration = new bool[,]
                {
                    // Results in vertical line
                    { false, true, false },
                    { false, true, false },
                    { false, true, false }
                };

                public static bool[,] Sample3x3BoardB = new bool[,]
                {
                    { false, true, false },
                    { true, true, true },
                    { false, true, false },
                };

                public static bool[,] Sample3x3BoardB_After3Iterations = new bool[,]
                {
                    // Results in four diagonals as populated
                    { true, false, true },
                    { false, false, false },
                    { true, false, true },
                };

                #endregion

                #region 5x5 boards

                public static bool[,] Sample5x5BoardA = new bool[,]
                {
                    { false, false, false, false, false },
                    { false, false, false, false, false },
                    { true, true, true, true, true },
                    { false, false, false, false, false },
                    { false, false, false, false, false },
                };

                public static bool[,] Sample5x5BoardA_After2Iterations = new bool[,]
                {
                    // Results in diamond shape with empty inside
                    { false, false, true, false, false },
                    { false, true, false, true, false },
                    { true, false, false, false, true },
                    { false, true, false, true, false },
                    { false, false, true, false, false },
                };

                #endregion
            }

            #endregion
        }
    }
}
