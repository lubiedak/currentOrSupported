#pragma once
#include "Cycle.h"
#include "CycleCreator.h"
class CycleConnector
{
public:
    CycleConnector(void);
    CycleConnector(CycleCreator creator, USHORT groupSize, UINT groupId);
    ~CycleConnector(void);

    void PrepareData(vUINT &id1, vvUSHORT &join1 );

    UINT CountPossibleJoins(vUINT &toJoinId, vvUSHORT &toJoin);

    bool TryToJoin(
        vUINT &idLeft,
        vUINT &idRight,
        vvUSHORT &joinLeft,
        vvUSHORT &joinRight);

    bool Join(
        vUINT &idBranch,
        vvUSHORT & branch,
        vUINT &idTrunk,
        vvUSHORT & trunk,
        UINT possibleJoins);
        
    UINT IsThisLastTry(vUINT &idTrunk);

    void FullWithConnected(
        vUINT &idTrunk,
        vvUSHORT &trunk,
        UINT connectedCyclesSize);

    void PrintConnected(UINT howMany);

    void AddToResultTable(vvUINT &table);

    vvUINT Connect();

    CycleCreator creator_;
    vvUSHORT baseCycles_;
    vUINT baseId_;
    vUSHORT bestDistances_;
    vvUINT connectedCycles_;
    
    USHORT it_ ;//connection iterator;
    
    USHORT DIST; //shows which vector^2 column stores distance

    bool connectedAll_; //flag to stop joining
    UINT connectedState_;
    USHORT possibleLastIt_;
    
    UINT groupId_;
};

