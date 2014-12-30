#include "VRPStats.h"

const string AlgSteps::names[] =
{
    "Grouping",
    "CreatingCycles",
    "ConnectingCycles"
};
    
const string RunStats::names_[] = 
{
    "0GroupSize",
    "1DemandsSum",
    "CarsExpected",
    "CarsUsed",
    
    "CyclesAll",
    "CyclesBasic",
    "CyclesRest",
    
    "ConnStep",
    "ConnectedFully",
    "Distance"
};

template<typename T>
string Stats<T>::toString(char delimiter, bool printNames, bool printValues) const
{
    stringstream stream;
    typename map<string, T>::const_iterator it = stats_.begin();
    
    stream << (printNames ? it->first : "");
    stream << (printValues ? NumberToString(it->second) : "");
    ++it;
    
    for( ; it != stats_.end(); ++it)
    {
        if(printNames)
        {
            stream << delimiter << it->first;
        }
        if(printValues)
        {
            stream<< delimiter <<it->second;
        }
    }
    return stream.str();
}

template<typename T>
StatsMap<T> StatsTools<T>::createTable(StatsMap<T> source) const
{
    StatsMap<T> table = source;
    Stats< T > biggest_stat = getBiggestStats(table);
    table = expandTable(table, biggest_stat);

    return table;
}

template<typename T>
Stats< T > StatsTools<T>::getBiggestStats( StatsMap<T> &table) const 
{
    Stats< UINT > biggest = table.begin()->second;
    for(typename StatsMap<T>::const_iterator it = table.begin(); it != table.end(); ++it)
    {
        if(it->second.getSize() > biggest.getSize())
        {
            biggest = it->second;
        }
    }
    return biggest;
}

/*
 * This function is responsible to fill differences beetween rows in table
 */
template<typename T>
StatsMap<T> StatsTools<T>::expandTable(StatsMap<T> &source, Stats< T > &biggest_stat) const
{
    StatsMap<T> table = source;
    map < string, T > biggest_map = biggest_stat.getMap();
    
    for(typename StatsMap<T>::const_iterator it = source.begin(); it != source.end(); ++it)
    {
        for(typename map < string, T >::iterator b_it = biggest_map.begin(); b_it != biggest_map.end(); ++b_it)
        {
            if(! it->second.hasStat(b_it->first))
            {
                table[it->first].addStat(b_it->first, 0);
            }
        }
    }
    return table;
}

string RunStatsManager::toStringTable(char delimiter) const
{
    map< USHORT, Stats< UINT > > table = createTable();

    stringstream stream;
    //Header
    stream<< "0Group Number"<<delimiter;
    stream<< table.begin()->second.toString(delimiter, true, false);
    stream<<endl;

    //Stats
    for( map< USHORT, Stats < UINT > >::const_iterator it = table.begin(); it != table.end(); ++it)
    {
        stream<<"Group "<<it->first<<delimiter;
        stream<< it->second.toString(delimiter, false, true);
        stream<<endl;
    }
    return stream.str();
}

void RunStatsManager::addStat(const USHORT groupId, RUN_STATS stat, UINT value)
{
    allStats_[groupId].addStat(RunStats::names_[stat], value);
    allStats_[groupId].setName("G" + NumberToString(groupId));
}

void RunStatsManager::addStat(const USHORT groupId, string stat, UINT value)
{
    allStats_[groupId].addStat(stat, value);
    allStats_[groupId].setName("G" + NumberToString(groupId));
}

void RunStatsManager::exportToFile(string workspace) const
{
    ofstream outputFile;
    outputFile.open (workspace + fileName_);
    outputFile<<toStringTable();
    outputFile.close();
}

map< USHORT, Stats< UINT > > RunStatsManager::createTable() const
{
    map< USHORT, Stats< UINT > > table = allStats_;
    Stats< UINT > biggest_stat = getBiggestStats(table);
    expandTable(table, biggest_stat);

    return table;
}
    
Stats< UINT > RunStatsManager::getBiggestStats( map< USHORT, Stats< UINT > > &table) const 
{
    Stats< UINT > biggest = table.begin()->second;
    for( map< USHORT, Stats < UINT > >::const_iterator it = table.begin(); it != table.end(); ++it)
    {
        if(it->second.getSize() > biggest.getSize())
        {
            biggest = it->second;
        }
    }
    return biggest;
}

/*
 * This function is responsible to fill differences beetween rows in table
 */
void RunStatsManager::expandTable(map< USHORT, Stats< UINT > > &table, Stats< UINT > &biggest_stat) const
{
    map < string, UINT > biggest_map = biggest_stat.getMap();
    for( map< USHORT, Stats < UINT > >::const_iterator it = allStats_.begin(); it != allStats_.end(); ++it)
    {
        for(map < string, UINT >::iterator b_it = biggest_map.begin(); b_it != biggest_map.end(); ++b_it)
        {
            if(! it->second.hasStat(b_it->first))
            {
                table[it->first].addStat(b_it->first, 0);
            }
        }
    }
}
