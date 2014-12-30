#include "Depot.h"


Depot::Depot(int id, const std::string& name, const Point& point) :
	id(id),
	name(name),
	point(point)
{
}

Depot::Depot(const Depot& d) :
    id(d.id),
	name(d.name),
	point(d.point)
{
}


int Depot::getId() const
{
	return id;
}

void Depot::setId(int id)
{
	this->id = id;
}

const std::string& Depot::getName() const
{
	return name;
}

void Depot::setName(const std::string& name)
{
	this->name = name;
}

const Point& Depot::getPoint() const
{
	return point;
}

void Depot::setPoint(const Point& point)
{
	this->point = point;
}

bool Depot::isDepot() const
{
	return true;
}

