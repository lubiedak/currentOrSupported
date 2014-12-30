#include "City.h"


City::City(int id, const std::string& name, const Point& point, int demand = 0, int depotId = 0, bool isFirst = false, bool isLast = false, bool isStop = false) :
	Depot::Depot(id, name, point),
	demand(demand),
	depotId(depotId),
	isFirst(isFirst),
	isLast(isLast),
	isStop(isStop)
{
}

City::City(const City& c) :
    Depot::Depot(c.id, c.name, c.point),
    demand(c.demand),
    depotId(c.depotId),
    isFirst(c.isFirst),
	isLast(c.isLast),
	isStop(c.isStop)
{
}

int City::getDemand() const
{
	return demand;
}

void City::setDemand(int demand)
{
	this->demand = demand;
}

int City::getDepotId() const
{
	return depotId;
}

void City::setDepotId(int depotId)
{
	this->depotId = depotId;
}

bool City::getIsFirst() const
{
	return isFirst;
}

void City::setIsFirst(bool isFirst)
{
	this->isFirst = isFirst;
}

bool City::getIsLast() const
{
	return isLast;
}

void City::setIsLast(bool isLast)
{
	this->isLast = isLast;
}

bool City::getIsStop() const
{
	return isStop;
}

void City::setIsStop(bool isStop)
{
	this->isStop = isStop;
}

bool City::isDepot() const
{
    return false;
}

