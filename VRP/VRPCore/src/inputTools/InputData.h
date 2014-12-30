#pragma once
class InputData
{
public:
    InputData(void);
    InputData(vvUINT &input);
    InputData(const InputData &r);
    ~InputData(void);

    void CreateDistances();
    void FillCitiesAndDemands(vvUINT &input);
    void ConvertvvINTtovvDOUBLE(vvINT &source, vvDOUBLE &dest);
    vvDOUBLE TransformToR_FI();
    void CorrectData(void);
    vUINT GetXYDrepresentation(UINT i);


    UINT size;
    int mag_xy[2];
    vvINT cities_xy;
    vUSHORT demands;
    USHORT demands_sum;
    vvUSHORT distances;
    UINT biggest_demander;
    vvUSHORT perm;
};

