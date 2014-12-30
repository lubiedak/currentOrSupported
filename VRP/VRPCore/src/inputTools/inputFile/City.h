#ifndef CITY_HEADER
	#define CITY_HEADER

#include <string>
#include "Depot.h"

class City : public Depot
{

	protected:
		int demand;
		int depotId;

		bool isFirst;
		bool isLast;
		bool isStop;

	public:
		City(int, const std::string&, const Point&, int, int, bool, bool, bool);
		City(const City&);

		int getDemand() const;
		void setDemand(int);

		int getDepotId() const;
		void setDepotId(int);

		bool getIsFirst() const;
		void setIsFirst(bool);

		bool getIsLast() const;
		void setIsLast(bool);

		bool getIsStop() const;
		void setIsStop(bool);

		virtual bool isDepot() const;
};

#endif

