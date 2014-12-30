#pragma once
#include "Cycle.h"
#include "inputTools/InputData.h"
#include "Permutation.h"
class CycleCreator
{
public:
    CycleCreator(void);
    CycleCreator(InputData idata, vvUSHORT perms, UINT groupId);
    ~CycleCreator(void);

    UINT FindShortestPerm(vUSHORT &cycle);
    USHORT PermSize(USHORT N);
    USHORT SumCargo( bitset< MAX_BITS > &bin );
    vUSHORT SetDestinations( bitset< MAX_BITS > &bin, USHORT cycleSize );
    UINT CreateCycles();
    UINT CountPossibleCycles(UINT &cycleBasicCount, UINT &cycleRestCount);
    UINT FindPossibleCycles(UINT cycleAllCount, UINT cycleBasicCount, UINT cycleRestCount);
    UINT CountN();
    vector< vector< Cycle > > getAllCycles();

    Cycle getCycleByVecPosition( UINT vecPosition);

    InputData data_;
    vvUSHORT perms_;

    vector <Cycle> basicCycles_;
    vector <Cycle> restCycles_;
    vector <Cycle> allCycles_;
    
    UINT groupId_;
};


