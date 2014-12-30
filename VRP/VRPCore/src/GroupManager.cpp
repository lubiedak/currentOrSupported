
#include "testTools/VRPStats.h"

#include "base.h"
#include "GroupManager.h"
#include "inputTools/InputManager.h"
#include <math.h>
#include <bitset>

GroupManager::GroupManager()
{
}

GroupManager::GroupManager(UINT howManyGroups, InputData &inputData)
{
    allData_ = inputData;
    nOfGroups_ = howManyGroups;
    groupSize_ = allData_.size/nOfGroups_;
    requestedFulfillment = MAX_DEMAND/100 * 95;//95
    resultTable_ = vvUINT(0);
}


GroupManager::~GroupManager(void)
{
}

vvUINT GroupManager::ExportDivideResults(void)
{
    vvUINT results = vvUINT(0);
    for (UINT gr = 0; gr < nOfGroups_; gr++)
    {
        for(UINT i = 0; i<groups_[gr].data_.size; i++)
        {
            vUINT row = groups_[gr].data_.GetXYDrepresentation(i);
            row.push_back(gr);
            results.push_back(row);
        }
    }
    return results;
}

void GroupManager::DivideIntoGroups(void)
{
    FindBestStartPosition();
    vvUINT to_groups = RewriteDataByBiggestGap();
    groups_ = CreateGroups(to_groups);
}

vUINT GroupManager::ConvertToBitVector(UINT size, UINT number)
{
    bitset<16> basic(number);
    vUINT bitvector(size);
    for(UINT i = 0; i<size; i++)
    {
        bitvector[i] = basic[i];
    }
    return bitvector;
}

UINT GroupManager::VectorSum(vUINT &vec)
{
    UINT sum=0;
    for(vUINT::iterator it = vec.begin(); it!=vec.end(); it++)
    {
        sum+=*it;
    }
    return sum;
}

UINT GroupManager::VectorSumOfCol(const vvUINT &vec, UINT col)
{
    UINT sum=0;
    if(col<vec[0].size())
    {
        for(UINT i=0; i< vec.size(); i++)
        {
            sum+=vec[i][col];
        }
    }
    return sum;
}

vvUINT GroupManager::PossibleConfigurations(UINT size)
{
    vvUINT buffers = vvUINT(0,vUINT(size));
    UINT iterations = (UINT) pow (2, size);
    for(UINT i=1; i<iterations; i++ )
    {
        vUINT bitvector = ConvertToBitVector(size, i);

        if(ConfigurationAccepted(bitvector))
        {
            buffers.push_back(bitvector);
        }
    }
    return buffers;
}

bool GroupManager::ConfigurationAccepted(vUINT bitvector)
{
    return VectorSum(bitvector) < bitvector.size() * 3/4;
}

void GroupManager::SeparateFreeAndGroupedCities(UINT size,
                                                 const vvUINT &toGroups,
                                                 vector<vvUINT> &bases,
                                                 vector<vvUINT> &buffers)
{
    bases = vector<vvUINT>(nOfGroups_);
    buffers = vector<vvUINT>(nOfGroups_-1);
    vUINT is_buffer = vector<UINT>(toGroups.size());

    UINT coef = 1;
    for(UINT i=0; i<toGroups.size(); i++)
    {
        if(    i >= coef*toGroups.size()/nOfGroups_ - size
            && i <  coef*toGroups.size()/nOfGroups_ + size
            && coef!=nOfGroups_)
        {
            is_buffer[i] = 1;
        }
        else
        {
            is_buffer[i] = 0;
        }
        if(i == coef*toGroups.size()/nOfGroups_ + size)
        {
            coef++;
        }
    }

    UINT group_number = 0;
    for(UINT i=0; i<toGroups.size(); i++)
    {
        if(is_buffer[i])
        {
            buffers[group_number].push_back(toGroups[i]);
        }
        else
        {
            bases[group_number].push_back(toGroups[i]);
        }
        if(i == (group_number+1)*toGroups.size()/nOfGroups_ + size)
        {
            group_number++;
        }
    }
}


vector<Group> GroupManager::CreateGroups(vvUINT toGroups)
{
    UINT bufer_size = 3;//(int)ceil((group_size*0.15)-0.41);

    vector<vvUINT> grouped_cities, freeCities;
    SeparateFreeAndGroupedCities(bufer_size, toGroups, freeCities, grouped_cities);

    return CreateGroupsFromFreeAndBasisCities(grouped_cities, freeCities);
}

void GroupManager::PrepareConfigurationForFreeCities(vvUINT &buff1,
                                        vvUINT &buff2,
                                        const vUINT &buffers_bits,
                                        const vvUINT &buffer)
{
    buff1.clear();
    buff2.clear();
    for(UINT j = 0; j<buffers_bits.size(); j++)
    {
        if(buffers_bits[j])
        {
            buff1.push_back(buffer[j]);
        }
        else
        {
            buff2.push_back(buffer[j]);
        }
    }
}

UINT GroupManager::ChooseBestConfiguration(vvINT demands)
{
    UINT best_buffer = 0;
    double close_to_required_fulfillment = MAX_DEMAND;

    double a, b;
    for(UINT i = 0; i< demands.size(); i++)
    {
        a = abs(demands[i][0] - floor(demands[i][0]+0.0 /requestedFulfillment + 0.5) * requestedFulfillment);
        b = abs(demands[i][1] - floor(demands[i][1]+0.0 /requestedFulfillment + 0.5) * requestedFulfillment);
        if(a+b < close_to_required_fulfillment)
        {
            close_to_required_fulfillment = a+b;
            best_buffer = i;
        }
    }
    return best_buffer;
}

void GroupManager::PrepareGroups(vvUINT &group1, vvUINT &group2, vvUINT &freeCities)
{
    int demandColumn = 2;
    vvUINT possible_configurations = PossibleConfigurations(freeCities.size());
    UINT demand_g1 = VectorSumOfCol(group1, demandColumn);
    UINT demand_g2 = VectorSumOfCol(group2, demandColumn);

    vvINT demands_g1_g2_sums = vvINT(possible_configurations.size(), vINT(2));

    vvUINT temp_g1 = vvUINT(0);
    vvUINT temp_g2 = vvUINT(0);

    for(UINT i = 0; i<possible_configurations.size(); i++)
    {
        PrepareConfigurationForFreeCities(    temp_g1,
                                temp_g2,
                                possible_configurations[i],
                                freeCities);

        demands_g1_g2_sums[i][0] = VectorSumOfCol(temp_g1, demandColumn) + demand_g1;
        demands_g1_g2_sums[i][1] = VectorSumOfCol(temp_g2, demandColumn) + demand_g2;
    }

    UINT best_buffer = ChooseBestConfiguration(demands_g1_g2_sums);

    for(UINT i=0; i < freeCities.size(); i++)
    {
        if(possible_configurations[best_buffer][i])
        {
            group1.push_back(freeCities[i]);
        }
        else
        {
            group2.push_back(freeCities[i]);
        }
    }

}

void GroupManager::PutDepotXYOnBegin(vvUINT &group)
{
    vUINT v(0);
    v.push_back(allData_.mag_xy[0]);
    v.push_back(allData_.mag_xy[1]);
    v.push_back(0);
    group.insert(group.begin(), v);

}

vector<Group> GroupManager::CreateGroupsFromFreeAndBasisCities(vector<vvUINT> buffers_cities, vector<vvUINT> basis_cities)
{
    vector<Group> groups = vector<Group>(0);
    for(UINT i = 0; i<nOfGroups_-1; i++)
    {
        PrepareGroups(basis_cities[i], basis_cities[i+1], buffers_cities[i]);
    }

    for (UINT i = 0; i < nOfGroups_; i++)
    {
        PutDepotXYOnBegin(basis_cities[i]);
    }

    for (UINT i = 0; i < nOfGroups_; i++)
    {
        groups.push_back(Group(InputData(basis_cities[i]), i));
    }

    return groups;
}


void GroupManager::ReGroup(void)
{

}


void GroupManager::SolveAll(void)
{
    DivideIntoGroups();
    Solve();
    ReGroup();
    ImproveSolutions();
}


void GroupManager::Solve(void)
{
    for (vector<Group>::iterator group = groups_.begin();
        group != groups_.end(); group++)
    {
        cout<<"Group size: "<<group->data_.size<<" Demands sum: "<<group->data_.demands_sum<<endl;
        RunStatsManager::getManager().addStat(group->groupId_, GROUP_SIZE, group->data_.size);
        RunStatsManager::getManager().addStat(group->groupId_, DEMANDS_SUM, group->data_.demands_sum);
        group->Solve(resultTable_);
    }
}

void GroupManager::ImproveSolutions(void)
{
}

UINT GroupManager::FindBiggestGap(void)
{
    vvDOUBLE r_fi = allData_.TransformToR_FI();
    double fi, max_fi=0;
    UINT max_i = 0;

    for(UINT i=0; i< allData_.size-1; i++)
    {
        fi = r_fi[i+1][1] - r_fi[i][1];
        if(fi>max_fi)
        {
            max_fi = fi;
            max_i = i;
        }
    }
    if (2*acos(-1) - r_fi[allData_.size-1][1] > max_fi)
        return 0;

    return max_i;
}

vvUINT GroupManager::RewriteDataByBiggestGap(void)
{
    vvUINT new_data = vvUINT(allData_.size, vUINT(3));
    UINT gap_i = FindBiggestGap();
    if(gap_i != 0)
    {
        for(UINT i = gap_i; i<allData_.size; i++)
        {
            new_data[i] = allData_.GetXYDrepresentation(i);
        }
        for(UINT i = 0; i<gap_i; i++)
        {
            new_data[i] = allData_.GetXYDrepresentation(i);
        }
    }
    else
    {
        for(UINT i = 0; i<allData_.size; i++)
        {
            new_data[i] = allData_.GetXYDrepresentation(i);
        }
    }
    return new_data;
}


void GroupManager::FindBestStartPosition(void)
{

}
