import heapq
from typing import Dict, List
from progress.bar import Bar

class Vertex:
    def __init__(self, name: str, key: int, parent) -> None:
        self.name = name
        self.key = key
        self.parent = parent
        self.vertices_adjacent: List[Vertex]= []
        self.visited = False
        self.inserted=False

    def __eq__(self, other: object) -> bool:
        return isinstance(other, self.__class__) and self.name == other.name

    def __lt__(self, other: object) -> bool:
        return self.key < other.key

class Edge:
    def __init__(self, u: Vertex, v: Vertex, weight: int) -> None:
        self.u = u
        self.v = v
        self.weight = weight

    def __eq__(self, other: object) -> bool:
        return isinstance(other, self.__class__) and \
            self.u == other.u and self.v == other.v and \
            self.weight and other.weight

class Graph:
    def __init__(self) -> None:
        self.V: Dict[str, Vertex] = {}
        self.E: Dict[(str, str), Edge] = {}

    def _add_vertex(self, new_vertex: Vertex) -> Vertex:
        if new_vertex.name in self.V:
            return self.V[new_vertex.name]
        self.V[new_vertex.name] = new_vertex
        return new_vertex

    def _add_edge(self, new_edge: Edge) -> None:
        if new_edge.u == new_edge.v:
            print(f"**SELF LOOP DETECTED (Vertex: {new_edge.u.name})**")
        self.E[(new_edge.u.name, new_edge.v.name)] = new_edge

    def get_weight(self, u: Vertex, v: Vertex) -> int:
        if (u.name, v.name) in self.E:
            return self.E[(u.name, v.name)].weight
        if (v.name, u.name) in self.E:
            return self.E[(v.name, u.name)].weight
        raise Exception("NOT FOUND")

    def load_from_file(self, file_path: str) -> None:
        self.E.clear()
        self.V.clear()
        with open(file_path, 'r') as file:
            vertices, edges = file.readline().split(' ')
            remain_lines = file.readlines()

        for line in remain_lines:
            v1, v2, w = line.strip().split(' ')

            u = self._add_vertex(Vertex(v1, float('+inf'), None))
            v = self._add_vertex(Vertex(v2, float('+inf'), None))
            u.vertices_adjacent.append(v)
            v.vertices_adjacent.append(u)
            self._add_edge(Edge(u, v, int(w)))

        assert int(vertices) == len(self.V), f"The list of vertices has a different size compared to the number read from file ({int(vertices)}!={len(self.V)})"
        assert int(edges) == len(self.E), f"The list of edges has a different size compared to the number read from file ({int(edges)}!={len(self.E)})"

def Prim(G: Graph, s: Vertex):
    """G is the graph, s is the starting node"""
    for vertex in G.V.values():
        vertex.key = float('+inf')
        vertex.parent = None
    s.key = 0

    vertex_heap = [s]
    s.inserted = True

    bar = Bar('Processing', max=len(G.V.values()), suffix='%(index)d/%(max)d - ETA: %(eta)ds')
    while len(vertex_heap) != 0:
        #print([x.name for x in vertex_heap])
        u: Vertex = heapq.heappop(vertex_heap)
        u.visited = True
        #print(f"Estratto vertice: {u.name}")
        for v in u.vertices_adjacent:
            weight = G.get_weight(u,v)
            if not v.visited and weight < v.key:
                v.key = weight
                v.parent = u
                if not v.inserted:
                    v.inserted = True
                    heapq.heappush(vertex_heap, v)
        bar.next()
    bar.finish()


graph = Graph()
graph.load_from_file('mst_dataset/input_random_03_10.txt')
#graph.load_from_file('mst_dataset/input_random_50_10000.txt')
#graph.load_from_file('mst_dataset/input_random_53_20000.txt')
#graph.load_from_file('mst_dataset/input_random_68_100000.txt')

starting_node = graph.V['1']
from time import perf_counter_ns
s = perf_counter_ns()
Prim(graph, starting_node)
f = perf_counter_ns()
print(f"Time: {f-s}")

sum_result = sum(x.key for x in graph.V.values())
print(f"MSP: {sum_result}")
for x in graph.V.values():
    print(f"Nodo: {x.name}, parent: {x.parent.name if x.parent else x.name}")
