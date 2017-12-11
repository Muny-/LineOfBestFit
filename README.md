# Line of Best Fit
This is a .NET core program which finds the linear line of best fit for a given data set. It was created for a Calculus II class as the final project.

## Dependencies
* The .NET SDK is required to compile and run the program.
* GNUPlot is required to display the generated graph

## How to Run
1. Install .NET Core SDK
	* https://www.microsoft.com/net/learn/get-started
2. Clone/download git repository. Extract it if you downloaded as a ZIP.
3. Within the project folder, run one of the following commands in whichever command line tool you use:
	* ```dotnet run``` to generate the line of best fit using the sample data
	* ```dotnet run <your_file> <x column> <y column>``` to generate the line of best fit for custom data
		* Data shall be formatted as such: 
          ```csv
          xval1,yval1
          xval2,yval2
          xval3,yval3
          ...
          ```
If you have any issues, please feel free to contact me.