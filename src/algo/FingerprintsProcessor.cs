using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Database;

namespace algo {
    public class FingerprintsProcessor {
        /* function to process fingerprints */
        public static (string, double, double) ProcessFingerprints(string inputFingerprintImagePath, List<string> databaseFingerprints, bool useKmp){
            string bestMatchFingerprint = null;
            double bestMatchPercentage = 0;

            // start timer
            Stopwatch stopwatch = Stopwatch.StartNew();
            
            // segment input image
            List<string> inputSegments = SegmentImage(inputFingerprintImagePath, 30);

            foreach (var dbFingerprintPath in databaseFingerprints){
                string dbAscii = DataProcessor.ConvertImageToAscii(dbFingerprintPath);

                double matchPercentage = CompareSegments(inputSegments, dbAscii, useKmp);
                
                if (matchPercentage > bestMatchPercentage){
                    bestMatchFingerprint = dbFingerprintPath;
                    bestMatchPercentage = matchPercentage;
                }
            }

            // Stop timer
            stopwatch.Stop();
            double executionTime = stopwatch.Elapsed.TotalMilliseconds;

            if (bestMatchFingerprint == null){
                return (null, executionTime, 0);
            }

            return (bestMatchFingerprint, executionTime, bestMatchPercentage);
        }

        /* function to segment image */
        private static List<string> SegmentImage(string inputFingerprintImagePath, int segmentSize){
            Image<Rgba32> image = DataProcessor.LoadBitmap(inputFingerprintImagePath);
            List<string> segments = new List<string>();

            for (int i = 0; i < image.Width; i += segmentSize){
                int width = Math.Min(segmentSize, image.Width - i);
                string segment = DataProcessor.ConvertToAscii(image.Clone(new Rectangle(i, 0, width, image.Height)));
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
