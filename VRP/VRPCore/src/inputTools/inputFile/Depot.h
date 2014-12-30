#ifndef DEPOT_HEADER
	#define DEPOT_HEADER

#include <string>
#include "Point.h"

class Depot
{
	protected:
		int id;
		std::string name;
		Point point;

	public:
		Depot(int, const std::string&, const Point&);
		Depot(const Depot&);

		int getId() const;
		void setId(int);

		const std::string& getName() const;
		void setName(const std::string&);

		const Point& getPoint() const;
		void setPoint(const Point&);

		virtual bool isDepot() const;
};

#endif


