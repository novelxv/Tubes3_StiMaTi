public static class BoyerMoore {
    /* Function to match two patterns with Boyer-Moore Algorithm */
    public static bool isAMatch(string pattern1, string pattern2){
        // array untuk menyimpan bad character
        int[] badChar = new int[256];
        // buat bad character array
        makeBadChar(pattern1, pattern1.Length, ref badChar);

        // array untuk menyimpan good suffix
        int[] goodSuffix = new int[pattern1.Length];
        // buat good suffix array
        makeGoodSuffix(pattern1, pattern1.Length, ref goodSuffix);

        int shiftIndex = 0;
        // iterasi sepanjang pattern2
        while (shiftIndex <= (pattern2.Length - pattern1.Length)){
            int i = pattern1.Length - 1;
            // iterasi sepanjang pattern1
            while (i >= 0 && pattern1[i] == pattern2[shiftIndex + i]){
                i--;
            }
            // jika pattern1 sudah habis
            if (i < 0){
                return true;
            } 
            else {
                // geser pattern1
                shiftIndex += Math.Max(goodSuffix[i + 1], i - badChar[pattern2[shiftIndex + i]]);
            }
        }
        return false;
    }

    /* Function to create the bad character array */
    private static void makeBadChar(string pattern, int length, ref int[] badChar){
        // inisialisasi semua karakter dengan -1
        for (int i = 0; i < 256; i++){
            badChar[i] = -1;
        }
        // isi bad character array dengan indeks terakhir dari karakter
        for (int i = 0; i < length; i++){
            badChar[(int)pattern[i]] = i;
        }
    }

    /* Function to create the good suffix array */
    private static void makeGoodSuffix(string pattern, int length, ref int[] goodSuffix){
        // array untuk menyimpan nilai border
        int[] border = new int[length + 1];
        int i = length;
        int j = length + 1;
        border[i] = j;

        // iterasi dari belakang
        while (i > 0){
            // jika karakter pattern tidak cocok dengan border
            while (j <= length && pattern[i - 1] != pattern[j - 1]){
                if (goodSuffix[j] == 0){
                    goodSuffix[j] = j - i;
                }
                j = border[j]; // geser border
            }
            i--;
            j--;
            border[i] = j;
        }

        j = border[0];
        // iterasi dari depan
        for (i = 0; i <= length; i++){
            if (goodSuffix[i] == 0){
                goodSuffix[i] = j;
            }
            if (i == j){
                j = border[j];
            }
        }
    }
}