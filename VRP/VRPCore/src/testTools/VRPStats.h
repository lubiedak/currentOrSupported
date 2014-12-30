#ifndef VRPSTATS_H
#define VRPSTATS_H
#include "../base.h"

const string statsFile = "stats.csv";

enum ALG_STEPS
{
    GROUPING = 0,
    CREATING_CYCLES,
    CONNECTING_CYCLES,
    LAST_STEP
};

class AlgSteps
{ 
    const static string names[];
};

enum RUN_STATS
{
    GROUP_SIZE = 0,
    DEMANDS_SUM,
    CARS_EXPECTED,
    CARS_USED,

    ALL_CYCLES,
    BASIC_CYCLES,
    REST_CYCLES,

    CONNECTING_STEPS,
    FULLY_CONNECTED,
    DISTANCE,
    LAST_STAT
};

class RunStats
{
public:
    const static string names_[];
};

/*
 * Generic container for many type of statistics
 * collected during algorithm execution.
 */
template<typename T>
class Stats
{
public:
    Stats(void){stats_ = map <string, T>();}

    ~Stats(void){};
    
    string toString(char delimiter = ',', bool printNames = true, bool printValues = true) const;
    
    void addStat( const string& statName, T value) { stats_[statName] = value; }
    void setName( const string& name) { name_ = name; }
    string& getName( ){return name_;}
    UINT getSize() const {return stats_.size();}
    bool hasStat(const string& statName) const { return stats_.count(statName) > 0; } 
    
    map < string, T >& getMap(){return stats_;}
    
private:
    map < string, T > stats_;
    string name_;
};

template<typename T>
using StatsMap = map< USHORT, Stats< T > >;


template<typename T>
class StatsTools
{
public:
    StatsTools(){}
    virtual ~StatsTools(){}
    
    void exportToFile(string workspace, StatsMap<T> statsMap) const;
    string toStringTable(StatsMap<T> statsMap, char delimiter = ',') const;
    
private:
    StatsMap<T> createTable(StatsMap<T> source) const;
    Stats< T > getBiggestStats( StatsMap<T> &table) const;
    StatsMap<T> expandTable( StatsMap<T> &table, Stats< T > &biggest_stat) const;
};
/*
 * Singleton container for managing Run stats
 */
class RunStatsManager
{
public:
    
    static RunStatsManager& getManager()
    {
        static RunStatsManager manager;
        return manager;
    }
    
    string toStringTable(char delimiter = ',') const;
    void addStat(const USHORT groupId, RUN_STATS stat, UINT value);
    void addStat(const USHORT groupId, string stat, UINT value);
    void exportToFile(string workspace) const;
    
private:
    RunStatsManager(void)
    {
        allStats_ = map<USHORT, Stats< UINT > >();
        fileName_ = "/runStats.csv";
    }
    ~RunStatsManager(void){};
    RunStatsManager(const RunStatsManager&){};
    
    map< USHORT, Stats< UINT > > createTable() const;
    Stats< UINT > getBiggestStats( map< USHORT, Stats< UINT > > &table) const;
    void expandTable(map< USHORT, Stats< UINT > > &table, Stats< UINT > &biggest_stat) const;


    map< USHORT, Stats< UINT > > allStats_;
    string fileName_;
};

/*
 * Singleton container for managing Time stats
 */
class TimeStatsManager
{
public:
    
    static TimeStatsManager& getManager()
    {
        static TimeStatsManager manager;
        return manager;
    }

    void addStat(const USHORT groupId, ALG_STEPS step, UINT value);
    void addStat(const USHORT groupId, string stat, UINT value);
    
private:
    TimeStatsManager(void)
    {
        allStats_ = map<USHORT, Stats< UINT > >();
        fileName_ = "/timeStats.csv";
    }
    ~TimeStatsManager(void){};
    TimeStatsManager(const TimeStatsManager&){};

    map< USHORT, Stats< UINT > > allStats_;
    string fileName_;
};

#endif
