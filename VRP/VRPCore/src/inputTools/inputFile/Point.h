#ifndef POINT_HEADER
	#define POINT_HEADER

class Point
{

protected:
	int x;
	int y;

public:
	Point(int, int);
	Point(const Point&);
	
	int getX() const;
	void setX(int);
	int getY() const;
	void setY(int);
	
};

#endif

