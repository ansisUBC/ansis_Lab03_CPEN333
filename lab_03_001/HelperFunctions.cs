using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Microsoft.VisualBasic;
using System.Collections.Specialized;

namespace Lab3Q1
{
    public class HelperFunctions
    {
        /**
         * Counts number of words, separated by spaces, in a line.
         * @param line string in which to count words
         * @param start_idx starting index to search for words
         * @return number of words in the line
         */
        public static int WordCount(ref string line, int start_idx)
        {
            // YOUR IMPLEMENTATION HERE
            int i = 0;
            string lowerCaseConversion  =               line.ToLower();
            char[] charArray            =               lowerCaseConversion.ToArray();

            int charCount = 0;
            int wordCount = 0;
            int nonLetter = 0;

            for (i = start_idx; i < line.Length; i++)
            {
                char testChar = charArray[i];
                if (   (!Char.IsDigit(charArray[i])) && (!Char.IsLetter(charArray[i])) && (!Char.IsWhiteSpace(charArray[i])) && (!charArray[i].Equals('-') && !charArray[i].Equals('/'))   )  // Not anything conventional
                {
                    if ( (charCount != 0) )
                    {
                        charCount = 0;
                        if (wordCount > 0)
                        { 
                            wordCount--;        // Anything strange characters within a series of letters is disqualifying.
                        }
                    }


                    while (i < line.Length)
                    {


                        if (Char.IsWhiteSpace(charArray[i]))
                        {
                            break;
                        }


                        i++;
                    }
                }
                else 
                {
                    if (Char.IsLetterOrDigit(charArray[i])) // The character at the specific index is an alphabetical symbol.
                    {
                        if (charCount == 0) // Beginning of a word.
                        {
                            wordCount++;
                            charCount++;
                        }
                        else
                        {
                            charCount++;
                        }
                    }
                    else if (  (i < (line.Length - 1)) && (charArray[i].Equals('-') || charArray[i].Equals('/'))   )
                    {

                        if (   (Char.IsLetter(charArray[(i + 1)])) && (Char.IsLetter(charArray[(i - 1)]))   ) // If the word is hyphenated
                        {
                            charCount++;
                        }
                        else                                        // Word has a hyphen at the end but not a letter afterwards.
                        {
                            charCount = 0;
                            if (wordCount > 0)
                            {
                                wordCount--;
                            }

                            while (i < line.Length)                 // If a hyphen or dash is not properly used, then it is a disqualifying word count.
                            {
                                
                                if (Char.IsWhiteSpace(charArray[i]))    // If there exist any consecutive repetition of non-digit or non-letter characters, the entire "word" is omitted.
                                {
                                    break;
                                }
                                i++;
                            }
                        }

                    }
                    else        // Not a letter.
                    {
                        if (charCount != 0) // Check if the previous character was part of a word.
                        {
                            charCount = 0;  // Reset letter count.
                        }
                    }
                }
            }



            return wordCount;


        }
    


        /**
        * Reads a file to count the number of words each actor speaks.
        *
        * @param filename file to open
        * @param mutex mutex for protected access to the shared wcounts map
        * @param wcounts a shared map from character -> word count
        */
        public static void CountCharacterWords(string filename, Mutex mutex, Dictionary<string, int> wcounts)
        {

            //===============================================
            //  IMPLEMENT THIS METHOD INCLUDING THREAD SAFETY
            //===============================================

            string line;  // for storing each line read from the file
            string character = "";  // empty character to start
            System.IO.StreamReader file = new System.IO.StreamReader(filename);

            while ((line = file.ReadLine()) != null)
            {
                //=================================================
                // YOUR JOB TO ADD WORD COUNT INFORMATION TO MAP
                //=================================================

                // Is the line a dialogueLine?
                //    If yes, get the index and the character name.
                //      if index > 0 and character not empty
                //        get the word counts
                //          if the key exists, update the word counts
                //          else add a new key-value to the dictionary
                //    reset the character

            }
            // Close the file
        }



        /**
         * Checks if the line specifies a character's dialogue, returning
         * the index of the start of the dialogue.  If the
         * line specifies a new character is speaking, then extracts the
         * character's name.
         *
         * Assumptions: (doesn't have to be perfect)
         *     Line that starts with exactly two spaces has
         *       CHARACTER. <dialogue>
         *     Line that starts with exactly four spaces
         *       continues the dialogue of previous character
         *
         * @param line line to check
         * @param character extracted character name if new character,
         *        otherwise leaves character unmodified
         * @return index of start of dialogue if a dialogue line,
         *      -1 if not a dialogue line
         */
        static int IsDialogueLine(string line, ref string character)
        {
                
            // new character
            if (line.Length >= 3 && line[0] == ' '
                && line[1] == ' ' && line[2] != ' ')
            {
                // extract character name

                int start_idx = 2;
                int end_idx = 3;
                while (end_idx <= line.Length && line[end_idx - 1] != '.')
                {
                    ++end_idx;
                }

                // no name found
                if (end_idx >= line.Length)
                {
                    return 0;
                }

                // extract character's name
                character = line.Substring(start_idx, end_idx - start_idx - 1);
                return end_idx;
            }

            // previous character
            if (line.Length >= 5 && line[0] == ' '
                && line[1] == ' ' && line[2] == ' '
                && line[3] == ' ' && line[4] != ' ')
            {
                // continuation
                return 4;
            }

            return 0;
        }

        /**
         * Sorts characters in descending order by word count
         *
         * @param wcounts a map of character -> word count
         * @return sorted vector of {character, word count} pairs
         */
        public static List<Tuple<int, string>> SortCharactersByWordcount(Dictionary<string, int> wordcount)
        {
            List<Tuple<int, string>> sortedByValueList = new List<Tuple<int, string>>();
            // Implement sorting by word count here
            Tuple<int, string>[] tupleArray = new Tuple<int, string>[wordcount.Count];
            int i = 0;

            OrderedDictionary orderedWordCount = new OrderedDictionary();

            foreach (KeyValuePair<string, int> pair in wordcount)
            {
                orderedWordCount.Add(pair.Key, pair.Value);
            }

            foreach (KeyValuePair<string, int> pair in orderedWordCount)
            {
                if (i < tupleArray.Length)
                {
                    tupleArray[i] = new Tuple<int, string>(pair.Value, pair.Key);
                } 
                else {
                    Console.WriteLine("Error: 'i' index is out of bounds.\n");
                }

                i++;
            }

            var result = tupleArray.OrderByDescending(a => a.Item1);
            foreach (var item in result)
            {
                sortedByValueList.Add(item);
            }

            return sortedByValueList;

        }
        

        /**
         * Prints the List of Tuple<int, string>
         *
         * @param sortedList
         * @return Nothing
        */
        public static void PrintListofTuples(List<Tuple<int, string>> sortedList)
        {

            // Implement printing here

        }
    }
}