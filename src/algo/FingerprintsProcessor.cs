using System.Diagnostics;
using Database;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace algo {
    public class FingerprintsProcessor {
        /* function to process fingerprints */
        public static (string?, double, double) ProcessFingerprints(string inputFingerprintImagePath, List<string> databaseFingerprints, bool useKmp){
            string? bestMatchFingerprint = null;
            double bestMatchPercentage = 0;

            // start timer
            Stopwatch stopwatch = Stopwatch.StartNew();
            
            // segment input image
            List<string> inputSegments = SegmentImage(inputFingerprintImagePath, 30);

            foreach (var dbFingerprint in databaseFingerprints){
                double matchPercentage = CompareSegments(inputSegments, dbFingerprint, useKmp);
                
                if (matchPercentage > bestMatchPercentage){
                    bestMatchFingerprint = dbFingerprint;
                    bestMatchPercentage = matchPercentage;
                }
            }

            // jika tidak ditemukan match dengan KMP atau BM
            if (bestMatchFingerprint == null){
                foreach (var dbFingerprint in databaseFingerprints){
                    string inputAscii = DataProcessor.ConvertImageToAscii(inputFingerprintImagePath);
                    double matchPercentage = Levenshtein.ComputeSimilarity(inputAscii, dbFingerprint);

                    if (matchPercentage > bestMatchPercentage && matchPercentage >= 70.0){
                        bestMatchFingerprint = dbFingerprint;
                        bestMatchPercentage = matchPercentage;
                    }
                }
            }

            // Stop timer
            stopwatch.Stop();
            double executionTime = stopwatch.Elapsed.TotalMilliseconds;

            if (bestMatchFingerprint == null){
                // tidak ada match
                return (null, executionTime, 0);
            }

            return (bestMatchFingerprint, executionTime, bestMatchPercentage);
        }

        /* function to segment image */
        private static List<string> SegmentImage(string inputFingerprintImagePath, int segmentSize){
            Image<Rgba32> image = DataProcessor.LoadBitmap(inputFingerprintImagePath);
            List<string> segments = [];

            for (int i = 0; i < image.Width; i += segmentSize){
                int width = Math.Min(segmentSize, image.Width - i);
                var segmentImage = image.Clone(ctx => ctx.Crop(new Rectangle(i, 0, width, image.Height)));
                string segment = DataProcessor.ConvertToAscii(segmentImage);
                segments.Add(segment);
            }

            return segments;
        }

        /* function to compare segments */
        private static double CompareSegments(List<string> inputSegments, string dbAscii, bool useKmp){
            int totalSegments = inputSegments.Count;
            int matchedSegments = 0;

            foreach (var segment in inputSegments){
                if (useKmp){
                    if (KMP.KMPMatch(segment, dbAscii)){
                        matchedSegments++;
                    }
                }
                else {
                    if (BoyerMoore.BMMatch(segment, dbAscii)){
                        matchedSegments++;
                    }
                }
            }

            return (double)matchedSegments / totalSegments * 100;
        }
    }
}
