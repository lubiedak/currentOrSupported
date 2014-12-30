#pragma once
class Cycle
{
public:
    Cycle(void);
    Cycle(UINT id, USHORT distance, USHORT demand, const vUSHORT& points, USHORT vectorPosition);
    ~Cycle(void);

    void Print();

    USHORT getDistance() const{ return distance_;};
    USHORT getDemand() const{ return demand_;};
    UINT getID() const{ return id_;};
    vUSHORT getPoints() const { return points_; };
    vUSHORT getAsVector();
    USHORT getVecPos() const {return vectorPosition_;};

private:
    UINT id_;
    USHORT distance_;
    USHORT demand_;
    USHORT capacity_;
    vUSHORT points_;
    USHORT vectorPosition_;
    vUSHORT vec_;
};


