namespace algo {
    public static class KMP {
        /* finds if the pattern is found in the text using KMP algorithm */
        public static bool kmpMatch(string pattern, string text){
            int i = 0;
            int j = 0;
            while (i < text.Length){
                if (pattern[j] == text[i]){
                    if (j == pattern.Length - 1){
                        return true;
                    }
                    i++;
                    j++;
                }
                else if (j == 0){
                    i++;
                }
                else {
                    int k = j - 1;
                    j = borderFunction(pattern, k);
                }
            }
            return false;
        }

        /* finds the border function of the pattern */
        public static int borderFunction(string pattern, int k){
            if (k == 0){
                return 0;
            }
            string[] prefixes = allPrefixes(pattern[..k]);
            string[] suffixes = allSuffixes(pattern[1..k]);
            int maxLength = 0;
            foreach (string s in prefixes){
                foreach (string t in suffixes){
                    if (s == t){
                        maxLength = Math.Max(maxLength, s.Length);
                    }
                }
            }
            return maxLength;
        }

        /* finds all prefixes of the pattern */
        public static string[] allPrefixes(string pattern){
            string[] prefixes = new string[pattern.Length];
            for (int i = 0; i < pattern.Length; i++){
                prefixes[i] = pattern[..i];
            }
            return prefixes;
        }

        /* finds all suffixes of the pattern */
        public static string[] allSuffixes(string pattern){
            string[] suffixes = new string[pattern.Length];
            for (int i = 0; i < pattern.Length; i++){
                suffixes[i] = pattern[i..];
            }
            return suffixes;
        }
    }
}