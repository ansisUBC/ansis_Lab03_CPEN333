using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Lab3Q1
{
    public class WordCountTester
    {
        static void Main(string[] args)
        {
            try
            {
                string line;
                int startIdx, expectedResults;

                int i = 0;
                string[] testCases = new string[] { 
                    "", 
                    "---ascva-a--", 
                    "///a/df////", 
                    "A", 
                    "kn39kjn", 
                    "AInadf", 
                    ";/,/-=()(s)(+asd><?+_()*&", 
                    "*&(*&sd(*)(*   sample", 
                    "lkjsdf  \n\n\n\n\nsl/dk/fj\t\t sld/kfj             sld-kf-j slkdfj"
                };

                int[] expectRes = new int[] {0, 0, 0, 1, 1, 1, 0, 1, 5};

                //=================================================
                // Implement your tests here. Check all the edge case scenarios.
                // Create a large list which iterates over WCTester
                //=================================================
                for (i = 0; i < testCases.Length; i++)
                {
                    line = testCases[i];
                    startIdx = 0;
                    expectedResults = expectRes[i];
                    WCTester(line, startIdx, expectedResults);
                }
            }
            catch (UnitTestException e)
            {
                Console.WriteLine(e);
            }

        }


        /**
         * Tests word_count for the given line and starting index
         * @param line line in which to search for words
         * @param start_idx starting index in line to search for words
         * @param expected expected answer
         * @throws UnitTestException if the test fails
         */
        static void WCTester(string line, int start_idx, int expected)
        {

            //=================================================
            // Implement: comparison between the expected and
            // the actual word counter results
            //=================================================
            int result = HelperFunctions.WordCount(ref line, start_idx);


            if (result != expected)
            {
                throw new Lab3Q1.UnitTestException(ref line, start_idx, result, expected, String.Format("UnitTestFailed: result:{0} expected:{1}, line: {2} starting from index {3}", result, expected, line, start_idx));
            }
            /* 
            else
            {
                Console.WriteLine("Success.\n\n");
            }
            */
        }
    }
}