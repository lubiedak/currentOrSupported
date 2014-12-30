#include "base.h"
#include "DefaultConditions.h"


DefaultConditions::DefaultConditions(void)
{
    capacity_ = 1000;
    maxCycleSize_ = 5;
    nPoints_ = 20;
    MAXcycleSize_ = 7;
    MINdemand_ = 50;
    MAXlinearCost_ = 100;
}


DefaultConditions::~DefaultConditions(void)
{
}

//extern DefaultConditions defConditions = DefaultConditions();
