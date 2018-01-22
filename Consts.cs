namespace LineOfBestFit
{
    public static class Consts
    {
        public static string PlotString(double best_fit_line_slope, double y_intercept)
        {
            return @"set terminal wxt size 800,600 enhanced font 'Lato Light,12' persist

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
        }
    }
}