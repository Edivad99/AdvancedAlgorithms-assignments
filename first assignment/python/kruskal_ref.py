from typing import List
from vertex import Vertex
from edge import Edge
from graph import Graph
from progress.bar import Bar

def Kruskal(G: Graph):
    edges = list(G.E.values())
    edges.sort(key= lambda x: x.weight)

    graph = Graph()
    graph.V = G.V
    graph.E.clear()
    for x in graph.V.values():
        x.edges_incident.clear()

    bar = Bar('Processing', max=len(edges), suffix='%(index)d/%(max)d - ETA: %(eta_td)s, AVG: %(avg)s')
    for edge in edges:
        if not is_cyclic(graph, edge):
            graph._add_edge(edge)
        bar.next()
    bar.finish()
    return graph.E.values()


def is_cyclic(graph: Graph, current: Edge):
    for x in graph.V.values():
        x.visited = False
    for x in graph.E.values():
        x.label = ''

    graph._add_edge(current)

    s = current.u
    s.visited = True
    l0: List[Vertex] = [s]
    #print(f"({current.u.name}-{current.v.name})")
    while l0:
        #print([x.name for x in l0])
        l1: List[Vertex] = []
        for vertex in l0:
            #for vertexEdge in graph.get_incident(vertex):
            for vertexEdge in vertex.edges_incident.values():
                if vertexEdge.label == '':
                    w = vertexEdge.get_opposite(vertex)
                    if not w.visited:
                        vertexEdge.label = 'DISCOVERY'
                        w.visited = True
                        l1.append(w)
                    else:
                        vertexEdge.label = 'CROSS'
                        graph.remove_edge(current)
                        #print(f"!!!!!({current.u.name}-{current.v.name})")
                        return True
        l0.clear()
        l0.extend(l1)
    graph.remove_edge(current)
    return False

graph = Graph()
#graph.load_from_file('mst_dataset/input_random_45_8000.txt')
#graph.load_from_file('mst_dataset/input_random_03_10.txt')
#graph.load_from_file('mst_dataset/input_random_05_20.txt')
#graph.load_from_file('mst_dataset/input_random_11_40.txt')
#graph.load_from_file('mst_dataset/input_random_50_10000.txt')
graph.load_from_file('mst_dataset/input_random_59_40000.txt')
#graph.load_from_file('mst_dataset/input_random_68_100000.txt')


#print(is_cyclic(graph.V['1'], None))

from time import perf_counter
s = perf_counter()
res = Kruskal(graph)
f = perf_counter()
print(f"Time: {f-s}")
sum_result = sum(x.weight for x in res)
print(f"MSP: {sum_result}")
