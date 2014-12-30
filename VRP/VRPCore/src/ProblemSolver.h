#pragma once
#include "inputTools/InputManager.h"
#include "OutputManager.h"
#include "GroupManager.h"
#include "CVRPSolver.h"

class ProblemSolver
{
public:
    ProblemSolver(void);
    ProblemSolver(string path, string dir);
    ~ProblemSolver(void);

    UINT Solve(void);
    bool prepareData(void);

    InputManager inputManager_;
    OutputManager outputManager_;
    GroupManager groupManager_;

    UINT defaultGroupSize_;
    vector< Group > groups_;
};

