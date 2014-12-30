#ifndef CITYBUILDER_HEADER
	#define CITYBUILDER_HEADER

#include <string>
#include "City.h"
#include "DepotBuilder.h"

class CityBuilder : public DepotBuilder
{
    protected:
		int demand;
		int depotId;

		bool isFirst;
		bool isLast;
		bool isStop;

    public:
        CityBuilder();
        CityBuilder(int);

        CityBuilder& setDemand(int);
        CityBuilder& setDepotId(int);
        CityBuilder& setFlags(bool, bool, bool);
        CityBuilder& setIsFirst(bool);
        CityBuilder& setIsLast(bool);
        CityBuilder& setIsStop(bool);

        virtual void verifyData() const;

        City get() const;
};

#endif

