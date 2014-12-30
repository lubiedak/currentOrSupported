// pureCVRP.cpp : Defines the entry point for the console application.
//
#include "inputTools/ArgParser.h"
#include "ProblemSolver.h"
#include "testTools/VRPStats.h"


int main(int argc, char** argv)
{
    ArgParser argParser(argc, argv);

    if ( argParser.parse() )
    {
        option::Option* options = argParser.getOptions();

        try
        {
            ProblemSolver problem(options[POINTS].arg, options[WORKSPACE].arg);
            problem.prepareData();
            problem.Solve();
        }
        catch(const std::string& e)
        {
            cout << e << endl;
        }
        
        RunStatsManager::getManager().exportToFile(options[WORKSPACE].arg);
    }


    return 0;
}

