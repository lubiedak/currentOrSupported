#include "../base.h"
#include "InputData.h"
#include <math.h>
#include "../PermutationGenerator.h"


InputData::InputData(void)
{
}

InputData::InputData(vvUINT &input)
{
    FillCitiesAndDemands(input);
    CreateDistances();
    CorrectData();
}

InputData::InputData(const InputData &r)
{
    size = r.size;
    cities_xy = r.cities_xy;
    demands = r.demands;
    distances = r.distances;
    biggest_demander = r.biggest_demander;
    perm = r.perm;
    mag_xy[0] = r.mag_xy[0];
    mag_xy[1] = r.mag_xy[1];
    demands_sum = r.demands_sum;
}


InputData::~InputData(void)
{
}

void InputData::CorrectData(void)
{
    mag_xy[0] = cities_xy[0][0];
    mag_xy[1] = cities_xy[0][1];
    vvINT temp_xy = cities_xy;
    vUSHORT temp_demands = demands;
    cities_xy.clear();
    demands.clear();
    for(UINT i = 1; i< size; i++)
    {
        cities_xy.push_back(temp_xy[i]);
        demands.push_back(temp_demands[i]);
    }
    size = cities_xy.size();
}

void InputData::FillCitiesAndDemands(vvUINT &input)
{
    size = input.size();
    cities_xy = vvINT( size,vINT(2));
    demands = vUSHORT(size);
    demands_sum = 0;
    USHORT biggest_demand = 0;
    for(UINT i =0; i<size; i++)
    {
        cities_xy[i][0] = input[i][0];
        cities_xy[i][1] = input[i][1];
        demands[i] = (USHORT)input[i][2];
        demands_sum+=demands[i];
        if( demands[i] > biggest_demand )
        {
            biggest_demand = demands[i];
            biggest_demander = (int)pow(2,i-1);
        }
    }
}

void InputData::CreateDistances()
{
    distances = vvUSHORT(size, vUSHORT(size));

    for(UINT i =0; i<size; i++)
    {
        for(UINT j = 0; j<size; j++)
        {
            distances[i][j] = (USHORT)(5*sqrt(pow(cities_xy[i][0] - cities_xy[j][0], 2)
                                + pow(cities_xy[i][1] - cities_xy[j][1], 2)));
        }
    }
}

void InputData::ConvertvvINTtovvDOUBLE(vvINT &source, vvDOUBLE &dest)
{
    if(source.size() == dest.size())
    {
        for (UINT i = 0; i < dest.size(); i++)
        {
            for (UINT j = 0; j < dest[i].size(); j++)
            {
                dest[i][j]=(double)source[i][j];
            }
        }
    }
}

vUINT InputData::GetXYDrepresentation(UINT i)
{
    vUINT xyd = vUINT(3);
    xyd[0] = cities_xy[i][0];
    xyd[1] = cities_xy[i][1];
    xyd[2] = demands[i];
    return xyd;
}

vvDOUBLE InputData::TransformToR_FI()
{
    vvDOUBLE r_fi = vvDOUBLE(size, vDOUBLE(2));
    vvDOUBLE xy = vvDOUBLE(size, vDOUBLE(2));
    ConvertvvINTtovvDOUBLE(cities_xy, xy);

    double x,y, r, fi;
    double pi = acos(-1);
    for(UINT i = 0; i<size; i++)
    {
        x = xy[i][0] - (double)mag_xy[0];
        y = xy[i][1] - (double)mag_xy[1];
        r = sqrt(x*x + y*y);

        if(x>0 && y>=0)
            fi = atan(y/x);
        else if(x>0 && y<0)
            fi = atan(y/x) + 2*pi;
        else if(x<0)
            fi = atan(y/x) + pi;
        else if(x==0 && y>0)
            fi = pi/2;
        else if(x==0 && y<0)
            fi = 3*pi/2;

        r_fi[i][0] = r;
        r_fi[i][1] = fi;
    }
    return r_fi;

}

