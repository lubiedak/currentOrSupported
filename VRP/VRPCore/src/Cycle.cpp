#include "base.h"
#include "Cycle.h"


Cycle::Cycle(void)
{
}


Cycle::Cycle(UINT id, USHORT distance, USHORT demand, const vUSHORT & points, USHORT vectorPosition)
{
    id_ = id;
    distance_ = distance;
    demand_ = demand;
    points_ = points;
    vectorPosition_ = vectorPosition;
    vec_ = vUSHORT(2);
    vec_[0] = distance_;
    vec_[1] = vectorPosition_;
}

Cycle::~Cycle(void)
{
}

vUSHORT Cycle::getAsVector()
{
    return vec_;
}

void Cycle::Print()
{
    bitset<MAX_BITS> bin(id_);
    cout<<"\t"<<distance_<<"\t"<<demand_<<"\t";
    for (UINT i = 0; i<points_.size(); i++)
    {
        cout<<points_[i]<<"\t";
    }
    cout<<endl;
}
