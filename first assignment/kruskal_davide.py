import heapq
from typing import List
from vertex import Vertex
from edge import Edge
from graph import Graph
from progress.bar import Bar

def Kruskal(G: Graph):
    temp = list(G.E.values())
    temp.sort(key= lambda x: x.weight)

    A = list()
    print([(x.u.name,x.v.name) for x in temp])
    #bar = Bar('Processing', max=len(temp), suffix='%(index)d/%(max)d - ETA: %(eta)ds')
    for edge in temp:
        if not is_cyclic_mio(A, edge):
            print(f"Aggiungo {edge.u.name}-{edge.v.name}")
            A.append(edge)
        #bar.next()
    #bar.finish()
    return A


def is_cyclic(node: Vertex, parent: Vertex):
    node.visited = True
    for vertex in node.vertices_adjacent:
        if vertex.visited:
            if vertex != parent:
                return True
        elif (is_cyclic(vertex, node)):
            return True
    return False

def is_cyclic_mio(A: List[Edge], new_edge: Edge):
    A_copy=list(A)
    A_copy.append(new_edge)
    
    result = {}
    for edge in A_copy:
        if edge.u.name in result:
            return True
        else:
            result[edge.u.name] = 1
        if edge.v.name in result:
            return True
        else:
            result[edge.v.name] = 1
    
    for x in result.values():
        if x > 1:
            return True
    return False
    

graph = Graph()
#graph.load_from_file('mst_dataset/input_random_01_10.txt')
graph.load_from_file('mst_dataset/input_random_03_10.txt')
#graph.load_from_file('mst_dataset/input_random_05_20.txt')
#graph.load_from_file('mst_dataset/input_random_11_40.txt')
#graph.load_from_file('mst_dataset/input_random_50_10000.txt')
#graph.load_from_file('mst_dataset/input_random_53_20000.txt')
#graph.load_from_file('mst_dataset/input_random_68_100000.txt')


#print(is_cyclic(graph.V['1'], None))

from time import perf_counter
s = perf_counter()
res = Kruskal(graph)
f = perf_counter()
print(f"Time: {f-s}")
sum_result = sum(x.weight for x in res)
print(f"MSP: {sum_result}")
