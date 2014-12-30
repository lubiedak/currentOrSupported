#include "base.h"
#include "CycleConnector.h"
#include "testTools/VRPStats.h"

CycleConnector::CycleConnector(void)
{
}


CycleConnector::CycleConnector(CycleCreator creator, USHORT group_size, UINT groupId):
groupId_(groupId)
{
    creator_ = creator;
    bestDistances_ = vector<USHORT>((UINT)pow(2.0,group_size));

    for(vUSHORT::iterator distance = bestDistances_.begin();
        distance != bestDistances_.end();
        distance++)
        *distance =(USHORT) 65535;

    it_ = 0;
    connectedAll_ = false;
    connectedState_ = (UINT)pow(2,group_size) - 1;
    possibleLastIt_ = creator_.data_.demands_sum / MAX_DEMAND -1;
    DIST = 0;
}


CycleConnector::~CycleConnector(void)
{
}

vvUINT CycleConnector::Connect()
{
    vvUSHORT join_1, join_2;
    vUINT id_1, id_2;
    PrepareData(id_2, join_2);


    bool join_succed = true;
    while( join_succed && !connectedAll_)
    {
        if( it_ % 2 == 0 )
        {
            join_succed = TryToJoin(id_1, id_2, join_1, join_2);
        }
        else
        {
            join_succed = TryToJoin(id_2, id_1, join_2, join_1);
        }
        it_++;
    }
    return connectedCycles_;
}

void CycleConnector::PrepareData(vUINT &joiner_id, vvUSHORT &joiner )
{
    joiner.resize(creator_.basicCycles_.size());
    joiner_id.resize(creator_.basicCycles_.size());
    for (USHORT i = 0; i < joiner.size(); i++)
    {
        joiner[i] = creator_.basicCycles_[i].getAsVector();
        joiner_id[i] = creator_.basicCycles_[i].getID();
    }

    baseCycles_.resize(creator_.restCycles_.size());
    baseId_.resize(creator_.restCycles_.size());
    for (USHORT i =0; i< baseCycles_.size(); i++)
    {
        baseCycles_[i] = creator_.restCycles_[i].getAsVector();
        baseId_[i] = creator_.restCycles_[i].getID();
    }
}


bool CycleConnector::TryToJoin(vUINT &idTrunk,
                               vUINT &idBranch,
                               vvUSHORT &trunk,
                               vvUSHORT &branch)
{
    UINT possibleJoins = CountPossibleJoins(idBranch, branch);

    bool joinTryPassed = Join(idBranch, branch, idTrunk, trunk, possibleJoins);

    if (it_ >= possibleLastIt_)
    {
        UINT connected_cycles_size = IsThisLastTry(idTrunk);
        if(connectedAll_)
        {
            FullWithConnected(idTrunk, trunk, connected_cycles_size);
            sort(connectedCycles_.begin(), connectedCycles_.end(),
                [](const vUINT & a, const vUINT & b) -> bool
            {
                return a[0] < b[0];
            });
            PrintConnected(3);
        }
    }

    return joinTryPassed;
}

bool CycleConnector::Join(vUINT &idBranch,
                          vvUSHORT & branch,
                          vUINT &idTrunk,
                          vvUSHORT & trunk,
                          UINT possibleJoins)
{
    idTrunk.clear();
    trunk.clear();
    idTrunk.resize(possibleJoins);
    trunk.resize(possibleJoins, vUSHORT(it_+3));

    UINT joinCount = 0;

    for(UINT branch_i = 0; branch_i < idBranch.size(); ++branch_i)
    {
            for (UINT base_i = 0; base_i < baseCycles_.size(); ++base_i)
            {
                UINT id_and = idBranch[branch_i] & baseId_[base_i];
                if( id_and == 0)
                {
                    UINT id_or = idBranch[branch_i] + baseId_[base_i];
                    if( branch[branch_i][DIST] + baseCycles_[base_i][DIST] < bestDistances_[id_or])
                    {
                        bestDistances_[id_or] = branch[branch_i][DIST] + baseCycles_[base_i][DIST];
                        idTrunk[joinCount] = id_or;
                        trunk[joinCount][DIST] = bestDistances_[id_or];
                        for(USHORT k = 1; k < it_+2; k++)
                        {
                                trunk[joinCount][k] = branch[branch_i][k];
                        }
                        trunk[joinCount][it_+2] = baseCycles_[base_i][1];
                        joinCount++;
                    }
                }
            }
    }
    
    RunStatsManager::getManager().addStat(groupId_, RunStats::names_[CONNECTING_STEPS] + NumberToString(it_), joinCount);
    cout<<it_<<": "<<joinCount<<endl;
    return (joinCount > 0);
}

UINT CycleConnector::CountPossibleJoins(vUINT &to_join_id, vvUSHORT &to_join)
{
    UINT join_count = 0;
    vUSHORT distance_storage = bestDistances_;
    for(UINT i = 0; i < to_join.size(); ++i)
    {
        for (UINT j = 0; j < baseCycles_.size(); ++j)
        {
            UINT id_and = to_join_id[i] & baseId_[j];
            if( id_and == 0)
            {
                UINT id_or = to_join_id[i] + baseId_[j];
                if( to_join[i][DIST] + baseCycles_[j][DIST] < distance_storage[id_or])
                {
                    distance_storage[id_or] = to_join[i][DIST] + baseCycles_[j][DIST];
                    join_count++;
                }
            }
        }
    }
    return join_count;
}

UINT CycleConnector::IsThisLastTry(vUINT &trunk_id)
{
    UINT fully_conn_count = 0;
    for(UINT i = 0; i < trunk_id.size(); ++i)
    {
        if( trunk_id[i] == connectedState_ )
        {
            fully_conn_count++;
        }
    }
    connectedAll_ = fully_conn_count > 0 ? true:false;
    
    if(connectedAll_)
    {
        cout<<"Fully connected: "<<fully_conn_count<<endl;
        RunStatsManager::getManager().addStat(groupId_, FULLY_CONNECTED, fully_conn_count);
    }
    
    return fully_conn_count;
}

void CycleConnector::FullWithConnected(vUINT &id_trunk,
                                       vvUSHORT &trunk,
                                       UINT connected_cycles_size)
{
    connectedCycles_.resize(connected_cycles_size, vUINT(trunk[0].size()));
    UINT fully_conn_count = 0;
    for(UINT i = 0; i < id_trunk.size(); ++i)
    {
        if( id_trunk[i] == connectedState_ )
        {
            for(UINT j = 0; j<trunk[i].size(); j++)
            {
                connectedCycles_[fully_conn_count][j] = trunk[i][j];
            }
            fully_conn_count++;
        }
    }
}

void CycleConnector::PrintConnected(UINT how_many)
{
    UINT ccsize = connectedCycles_.size();
    how_many = min(how_many, ccsize);
    /*
    for (UINT i = 0; i<how_many; i++)
    {
        cout<<"Distance: "<<connected_cycles_[i][0]<<endl;
        for(UINT j = 1; j< connected_cycles_[i].size(); j++)
        {
            cout<<connected_cycles_[i][j]<<":\t";
            creator_.getCycleByVecPosition(connected_cycles_[i][j]).Print();
        }
        cout<<endl;
    }*/
}

void CycleConnector::AddToResultTable(vvUINT &table)
{

    static UINT id = 100000;
    for(UINT j = 1; j< connectedCycles_[0].size(); j++)
    {
        Cycle c = creator_.getCycleByVecPosition(connectedCycles_[0][j]);
        vUSHORT p = c.getPoints();
        vUINT cycle_line(0);
        cycle_line.push_back(id);
        cycle_line.push_back(c.getDistance());
        cycle_line.push_back(c.getDemand());
        id++;
        table.push_back(cycle_line);
        for(UINT i = 0; i< p.size(); i++)
        {
            vUINT xyd(0);

            xyd.push_back(creator_.data_.cities_xy[p[i]-1][0]);
            xyd.push_back(creator_.data_.cities_xy[p[i]-1][1]);
            xyd.push_back(creator_.data_.demands[p[i]-1]);

            table.push_back(xyd);
        }

    }
}
