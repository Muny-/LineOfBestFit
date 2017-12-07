/*
 *  Finds an equation for the line of best fit for a given data set
 *  
 *
 *                        ~Team Panthers~
 */

using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using UnityEngine;

namespace LineOfBestFit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Loading input vectors...");

            // Read all of InputDataPoints.txt (or first program argument)
            // into an array of strings, where the delimiter is a \n
            // character, then convert that into a list of DataPoint objects (x, y) 
            List<DataPoint> data_points = File.ReadAllLines(args.Length > 0 ? args[0]: "InputDataPoints.txt")
                .Where(val => !val.StartsWith("#")).ToList().ConvertAll(val => new DataPoint(ParseVector(val, 25, 13))).ToList();

            Console.WriteLine("done");


            // Find the average of all x values
            double X_Avg = data_points.Average(val => val.Location.x);

            // Find the average of all y values
            double Y_Avg = data_points.Average(val => val.Location.y);

            // Some verbosity...
            Console.WriteLine("X_Avg: " + X_Avg + ", Y_Avg: " + Y_Avg);

            // Housekeeping...
            data_points.ForEach(val => val.SetAvgs(X_Avg, Y_Avg));

            // Find the sum of all of each item's (XAvgDist*YAvgDist) value
            // This is justified in the write-up
            double sum_distance_products = data_points.Sum(val => val.DistanceFromX_Avg * val.DistanceFromY_Avg);

            // Find the sum of all of each item's (XAvgDist^2) value
            // Or, in other words, find the sum of all of the errors^2
            // We square the errors here for the following reasons:
            //   We don't want negatives (abs sounds like a good option, but...)
            //   We want dval/derr to not be proportional, exponential instead; plus
            //      abs values are ugly
            double sum_xavg_dist_squares = data_points.Sum(val => val.DistanceFromX_Avg * val.DistanceFromX_Avg);

            // Find the slope of the best fit line by dividing the sum of the distance products by the x dist squares.
            // Justified in the write-up
            double best_fit_line_slope = sum_distance_products / sum_xavg_dist_squares;

            // Find the y intercept of the line of best fit
            // Justified in the write-up
            double y_intercept = Y_Avg - (best_fit_line_slope * X_Avg);

            // So finally the equation looks like:
            // 
            //       y = best_fit_line_slope*x + y_intercept
            //

            Console.WriteLine("Equation: y = " + best_fit_line_slope + "x + " + y_intercept);

            // [Housekeeping...] Write plot visualization files

            StringBuilder plot_data = new StringBuilder();

            plot_data.AppendLine("#  X     Y1");

            foreach(DataPoint dp in data_points)
            {
                plot_data.AppendLine(dp.Location.x + "   " + dp.Location.y);
            }

            File.WriteAllText("OutputPlotData.dat", plot_data.ToString());

            string gnuplot = @"set terminal wxt size 800,600 enhanced font 'Lato Light,12' persist

# Line width of the axes
set border linewidth 1.5
# Line styles
set style line 2 linecolor rgb '#dd181f' linetype 1 linewidth 2

set style line 1 lc rgb '#0060ad' lt 2 lw 1 dt 5 pt 18 pi -1 ps 1.5
set pointintervalbox 3

f(x) = " + best_fit_line_slope + "*x + " + y_intercept + @"

plot 'OutputPlotData.dat' using 1:2 title 'Data Points' with points ls 1, \
    f(x) title 'Line of Best Fit, y = " + best_fit_line_slope + "x + " + y_intercept + @"' with lines linestyle 2
    
pause -1 'press Ctrl-D to exit'";


            // Write output plot to file
            File.WriteAllText("OutputPlot.gnu", gnuplot);

            // Display our results!
            "gnuplot OutputPlot.gnu".Bash();
        }

        // Parses a string that looks like "int,int,int,..." into a Vector2d object using the specified indices for x and y values
        static Vector2d ParseVector(string str, int x_index, int y_index)
        {            
            string[] parts = str.Trim().Split(",");

            if (parts.Length < 2)
                throw new FormatException("Too few characteristics, need at least 2");

            double x;

            if (!double.TryParse(parts[x_index].Trim(), out x))
                throw new FormatException("Error parsing '" + parts[x_index] + "' as double");

            double y;

            if (!double.TryParse(parts[y_index].Trim(), out y))
                throw new FormatException("Error parsing '" + parts[y_index] + "' as double");

            return new Vector2d(x, y);
        }
    }

    // Helper class to give us an easy data structure to work with
    public class DataPoint
    {
        private double _x_avg, _y_avg;

        // Coordinates in cartesian plane
        public Vector2d Location;

        // Horizontal distance from average x value of data
        public double DistanceFromX_Avg
        {
            get
            {
                return this.Location.x - _x_avg;
            }
        }

        // Vertical distance from average y value of data
        public double DistanceFromY_Avg
        {
            get
            {
                return this.Location.y - _y_avg;
            }
        }

        // Class constructor
        public DataPoint(Vector2d location)
        {
            this.Location = location;
        }

        // Housekeeping...
        public override string ToString()
        {
            return Location.ToString();
        }

        // Housekeeping...
        public void SetAvgs(double x_avg, double y_avg)
        {
            _x_avg = x_avg;
            _y_avg = y_avg;
        }
    }

}
