#ifndef DEPOTBUILDER_HEADER
	#define DEPOTBUILDER_HEADER

#include <string>
#include "Depot.h"

class DepotBuilder
{
    protected:
        int id;
		std::string name;
		Point point;

    public:
        DepotBuilder();
        DepotBuilder(int);

        DepotBuilder& setId(int);
        DepotBuilder& setName(const std::string&);
        DepotBuilder& setPoint(const Point&);
        DepotBuilder& setPoint(int, int);

        virtual void verifyData() const;

        Depot get() const;
};

#endif


