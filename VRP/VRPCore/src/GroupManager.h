#pragma once
#include "Group.h"
#include "inputTools/InputManager.h"
class GroupManager
{
public:
    GroupManager(void);
    GroupManager(UINT size, InputData &input_data);
    ~GroupManager(void);

    void SeparateFreeAndGroupedCities(
        UINT size,
        const vvUINT &toGroups,
        vector<vvUINT> &bases,
        vector<vvUINT> &buffers);

    void DivideIntoGroups(void);

    vvUINT ExportDivideResults(void);

    void ReGroup(void);

    void SolveAll(void);

    vvUINT PossibleConfigurations(UINT size);

    vUINT ConvertToBitVector(UINT size, UINT number);

    UINT VectorSum(vUINT &vec);

    UINT VectorSumOfCol(const vvUINT &vec, UINT col);

    UINT ChooseBestConfiguration(vvINT demands);

    bool ConfigurationAccepted(vUINT bitvector);

    void Solve();

    void ImproveSolutions();

    void FindBestStartPosition(void);

    UINT FindBiggestGap(void);

    vvUINT RewriteDataByBiggestGap(void);

    vector<Group> CreateGroups(vvUINT to_groups);

    vector<Group> CreateGroupsFromFreeAndBasisCities(
        vector<vvUINT> buffers_cities,
        vector<vvUINT> basis_cities);

    void PrepareConfigurationForFreeCities(
        vvUINT &buff1,
        vvUINT &buff2,
        const vUINT &buffers_bits,
        const vvUINT &buffer);

    void PrepareGroups(
        vvUINT &g1,
        vvUINT &g2,
        vvUINT &buffer);

    void PutDepotXYOnBegin(vvUINT &group);


    UINT groupSize_;
    UINT nOfGroups_;
    UINT fulfillment_;
    int requestedFulfillment;
    InputData allData_;
    vector<Group> groups_;

    vvUINT resultTable_;
};

