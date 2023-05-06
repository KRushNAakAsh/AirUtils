///
/// AirUtils is a simple sort and compress utility application that can be turned into a useful DLL and extended. Right now it is 
/// a console app for demonstration purposes.
/// Author: Leena K
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirUtils
{
    class Program
    {
        static int SUCCESS = 0, INVALID_INPUT = 1, EXCEPTION = -2, exitCode = SUCCESS;
        
        /// <summary>
        /// contains: Method tells if an integer array of a given length contains given number
        /// </summary>
        /// <param name="list">list of integers</param>
        /// <param name="len">operating length of array / list scanned</param>
        /// <param name="min">the value to be searched in the list</param>
        /// <returns>true/false</returns>
        protected static bool contains(int[] list, int len, int min)
        {
            int i;
            if (list == null || len < 1) return false;
            for (i=0; i<len; i++)
            {
                if (list[i] == min)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// swap: Method to swap 2 items in an array of integers
        /// </summary>
        /// <param name="list">list of integers</param>
        /// <param name="idx1">index of first element</param>
        /// <param name="idx2">index of the second element</param>
        /// <returns>true/false</returns>
        protected static bool swap(int[] list, int idx1, int idx2)
        {
            if (list == null || list.Length < 1) return false;

            int temp = list[idx1];
            list[idx1] = list[idx2];
            list[idx2] = temp;

            return true;
        }
        /// <summary>
        /// Method: inputSort()
        ///  Accepts an array of integers and sorts it in ascending order. Returns sorted array.
        /// </summary>
        /// <param name="intArray"></param>
        /// <returns>The sorted array of integers - sortedInts int[] </returns>
        protected static int[] inputSort(int[] intArray)
        {
            int[] sortedInts;
            if (intArray == null)
            {
                sortedInts = new int[0];
            }
            else
            {
                int[] workSpace = new int[intArray.Length];
                sortedInts = new int[intArray.Length];

                int i, j, min = intArray[0], minPtr = 0, cur = 0, len = intArray.Length, setSz = intArray.Length;

                
                for (i = 0; i < len; i++)
                {
                    workSpace[i] = intArray[i];
                    if (intArray[i] < min && !contains(sortedInts, cur, min))
                    {
                        min = intArray[i];
                        minPtr = i;
                    }
                }

                do
                {
                    sortedInts[cur++] = min;
                    if (minPtr < setSz-1) swap(workSpace, minPtr, setSz-1); // toss the min to the end so it is not in the set anymore.
                    setSz--;
                    min = workSpace[0]; minPtr = 0;

                    for (i = 0; i < setSz; i++)
                    {
                        if (workSpace[i] < min )
                        {
                            min = workSpace[i];
                            minPtr = i;
                        }
                    }

                } while (setSz >= 0 && cur < len);

            }

            return sortedInts;
        }

        /// <summary>
        /// Method compress : Ingests a string of printable alphabets , RegEx: [A-Za-z]* , to compress the string into a format where
        /// a set of consecutive repeated characters are replaced by the count followed by the repeat character.
        /// e.g.  abertqqqqqqwerwwwwwbyu   will be compressed as  abert6qwer5wbyu
        /// </summary>
        /// <param name="data"> the original string of alphabets</param>
        /// <returns>The compressed string of the alphabets as described above.</returns>
        protected static String compress(String data)
        {
            String compressedInfo = "";
            int i = 0, j = 0, count = 0;

            while (i < data.Length)
            {
                if ( data[i] < 'A' || data[i] > 'z' || ( data[i] > 'Z' && data[i] < 'a' ) )
                {
                    Console.WriteLine("");
                    Console.WriteLine("Invalid input string for compression. Must be alphabets only : [A-Za-z]*");
                    exitCode = INVALID_INPUT;
                    return compressedInfo;
                }

                j = i; count = 1;
                while (j < data.Length - 1 && data[j] == data[j + 1])
                {
                    count++;
                    j++;
                }
                if (count > 1)
                {
                    compressedInfo += count.ToString();
                }
                compressedInfo += data[i];
                i = j+1;
            }
            return compressedInfo;

        }

        static void Main(string[] args)
        {
            String inputSort = "";
            char[] commaSep = { ',' };
            int intVal = 0, i=0;

            try
            {

                Console.WriteLine("Welcome to CPatSortAndCompress. Please enter up to a maximum of 20 integers separated by commas: ");
                inputSort = Console.ReadLine();


                string[] userNumList = inputSort.Split(commaSep, StringSplitOptions.RemoveEmptyEntries);
                int[] userIntList = new int[userNumList.Length], sortedList = new int[userNumList.Length];

                foreach (String num in userNumList)
                {
                    if (int.TryParse(num, out intVal))
                    {
                        userIntList[i++] = intVal;
                    }
                    else
                    {
                        Console.WriteLine("Input number {0} has invalid format. Should be an integer only. Only the valid integers will be considered for sorting.", num);
                    }
                }

                sortedList = Program.inputSort(userIntList);

                Console.WriteLine("Your integers have been sorted: ");
                for (i=0; i<sortedList.Length; i++)
                {
                    Console.Write(" " + sortedList[i]);
                }
                Console.WriteLine("");
                Console.WriteLine("---------------------Press any key to continue to the next exercise --------------------------");
                Console.ReadKey();

                Console.WriteLine("");
                Console.Write (" That was great! Now please enter a string of printable alphabets and press enter : ");
                String data = Console.ReadLine();
                data.Replace(Environment.NewLine,  "");

                String compressedInfo = compress(data);
                Console.WriteLine("");
                if (exitCode == SUCCESS)
                {
                    Console.WriteLine("This is the resulting compressed version of the string : ");
                    Console.WriteLine("");
                    Console.WriteLine("COMPRESSED INFO: " + compressedInfo);
                }
                Console.WriteLine("");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                Console.WriteLine("--------------------- GOOD BYE! --------------------------");


                Environment.Exit(exitCode);

            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred during the sort or compress process. " + e.Message);
                Environment.Exit(EXCEPTION);
            }


        }

       
    }

    
}
