#pragma once
#include "CVRPSolver.h"
#include "inputTools/InputData.h"
#include "testTools/VRPStats.h"

class Group
{
public:
    Group(void);
    Group(const InputData input, UINT id);
    Group(const Group &r);
    ~Group(void);
    void Solve(vvUINT &table);

    InputData data_;

    CVRPSolver vrpSolver_;
    UINT groupId_;
};

