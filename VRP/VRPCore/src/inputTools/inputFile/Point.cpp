#include "Point.h"

Point::Point(int x = 0, int y = 0) :
	x(x), y(y)
{
}

Point::Point(const Point& p)
{
	this->x = p.getX();
	this->y = p.getY();
}

int Point::getX() const
{
	return x;
}

void Point::setX(int x)
{
	this->x = x;
}

int Point::getY() const
{
	return y;
}

void Point::setY(int y)
{
	this->y = y;
}


