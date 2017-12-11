# Line of Best Fit
.NET core program which finds the linear line of best fit for a given data set.

## How to Run
1. Install .NET Core SDK
	* https://www.microsoft.com/net/learn/get-started
2. Clone/download git repository. Extract if downloaded in as ZIP.
3. Within project folder, run
	* ```dotnet run``` to generate the line of best fit using the sample data
	* ```dotnet run <your_file> <x column> <y column>``` to generate the line of best fit for custom data
		* Data shall be formatted as such: 
          ```csv
          xval1,yval1
          xval2,yval2
          xval3,yval3
          ...
          ```
If you have any issues, feel free to contact me.