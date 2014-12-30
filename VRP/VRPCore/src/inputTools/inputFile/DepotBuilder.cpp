
#include "CityBuilder.h"

DepotBuilder::DepotBuilder() :
    id(0),
	name(""),
	point(0, 0)
{
}

DepotBuilder::DepotBuilder(int id) :
    DepotBuilder()
{
    setId(id);
}

DepotBuilder& DepotBuilder::setId(int id)
{
    this->id = id;
    return *this;
}

DepotBuilder& DepotBuilder::setName(const std::string& name)
{
    this->name = name;
    return *this;
}

DepotBuilder& DepotBuilder::setPoint(const Point& point)
{
    this->point = point;
    return *this;
}

DepotBuilder& DepotBuilder::setPoint(int x, int y)
{
    this->point = Point(x, y);
    return *this;
}

void DepotBuilder::verifyData() const
{
    if(id < 0) throw std::string("Depot's id must not be less than 0.");
    if(name.empty()) throw std::string("Depot's name must not be empty.");
    if(point.getX() < 0 || point.getY() < 0) throw std::string("Depot's x and y coordinates must not be less than 0.");
}

Depot DepotBuilder::get() const
{
    verifyData();

    return Depot(id, name, point);
}



