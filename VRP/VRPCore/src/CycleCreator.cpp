#include "base.h"
#include "CycleCreator.h"
#include "testTools/VRPStats.h"

bool CompareCycles(const Cycle& c1, const Cycle& c2){ return(c1.getDistance()<c2.getDistance());}

CycleCreator::CycleCreator(InputData idata, vvUSHORT perms, UINT groupId)
{
    data_ = idata;
    perms_ = perms;
    groupId_ = groupId;
}

CycleCreator::CycleCreator(void)
{
}


CycleCreator::~CycleCreator(void)
{
}

USHORT CycleCreator::SumCargo( bitset< MAX_BITS > &bin )
{
    USHORT cargo = 0;
    for (UINT j = 0; j < data_.size; j++)
    {
        cargo += bin[j] * data_.demands[j];
    }
    return cargo;
}

USHORT CycleCreator::PermSize(USHORT N)
{
    USHORT N_perm = 1;
    for(int i = 2; i <= N; ++i)
    {
        N_perm *= i;
    }
    N_perm /=2;
    return N_perm;
}

vUSHORT CycleCreator::SetDestinations( bitset< MAX_BITS > &bin, USHORT cycle_size )
{
    vUSHORT destinations ( cycle_size );
    USHORT l=0;
    for(USHORT k = 0; k<data_.size; ++k)
    {
        if(bin[k])
        {
            destinations[l++] = k+1; //0 i reserved for magazine
        }
    }
    return destinations;
}

UINT CycleCreator::FindShortestPerm(vUSHORT &cycle)
{
    USHORT shortest = 49999;
    USHORT the_shortest = 0;
    USHORT distance = 0;
    USHORT cycle_size = cycle.size();
    int depot = 0;

    if (cycle_size == 1)
    {
        shortest = 2*data_.distances[depot][cycle[0]];
    }
    else
    {
        USHORT perm_size = PermSize(cycle_size);
        for(USHORT i = 0; i < perm_size; ++i)
        {
            distance = data_.distances[depot][cycle[perms_[i][0]]];
            
            for(USHORT j = 0; j < cycle_size-1; ++j)
            {
                distance+=data_.distances[cycle[perms_[i][j]]][cycle[perms_[i][j+1]]];
            }
            distance+=data_.distances[cycle[ perms_[i][cycle_size-1] ]][depot];

            if(distance <= shortest)
            {
                shortest = distance;
                the_shortest = i;
            }
        }
        vUSHORT temp(cycle_size);
        for (USHORT i = 0; i<cycle_size; ++i)
        {
            temp[i] = cycle[perms_[the_shortest][i]];
        }
        cycle = temp;
    }
    return shortest;
}

UINT CycleCreator::CountN()
{
    return (UINT)(pow(2.0, data_.size) - 1 - pow(2.0, data_.size - MAX_CYCLE_SIZE));
}

UINT CycleCreator::CountPossibleCycles(UINT &cycle_basic_count, UINT &cycle_rest_count)
{
    UINT N = CountN();

    bitset < MAX_BITS > bin(0);
    UINT allCycles___count = 0;

    USHORT cycle_size, cargo;
    for( UINT i=1; i < N; ++i)
    {
        bin = i;
        cycle_size = bin.count();
        if( cycle_size <= MAX_CYCLE_SIZE)
        {
            cargo = SumCargo(bin);
            if ( cargo > 6.5*MAX_DEMAND/10 && cargo < MAX_DEMAND ) //should be also MIN_CAPACITY after analysis the data 
            {
                if(i&data_.biggest_demander)
                {
                    cycle_basic_count++;
                    allCycles___count++;
                }
                else
                {
                    cycle_rest_count++;
                    allCycles___count++;
                }
            }
        }
    }
    return allCycles___count;
}

UINT CycleCreator::FindPossibleCycles(UINT cycle_all_count, UINT cycle_basic_count, UINT cycle_rest_count)
{
    UINT N = CountN();

    bitset < MAX_BITS > bin(0);

    vector<Cycle> cycle_basic(cycle_basic_count);
    vector<Cycle> cycle_rest(cycle_rest_count);
    vector<Cycle> cycle_all(cycle_all_count);

    USHORT b=0,r=0,vec_position=0;

    USHORT cycle_size, cargo;
    for( UINT i=1; i < N; ++i)
    {
        bin = i;
        cycle_size = bin.count();
        if( cycle_size <= MAX_CYCLE_SIZE)
        {
            cargo = SumCargo(bin);
            if ( cargo > 6.5*MAX_DEMAND/10 && cargo < MAX_DEMAND ) //should be also MIN_CAPACITY after analysis the data 
            {
                vUSHORT destinations = SetDestinations( bin, cycle_size );
                USHORT distance = FindShortestPerm(destinations);
                //should be if(shortest<max_length)
                if(i&data_.biggest_demander)
                {
                    cycle_basic[b++] = Cycle( i, distance, cargo, destinations, vec_position );
                    cycle_all[vec_position] = Cycle( i, distance, cargo, destinations, vec_position );
                    vec_position++;
                }
                else
                {
                    cycle_rest[r++] = Cycle( i, distance, cargo, destinations, vec_position );
                    cycle_all[vec_position] = Cycle( i, distance, cargo, destinations, vec_position );
                    vec_position++;
                }
            }
        }
    }
    
    sort (cycle_basic.begin(), cycle_basic.end(), CompareCycles);
    sort (cycle_rest.begin(), cycle_rest.end(), CompareCycles);
    sort (cycle_all.begin(), cycle_all.end(), CompareCycles);
    
    restCycles_ = cycle_rest;
    basicCycles_ = cycle_basic;
    allCycles_ = cycle_all;

    UINT created_cycles = cycle_basic.size()+cycle_rest.size();
    cout<<"\n"<<created_cycles<<" cycles created.\n";
    cout<<cycle_basic.size()<<" cycles basic created.\n";
    cout<<cycle_rest.size()<<" cycles rest created.\n";
    
    RunStatsManager::getManager().addStat(groupId_, ALL_CYCLES, created_cycles);
    RunStatsManager::getManager().addStat(groupId_, BASIC_CYCLES, cycle_basic.size());
    RunStatsManager::getManager().addStat(groupId_, REST_CYCLES, cycle_rest.size());
    
    return created_cycles;
}

UINT CycleCreator::CreateCycles()
{
    UINT basic_count = 0, rest_count = 0;
    UINT all_count = CountPossibleCycles(basic_count, rest_count);

    FindPossibleCycles(all_count, basic_count, rest_count);
    return 2;
}

vector< vector< Cycle > > CycleCreator::getAllCycles()
{
    vector< vector< Cycle > > all(0);
    all.push_back(allCycles_);
    all.push_back(basicCycles_);
    all.push_back(restCycles_);

    return all;
}

Cycle CycleCreator::getCycleByVecPosition(UINT vecPosition)
{
    for(UINT i =0; i<allCycles_.size(); i++)
    {
        if(allCycles_[i].getVecPos() == vecPosition)
        {
            return allCycles_[i];
        }
    }
    return allCycles_[0];
}


