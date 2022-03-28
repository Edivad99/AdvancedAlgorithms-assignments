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
    heapq.heapify(vertex_heap)

    #bar = Bar('Processing', max=len(G.V.values()), suffix='%(index)d/%(max)d - ETA: %(eta)ds')
    while len(vertex_heap) != 0:
        '''stampa = ''
        for x in vertex_heap:
            stampa = stampa + str(x.key) + ' '
        print(stampa.strip())'''
        u: Vertex = heapq.heappop(vertex_heap)
        if u.visited:
            #bar.next()
            continue
        u.visited = True
        print(f"Estratto vertice: {u.name}, key: {u.key}")
        print("------");
        for v in u.vertices_adjacent:
            weight = G.get_weight(u,v)
            if not v.visited and weight < v.key:
                v.key = weight
                v.parent = u
                heapq.heappush(vertex_heap, v)
        #bar.next()
    #bar.finish()


graph = Graph()
#graph.load_from_file('mst_dataset/input_random_05_20.txt')
graph.load_from_file('mst_dataset/input_random_11_40.txt')
#graph.load_from_file('mst_dataset/input_random_50_10000.txt')
#graph.load_from_file('mst_dataset/input_random_53_20000.txt')
#graph.load_from_file('mst_dataset/input_random_68_100000.txt')



'''example_graph = {}
for x in graph.V.values():
    vertici = {}
    for y in x.vertices_adjacent:
        vertici[y.name] = graph.get_weight(x,y)
    example_graph[x.name] = vertici
#print(example_graph)
from prim2 import create_spanning_tree
print(dict(create_spanning_tree(example_graph, '1')))'''

starting_node = graph.V['1']
from time import perf_counter
s = perf_counter()
Prim(graph, starting_node)
f = perf_counter()
print(f"Time: {f-s}")

sum_result = sum(x.key for x in graph.V.values())
print(f"MSP: {sum_result}")

'''with open('parent_python.csv', 'w')as file:
    file.write("nodo;parent\n")
    for x in graph.V.values():
        file.write(f"{x.name};{x.parent.name if x.parent else x.name}\n")'''
