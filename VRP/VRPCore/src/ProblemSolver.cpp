#include "base.h"
#include "ProblemSolver.h"


ProblemSolver::ProblemSolver(void)
{
}

ProblemSolver::ProblemSolver(string in_path, string dir)
{
    inputManager_ = InputManager(in_path);
    outputManager_ = OutputManager(dir, in_path);
    defaultGroupSize_ = 16;
}

bool
ProblemSolver::prepareData()
{
    bool prepareDataSuccessful = inputManager_.prepareData();
    if(!prepareDataSuccessful)
    {
        throw inputManager_.errorMessage;
    }
    groupManager_ = GroupManager(inputManager_.data.size/defaultGroupSize_, inputManager_.data);
}


ProblemSolver::~ProblemSolver(void)
{
}

UINT ProblemSolver::Solve(void)
{
    
    groupManager_.DivideIntoGroups();
    outputManager_.ExportGroupDivision(groupManager_.ExportDivideResults());
    groupManager_.SolveAll();
    outputManager_.ExportResults(groupManager_.resultTable_);
    return 0;
}
