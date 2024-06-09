using System;
using System.Collections.Generic;

namespace algo {
    public static class BoyerMoore {
        /* finds if the pattern is found in the text using the Boyer-Moore algorithm */
        public static bool BMMatch(string pattern, string text){
            string patternNoDuplicate = new(pattern.Distinct().ToArray());
            int[] lastOccurence = LastOccurenceFunctionTable(patternNoDuplicate); 
            int i = pattern.Length - 1;
            int j = pattern.Length - 1;
            while (i < text.Length){
                // looking-glass technique
                if (pattern[j] == text[i]){
                    if (j == 0){
                        return true;
                    }
                    i--;
                    j--;
                }
                // character jump technique
                else {
                    char x = text[i];
                    if (pattern.Contains(x)){
                        int indexOfX = patternNoDuplicate.IndexOf(x);
                        int jx = lastOccurence[indexOfX];
                        if (jx < j){ // case 1
                            i += pattern.Length - (j - jx);
                            j = pattern.Length - 1;
                        }
                        else if (jx > j) { // case 2
                            i += pattern.Length - j;
                            j = pattern.Length - 1;
                        }
                    }
                    else { // case 3
                        i += pattern.Length;
                        j = pattern.Length - 1;
                    }
                }
            }
            return false;
        }


        /* finds the last occurence function table of the pattern */
        public static int[] LastOccurenceFunctionTable(string pattern){
            int[] lastOccurence = new int[pattern.Length];
            for (int i = 0; i < pattern.Length; i++){
                lastOccurence[i] = LastOccurenceFunction(pattern, pattern[i]);
            }
            return lastOccurence;
        }

        /* finds the last occurence of a character in the pattern */
        public static int LastOccurenceFunction(string pattern, char x){
            for (int i = pattern.Length - 1; i >= 0; i--){
                if (pattern[i] == x){
                    return i;
                }
            }
            return -1;
        }
    }
}