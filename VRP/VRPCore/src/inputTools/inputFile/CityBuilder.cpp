#include "CityBuilder.h"

CityBuilder::CityBuilder() :
    DepotBuilder::DepotBuilder(),
	demand(0),
	depotId(0),
	isFirst(false),
	isLast(false),
	isStop(false)
{
}

CityBuilder::CityBuilder(int id) :
    CityBuilder()
{
    setId(id);
}

CityBuilder& CityBuilder::setDemand(int demand)
{
    this->demand = demand;
    return *this;
}

CityBuilder& CityBuilder::setDepotId(int depotId)
{
    this->depotId = depotId;
    return *this;
}

CityBuilder& CityBuilder::setFlags(bool isFirst, bool isLast, bool isStop)
{
    return setIsFirst(isFirst).setIsLast(isLast).setIsStop(isStop);
}

CityBuilder& CityBuilder::setIsFirst(bool isFirst)
{
    this->isFirst = isFirst;
    return *this;
}

CityBuilder& CityBuilder::setIsLast(bool isLast)
{
    this->isLast = isLast;
    return *this;
}

CityBuilder& CityBuilder::setIsStop(bool isStop)
{
    this->isStop = isStop;
    return *this;
}

void CityBuilder::verifyData() const
{
    if(id < 0) throw std::string("City's id must not be less than 0.");
    if(name.empty()) throw std::string("City's name must not be empty.");
    if(point.getX() < 0 || point.getY() < 0) throw std::string("City's x and y coordinates must not be less than 0.");
    if(demand < 0) throw std::string("City's demand must not be less than 0.");
    if(depotId < 0) throw std::string("City's depotId must not be less than 0.");
}

City CityBuilder::get() const
{
    verifyData();

    return City(id, name, point, demand, depotId, isFirst, isLast, isStop);
}



