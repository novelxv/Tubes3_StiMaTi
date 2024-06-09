using System;
using System.Collections.Generic;

namespace algo {
    public static class Levenshtein {
        /* function to compute distance between two strings with levenshtein algorithm */
        public static int ComputeDistance(string str1, string str2){
            int n = str1.Length; 
            int m = str2.Length; 
            int[,] d = new int[n + 1, m + 1]; // matriks untuk menyimpan jarak

            if (n == 0) return m;
            if (m == 0) return n;

            for (int i = 0; i <= n; d[i, 0] = i++) { }
            for (int j = 0; j <= m; d[0, j] = j++) { }

            // mengisi matriks dengan nilai jarak
            for (int i = 1; i <= n; i++){
                for (int j = 1; j <= m; j++){
                    int cost = (str2[j - 1] == str1[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, // cost penghapusan
                        d[i, j - 1] + 1), // cost penyisipan
                        d[i - 1, j - 1] + cost // cost substitusi
                    );
                }
            }

            return d[n, m];
        }

        /* fungsi untuk menghitung kemiripan */
        public static double ComputeSimilarity(string str1, string str2){
            int maxLen = Math.Max(str1.Length, str2.Length); 
            if (maxLen == 0) return 100.0;
            int distance = ComputeDistance(str1, str2); 
            return (1.0 - (double)distance / maxLen) * 100.0;
        }
    }
}