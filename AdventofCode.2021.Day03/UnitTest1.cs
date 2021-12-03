using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace AdventofCode._2021.Day03
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
        }
        
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
                    new object[] { },
                    //new object[] { testData, 0, 0, 0, 0} 
                };

            }
        }
    }
}

