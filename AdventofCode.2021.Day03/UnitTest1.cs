using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using AdventofCode._2021.Day03.Part1;
using AdventofCode._2021.Day03.Part2;

namespace AdventofCode._2021.Day03
{
    public class UnitTest1
    {
        public static IEnumerable<object[]> Part1Data
        {
            get
            {
                var testData = File
                    .ReadLines("TestData.txt")
                    .Where(x => !String.IsNullOrEmpty(x))
                    .Select(line => line)
                    .ToList();
                
                return new List<object[]>
                {
                    new object[] { new List<string> { "00100", "11110", "00110" }, "00110", "11001", 150 },
                    new object[] { new List<string> { "00100", "11110", "10110", "10111", "10101", "01111", "00111", "11100", "10000", "11001", "00010", "01010" }, "10110", "01001", 198 },
                    new object[] { testData, "", "", 0} 
                };

            }
        }
        
        [Theory]
        [MemberData(nameof(Part1Data))]

        public void CalculateGammaTest(List<string> report, string expectedGamma, string expectedEpsilon, int expectedAnswer)
        {
            // Arrange
            var gammaCalculator = new GammaCalculator();
            var epsilonCalculator = new EpsilonCalculator();

            // Act
            var gamma = gammaCalculator.Calculate(report);
            var espilon = epsilonCalculator.Calculate(gamma);

            var gammaNumber = Convert.ToInt32(gamma, 2);
            var espilonNumber = Convert.ToInt32(espilon, 2);

            // Assert
            Assert.Equal(expectedAnswer, gammaNumber * espilonNumber);
            Assert.Equal(expectedGamma, gamma);
            Assert.Equal(expectedEpsilon, espilon);
        }
        
        public static IEnumerable<object[]> Part2Data
        {
            get
            {
                var testData = File
                    .ReadLines("TestData.txt")
                    .Where(x => !String.IsNullOrEmpty(x))
                    .Select(line => line)
                    .ToList();
                
                return new List<object[]>
                {
                    new object[] { new List<string> { "00100", "11110", "10110", "10111", "10101", "01111", "00111", "11100", "10000", "11001", "00010", "01010" }, "10111", "01010", 230 },
                    new object[] { testData, "", "", 0} 
                };

            }
        }

        [Theory]
        [MemberData(nameof(Part2Data))]
        public void CalculateOxygenRatingTest(List<string> readings, string expectedOxygen, string expectedCo2, int expectedAnswer)
        {
            // Arrange
            var oxygenCalculator = new OxygenCalculator();
            var co2ScrubberCalculator = new Co2ScrubberCalculator();

            // Act
            var actualOxygen = oxygenCalculator.Calculate(readings);
            var actualCo2 = co2ScrubberCalculator.Calculate(readings);
            
            var oxygenNumber = Convert.ToInt32(actualOxygen, 2);
            var co2Number = Convert.ToInt32(actualCo2, 2);

            // Assert
            Assert.Equal(expectedAnswer, oxygenNumber * co2Number);
            Assert.Equal(expectedOxygen, actualOxygen);
            Assert.Equal(expectedCo2, actualCo2);
        }
    }
}


