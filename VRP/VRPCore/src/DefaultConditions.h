
#pragma once
class DefaultConditions
{
public:
    DefaultConditions(void);
    ~DefaultConditions(void);
    int getCapacity() const { return capacity_; };
    int getNPoints() const { return nPoints_; };
    int getMaxCycleSize() const { return maxCycleSize_; };
    enum {EXEC = 0, PATH, N_POINTS, MAX_POINTS};

    static const int MAXnPoints_ = 32;
    int MAXcycleSize_;
    int MINdemand_;
    int MAXlinearCost_;

private:
    int capacity_;
    int nPoints_;
    int maxCycleSize_;
     //int size;

};

extern DefaultConditions defConditions;
