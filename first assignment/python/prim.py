import heapq
from vertex import Vertex
from graph import Graph
from progress.bar import Bar

def Prim(G: Graph, s: Vertex):
    """G is the graph, s is the starting node"""
    for vertex in G.V.values():
        vertex.key = float('+inf')
        vertex.parent = None
    s.key = 0

    vertex_heap = [s]

    bar = Bar('Processing', max=len(G.V.values()), suffix='%(index)d/%(max)d - ETA: %(eta)ds')
    while len(vertex_heap) != 0:
        u: Vertex = heapq.heappop(vertex_heap)
        if u.visited:
            bar.next()
            continue
        u.visited = True
        for v in u.vertices_adjacent:
            weight = G.get_weight(u,v)
            if not v.visited and weight < v.key:
                v.key = weight
                v.parent = u
                heapq.heappush(vertex_heap, v)
        bar.next()
    bar.finish()


graph = Graph()
#graph.load_from_file('mst_dataset/input_random_05_20.txt')
graph.load_from_file('mst_dataset/input_random_11_40.txt')
#graph.load_from_file('mst_dataset/input_random_50_10000.txt')
#graph.load_from_file('mst_dataset/input_random_53_20000.txt')
#graph.load_from_file('mst_dataset/input_random_68_100000.txt')


starting_node = graph.V['1']
from time import perf_counter
s = perf_counter()
Prim(graph, starting_node)
f = perf_counter()
print(f"Time: {f-s}")

sum_result = sum(x.key for x in graph.V.values())
print(f"MSP: {sum_result}")
