import sys
import csv
import random as r
import math as m
import Image
import ImageDraw
import ImageFont
import copy

class Cycle:
    def __init__(self, id, length, demands):
        self.id = id
        self.length = length
        self.demands = demands
        
    def addPoints(self, points):
        self.points = copy.copy(points)
    
    def addDepot(self, depot):
        self.depot = copy.copy(depot)   
    
    def Draw(self, g, color = "black"):
        self.color = copy.copy(color)
        self.points.insert(0, self.depot)
        self.points.append(self.depot)
        for i in range(len(self.points) - 1):
            line = (self.points[i].x, self.points[i].y, self.points[i+1].x, self.points[i+1].y)
            g.line(line, fill = self.color, width = 3)
            
    def DrawP(self, g,):
        for i in range(len(self.points) - 1):
            self.points[i].color = self.color
            self.points[i].Draw(g)

class Point:
    def __init__(self, x, y, r, fi, demand, p_size, color = "black"):
        self.x = copy.copy(x)
        self.y = copy.copy(y)
        self.R = r
        self.FI = fi
        self.demand = copy.copy(demand)
        self.isDepot = False
        self.size = p_size
        self.color = color
    
    def GenerateDemand(self, min_demand, max_demand):
        self.demand = r.randint(min_demand, max_demand)
        
    def GenerateXY(self, min_x, max_x, min_y, max_y):
        self.x = r.randint(min_x, max_x)
        self.y = r.randint(min_y, max_y)
        
    def GenerateR_FI(self, min_r, max_r, min_fi, max_fi):
        self.R = r.randint(min_r, max_r)
        self.FI = r.randint(min_fi, max_fi)
        
    def ConvertR_FItoXY(self, middle_x, middle_y):
        pi = m.acos(-1)
        self.x = middle_x + self.R*m.cos(self.FI * pi / 180)
        self.x = middle_y + self.R*m.sin(self.FI * pi / 180)
    
    def IsDepot(self):
        self.isDepot = True
    
    def Draw(self, g):
        rect = (self.x - self.size, self.y - self.size, self.x + self.size, self.y + self.size)
        if(self.isDepot == False):
            g.ellipse(rect, fill = self.color)
            
            FONT = ImageFont.load_default()
            demand = str(self.demand)
            (w,h) = g.textsize(demand, font = FONT)
            g.text((self.x - w/2, self.y - h/2), demand, font = FONT, fill = "white")
        else:
            g.rectangle(rect, fill = "red")
        
       