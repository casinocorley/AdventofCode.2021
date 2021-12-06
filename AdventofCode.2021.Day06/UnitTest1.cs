using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Xunit;
using AdventofCode._2021.Day06.Part1;
using AdventofCode._2021.Day06.Part2;
using LanternFishSchool = AdventofCode._2021.Day06.Part1.LanternFishSchool;

namespace AdventofCode._2021.Day06

{
    public class UnitTest1
    {
        private static List<string> GetData(string fileName)
        {
            var fileLines = File
                .ReadLines(fileName)
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(line => line)
                .ToList();

            return fileLines;
        }
        
        public static IEnumerable<object[]> Part1Data
        {
            get
            {
                var puzzleInput = File
                    .ReadLines("puzzleData.txt")
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(line => line)
                    .ToList()
                    .SelectMany(x => x.Split(','))
                    .Select(x => int.Parse(x))
                    .ToList();
                
                return new List<object[]>
                {
                    new object[] {new int[] {1}, 41, 0},
                    new object[] {new int[] {3,4,3,1,2}, 1, 5},
                    new object[] {new int[] {3,4,3,1,2}, 2, 6},
                    new object[] {new int[] {3,4,3,1,2}, 3, 7},
                    new object[] {new int[] {3,4,3,1,2}, 4, 9},
                    new object[] {new int[] {3,4,3,1,2}, 18, 26},
                    new object[] {new int[] {3,4,3,1,2}, 40, 174},
                    new object[] {new int[] {3,4,3,1,2}, 80, 5934},
                    new object[] {puzzleInput, 80, 395627}
                };

            }
        }
        
        [Theory]
        [MemberData(nameof(Part1Data))]
        public void SolvePart1(IEnumerable<int> initialStates, int passingDays, int expected)
        {
            
            // Arrange
            var schoolOfFish = new Part1.LanternFishSchool();
            foreach(var value in initialStates)
                schoolOfFish.Add(new LanternFish(value));

            // Act
            schoolOfFish.PassingDay(passingDays);

            // Assert
            Assert.Equal(expected, schoolOfFish.Count);
        }
        
        public static IEnumerable<object[]> Part2Data
        {
            get
            {
                var puzzleInput = File
                    .ReadLines("puzzleData.txt")
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(line => line)
                    .ToList()
                    .SelectMany(x => x.Split(','))
                    .Select(x => int.Parse(x))
                    .ToList();
                
                return new List<object[]>
                {
                    new object[] {new int[] {3,4,3,1,2}, 1, 5},
                    new object[] {new int[] {3,4,3,1,2}, 2, 6},
                    new object[] {new int[] {3,4,3,1,2}, 3, 7},
                    new object[] {new int[] {3,4,3,1,2}, 4, 9},
                    new object[] {new int[] {3,4,3,1,2}, 18, 26},
                    new object[] {new int[] {3,4,3,1,2}, 80, 5934},
                    new object[] {puzzleInput, 256, 0}
                };
            }
        }

       
        
        
        [Theory]
        [MemberData(nameof(Part2Data))]
        public void SolvePart2(IEnumerable<int> initialStates, int passingDays, int expected)
        {
            // Arrange
            var school1 = new LanternFishSchool();
            school1.Add(new LanternFish(1));
            
            var school2 = new LanternFishSchool();
            school2.Add(new LanternFish(2));
            
            var school3 = new LanternFishSchool();
            school3.Add(new LanternFish(3));
            
            var school4 = new LanternFishSchool();
            school4.Add(new LanternFish(4));
            
            var school5 = new LanternFishSchool();
            school5.Add(new LanternFish(5));

            var schools = new Dictionary<int, LanternFishSchool>()
            {
                { 1, school1 },
                { 2, school2 },
                { 3, school3 },
                { 4, school4 },
                { 5, school5 },
            };
            
            var numberOf1 = initialStates.Count(x => x == 1);
            var numberOf2 = initialStates.Count(x => x == 2);
            var numberOf3 = initialStates.Count(x => x == 3);
            var numberOf4 = initialStates.Count(x => x == 4);
            var numberOf5 = initialStates.Count(x => x == 5);
            
            var localLock = new object();

            var answers = new Dictionary<int, BigInteger>();
            
            // Act
            Parallel.ForEach(schools, school =>
            {
                for (var i = 0; i < passingDays; i++)
                {
                    school.Value.PassingDay();
                }

                lock (localLock)
                {
                    answers.Add(school.Key, school.Value.Count());
                }
            });

            var total =
                school1.Count * numberOf1 +
                school2.Count * numberOf2 +
                school3.Count * numberOf3 +
                school4.Count * numberOf4 +
                school5.Count * numberOf5;
            
            // Assert
            Assert.Equal(expected, total);
        }
        
        public static IEnumerable<object[]> Part2AgainData
        {
            get
            {
                return new List<object[]>
                {
                    new object[] {new int[] {6},  1, 1},
                    new object[] {new int[] {6},  6, 1},    // right before 1st birth
                    new object[] {new int[] {6},  7, 2},    // day of birth
                    new object[] {new int[] {6}, 13, 2},    // 1st right before 2nd birth, 
                    new object[] {new int[] {6}, 14, 4},    // 1st gave birth, 2nd birth is in 2 days
                    new object[] {new int[] {6}, 16, 8},    // 1st birth is days in, 2nd gave birth
                    new object[] {new int[] {6}, 21, 8},    
                    
                    /*
                    new object[] {new int[] {5},  1, 1},
                    new object[] {new int[] {5},  5, 1},
                    new object[] {new int[] {5},  6, 2},
                    new object[] {new int[] {5},  7, 2},
                    new object[] {new int[] {5},  8, 2},
                    new object[] {new int[] {5},  9, 2},
                    new object[] {new int[] {5}, 10, 2},
                    new object[] {new int[] {5}, 11, 2},
                    new object[] {new int[] {5}, 12, 2},
                    new object[] {new int[] {5}, 13, 4},
                    new object[] {new int[] {5}, 19, 4},
                    new object[] {new int[] {5}, 20, 8},
                    new object[] {new int[] {5}, 26, 8},
                    new object[] {new int[] {5}, 27, 16},
                    
                    new object[] {new int[] {4},  1, 1},
                    new object[] {new int[] {4},  4, 1},
                    new object[] {new int[] {4},  5, 2},
                    new object[] {new int[] {4}, 11, 2},
                    new object[] {new int[] {4}, 12, 4},
                    new object[] {new int[] {4}, 18, 4},
                    new object[] {new int[] {4}, 19, 8},

                    new object[] {new int[] {6, 6},  1,  2},
                    new object[] {new int[] {6, 6},  6,  2},
                    new object[] {new int[] {6, 6},  7,  4},
                    new object[] {new int[] {6, 6}, 13,  4},
                    new object[] {new int[] {6, 6}, 14,  8},
                    new object[] {new int[] {6, 6}, 21, 16},
                    
                    new object[] {new int[] {5, 5},  1,  2},
                    new object[] {new int[] {5, 5},  5,  2},
                    new object[] {new int[] {5, 5},  6,  4},
                    new object[] {new int[] {5, 5}, 12,  4},
                    new object[] {new int[] {5, 5}, 13,  8},
                    new object[] {new int[] {5, 5}, 21, 16},
                    
                    new object[] {new int[] {6, 5},  1,  1+1},
                    new object[] {new int[] {6, 5},  5,  1+1},
                    new object[] {new int[] {6, 5},  6,  1+2},
                    new object[] {new int[] {6, 5},  7,  2+2},
                    new object[] {new int[] {6, 5}, 12,  2+2},
                    new object[] {new int[] {6, 5}, 13,  2+4},
                    new object[] {new int[] {6, 5}, 14,  4+4},
                    new object[] {new int[] {6, 5}, 21,  8+8},
                    */
                    /*
                    new object[] {new int[] {3,4,3,1,2}, 1, 5},
                    new object[] {new int[] {3,4,3,1,2}, 2, 6},
                    new object[] {new int[] {3,4,3,1,2}, 3, 7},
                    new object[] {new int[] {3,4,3,1,2}, 4, 9},
                    new object[] {new int[] {3,4,3,1,2}, 18, 26},
                    new object[] {new int[] {3,4,3,1,2}, 80, 5934},
                     */
                };
            }
        }
        
        [Theory]
        [MemberData(nameof(Part2AgainData))]
        public void GetNumberOfFishTest(int[] initialStates, int days, int expected)
        {
            // Arrange
            var calculator = new FishSpawnCalculator();

            // Act
            var actual = calculator
                .GetNumberOfFish(initialStates, days);
            

            // Assert
            Assert.Equal(expected, actual);

        }
    }
}

