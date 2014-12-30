import subprocess
import argparse
import os
import time
import math as m
import random as r
import shutil
import csv
import Image, ImageDraw

import PointsCycles as pc

p_size = 8
min_diff = p_size +4

def sorted_table(data, column=0, reverse=False):
    return sorted(data, cmp=lambda a,b: cmp(a[column], b[column]), reverse=reverse)

def CheckPoint(point, r_fi):

    for i in range(len(r_fi)):
        if( (m.fabs(r_fi[i][0]-point[0]) < min_diff) and (2*r_fi[i][0]*m.tan(3.14*m.fabs( (r_fi[i][1]-point[1]) / 2 )/180) < min_diff) ):
            return False
    return True
        
def GeneratePoints(min_r, size, n):
    r_fi = []
    xy = []
    IsUnique = False
    for i in range(0,n):
        proposition = [min_r + r.randint(0, size/2 - min_r), r.randint(0, 360)]
        while IsUnique == False:
            proposition = [min_r + r.randint(0, size/2 - min_r), r.randint(0, 360)]
            IsUnique = CheckPoint(proposition, r_fi)
          
        r_fi.append(proposition)
        IsUnique = False
    r_fi = sorted_table(r_fi, 1)
    
    for i in r_fi:
        point = [ (int)(size/2 + i[0] * m.cos(i[1] * m.pi / 180)),
        (int)(size/2 + i[0] * m.sin(i[1] * m.pi / 180))]
        xy.append( point )
    
    xy[0][0] = 400
    xy[0][1] = 400
    return xy

def GenerateDemands(xy, min, max):
    for i in xy:
        i.append(r.randint(min, max))
    xy[0][2] = 0
    return xy
    
def DrawPointsImage(xy, size, path, groups = False, number = 0):
    image = Image.new("RGB", (size, size), "white")
    draw = ImageDraw.Draw(image)
    
    if groups == False:
        for i in xy:
            draw.ellipse((i[0] - p_size/2, i[1] - p_size/2, i[0]+p_size/2, i[1]+p_size/2), fill = (0,0,0))
    else:
        colors = GenerateColors(number)
        for i in xy:
            draw.ellipse((i[0] - p_size/2, i[1] - p_size/2, i[0]+p_size/2, i[1]+p_size/2), fill = colors[i[3]])
    
    draw.rectangle((size/2 - 5, size/2 - 5, size/2 + 5, size/2 + 5), fill = "black")
    del draw
    image.save(path)

def ExportToFile(xyd, workspace_dir, input_file):
    f = open(workspace_dir + '/' + input_file, 'w')
    for point in xyd:
        f.write(str(point[0]) + ',' + str(point[1]) + ',' + str(point[2]) + "\n")
    f.close()
    
def StringTableToInt(table):
    inttable = []
    for line in table:
        row = []
        print line
        for cell in line:
            row.append((int)(cell))
        inttable.append(row)
    return inttable

def ReadCSV(path):
    read_data = []
    with open(path, 'r') as f:
        read_data = f.read()
    all_lines = read_data.split('\n')
    
    table = []
    for line in all_lines:
        if line != '':
            cells = line.split(',')
            row = []
            for cell in cells:
                try:
                    row.append((int)(cell))
                except:
                    print "blad"
            table.append(row)
    return table
    
def GenerateColors(number):
    color_list = []
    for i in range(number):
        if(i==number-1):
            color_list.append("green")
        else:
            if(i%2 == 0):
                color_list.append("red")
            else:
                color_list.append("blue")
    return color_list

def DrawConnections(result, path, size):
    image = Image.new("RGB", (size, size), "white")
    draw = ImageDraw.Draw(image)
    
    depot = pc.Point(size/2, size/2, 0,0, 0, 12)
    depot.IsDepot()
    cycles = []
    all_points = []
    points = []
    next_cycle = -1
    cycle = 0
    for line in result:
        if(line[0] > 10000):
            cycle = pc.Cycle(line[0], line[1], line[2])
            next_cycle = next_cycle + 1
            
        else:
            points.append(pc.Point(line[0], line[1], 0,0, line[2], 11))
            next_cycle = 0
        
        if(next_cycle>0):
            cycle.addDepot(depot)
            cycle.addPoints(points)
            cycles.append(cycle)
            points = []
    
    cycle.addDepot(depot)
    cycle.addPoints(points)
    cycles.append(cycle)
    
    for c in cycles:
        c.Draw(draw, (r.randint(0,200), r.randint(0,200), r.randint(0,200)))
        
    for c in cycles:
        c.DrawP(draw)
    
    del draw
    image.save(path)