#include "base.h"
#include "Group.h"
#include "PermutationGenerator.h"
#include "CycleCreator.h"
#include "CycleConnector.h"
#include "OutputManager.h"

Group::Group(void)
{
}

Group::Group(const Group &r)
{
    data_ = r.data_;
    vrpSolver_ = r.vrpSolver_;
    groupId_ = r.groupId_;
}

Group::Group(const InputData input, UINT id)
{
    data_ = input;
    groupId_ = id;
}


Group::~Group(void)
{
}

void Group::Solve(vvUINT &table)
{
    PermutationGenerator pg = PermutationGenerator(MAX_CYCLE_SIZE, data_.size);
    pg.CreatePermVector(false);
    vvUSHORT perm = pg.GetConstVecPerm();
    CycleCreator c = CycleCreator(data_, perm, groupId_);
    UINT a = c.CreateCycles();
    a++;
    CycleConnector cc = CycleConnector(c, data_.size, groupId_);
    cc.Connect();
    cc.AddToResultTable(table);
}
