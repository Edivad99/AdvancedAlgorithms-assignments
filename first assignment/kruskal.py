from typing import List
from vertex import Vertex
from edge import Edge
from graph import Graph
from progress.bar import Bar

def Kruskal(G: Graph):
    temp = list(G.E.values())
    temp.sort(key= lambda x: x.weight)

    A = list()
    #print([(x.u.name,x.v.name) for x in temp])
    bar = Bar('Processing', max=len(temp), suffix='%(index)d/%(max)d - ETA: %(eta)ds')
    for edge in temp:
        if not is_cyclic(A, edge):
            #print(f"Aggiungo {edge.u.name}-{edge.v.name}")
            A.append(edge)
        bar.next()
    bar.finish()
    return A

Vertices = {}
def unite(v1: str, v2: str) -> None:
    newpid = Vertices[v1]
    oldpid = Vertices[v2]
    for k, v in Vertices.items():
        if v == oldpid:
            Vertices[k] = newpid

def is_cyclic(edges: List[Edge], new_edge: Edge):
    Vertices.clear()
    _edges = edges.copy() # shallow copy
    _edges.append(new_edge)
    for edge in _edges:
        if not edge.u.name in Vertices.keys():
            Vertices[edge.u.name] = edge.u
        if not edge.v.name in Vertices.keys():
            Vertices[edge.v.name] = edge.v

        if Vertices[edge.v.name] == Vertices[edge.u.name]:
            return True
        unite(edge.u.name, edge.v.name)
    return False

graph = Graph()
#graph.load_from_file('mst_dataset/input_random_17_1000.txt')
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
