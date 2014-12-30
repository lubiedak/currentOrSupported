import HTML
import csv
import collections

dir_ = ""

def CreateComparedTable(path, pathCompare, reportName, onlyTable = True):  
    table = readCSVTable(path)
    compareTable = readCSVTable(pathCompare)

    diff = computeDifferences(table, compareTable)
    table = convertListToDict(table)
    table = addDiff(table, diff)
    
    htmlPage = ""
    if not onlyTable:
        htmlPage += beginOfHTMLPage(reportName, reportName)
    
    htmlPage += addHTMLTable(table)
    
    if not onlyTable:
        htmlPage += endOfHTMLPage()
    
    return htmlPage
    
def CreateSimpleTable(path):
    table = readCSVTable(path)
    htmlPage = addHTMLTable(table)
    return htmlPage

def addDiff(table, diff):

    for key in table.keys():
        temp = []
        for val in range(len(table[key])):
            try:
                if (int(diff[key][val]) != 0):
                    temp.append(str(table[key][val]) + "<br/><b>" + str(diff[key][val]) + "%</b>")
                else:
                    temp.append(str(table[key][val]) )
            except TypeError:
                temp.append("")
            except ValueError:
                temp.append("")
        table[key] = temp

    return convertDictToList(table)
    
    
def readCSVTable(path, transpose = True):
    table = []
    with open(path, 'rb') as f:
        reader = csv.reader(f)
        for row in reader:
            table.append(row)
    if(transpose):
        table = [list(row) for row in zip(*table)]
    else:
        table = [list(row) for row in table]
    
    return table

def computeDifferences(tableA, tableB):
    tableA = convertTableToNumbers(tableA)
    tableB = convertTableToNumbers(tableB)
    
    (dictA,dictB) = matchTables(tableA, tableB)

    diff ={}
    for key in dictA.keys():
        temp = []
        for val in range(len(dictA[key])):
            try:
                if (int(dictB[key][val]) != 0):
                    temp.append( round(100*float(int(dictA[key][val]) - int(dictB[key][val]))/float(dictB[key][val]), 2))
                else:
                    temp.append("XX")
            except TypeError:
                temp.append(0);
            except ValueError:
                temp.append(0);
                
            diff[key] = temp
    
    return diff
    
def matchTables(tableA, tableB): 
    dictA = convertListToDict(tableA)
    dictB = convertListToDict(tableB)
    
    if(len(tableA) > len(tableB) or len(tableA[0]) > len(tableB[0])):
        dictB = matchDicts(dictA, dictB)
        dictA = matchDicts(dictB, dictA)
    else:
        dictA = matchDicts(dictB, dictA)
        dictB = matchDicts(dictA, dictB)
    
    return dictA, dictB
    
def printDict(dict):
    for k, v in dict.items():
        print k, v

def matchDicts(dictA, dictB):
    for key in dictA.keys():
        if key in dictB.keys():
            if len(dictA[key]) > len(dictB[key]):
                dictB[key].extend([0]*(len(dictA[key]) - len(dictB[key])))
        else:
            dictB[key] = [0]*len(dictA[key])
    return dictB


def convertListToDict(table):# we assume that first cell is name
    dict = {}
    for row in table:
        dict[row[0]] = row[1:]
    return dict
    
def convertDictToList(dict):# we assume that first cell is name
    table = []
    orderDict = collections.OrderedDict(sorted(dict.items()))
    for k, v in orderDict.iteritems():
        temp = [] #should be better coded
        temp.append(k)
        temp.extend(v)
        table.append(temp)
    return table

def convertTableToNumbers(table):
    for row in table :
        for cell in row:
            try:
                cell = float(cell)
            except ValueError:
                pass
    return table

def convertTableToStrings(table):
    for row in table:
        for cell in row:
            try:
                cell = str(cell)
            except ValueError:
                pass
    return table

def addHTMLTable(table):
    htmlTable = HTML.table(table[1:], header_row = table[0])
    htmlTable = updateTableTag(htmlTable)

    return htmlTable
    
def saveToFile(filename, page):
    with open(filename, 'w') as f:
        f.write(page)
        f.close()
        
def updateTableTag(htmlTable):
    table = htmlTable.split("\n")
    table[0] = "<TABLE class=\"table table-condensed table-bordered table-hover\">"
    htmlTable = "\n".join(table)
    return htmlTable
    
def beginOfHTMLPage(header, title):
    pageBegin = "<!DOCTYPE html><html><head><meta charset=\"utf-8\"/><link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css\"><title>"
    pageBegin += title
    pageBegin += "</title></head><body><div class=\"container\" style=\"width:900px;\"><h1>"
    pageBegin += header
    pageBegin += "</h1>"
    return pageBegin
    
def endOfHTMLPage():
    pageEnd = "</div></body></html>"
    return pageEnd


