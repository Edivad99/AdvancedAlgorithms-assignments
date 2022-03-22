import heapq
from typing import Dict, List

class Vertex:
    def __init__(self, name, key, parent) -> None:
        self.name = name
        self.key = key
        self.parent = parent

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
        self.E[(new_edge.u.name, new_edge.v.name)] = new_edge

    def get_adjacent(self, vertex: Vertex) -> List[Vertex]:
        adjacent_vertices = []
        for edge in self.E.values():
            if vertex == edge.u:
                adjacent_vertices.append(edge.v)
            elif vertex == edge.v:
                adjacent_vertices.append(edge.u)
        return adjacent_vertices

    def get_weight(self, u: Vertex, v:Vertex) -> int:
        if (u.name, v.name) in self.E:
            return self.E[(u.name, v.name)].weight
        if (v.name, u.name) in self.E:
            return self.E[(v.name, u.name)].weight
        raise Exception("NOT FOUND")

    def load_from_file(self, file_path: str) -> None:
        with open(file_path, 'r') as file:
            vertices, edges = file.readline().split(' ')
            remain_lines = file.readlines()

        for line in remain_lines:
            v1, v2, w = line.strip().split(' ')

            u = self._add_vertex(Vertex(v1, float('+inf'), None))
            v = self._add_vertex(Vertex(v2, float('+inf'), None))
            self._add_edge(Edge(u, v, int(w)))

        assert int(vertices) == len(self.V), f"The list of vertices has a different size compared to the number read from file ({int(vertices)}!={len(self.V)})"
        assert int(edges) == len(self.E), f"The list of edges has a different size compared to the number read from file ({int(edges)}!={len(self.E)})"

def Prim(G: Graph, s: Vertex):
    """G is the graph, s is the starting node"""
    for key, vertex in G.V.items():
        vertex.key = float('+inf')
        vertex.parent = None
    s.key = 0
    vertex_heap = list(G.V.values())
    heapq.heapify(vertex_heap)
    while len(vertex_heap) != 0:
        u: Vertex = heapq.heappop(vertex_heap)
        #print(f"Estratto vertice: {u.name}")
        for v in G.get_adjacent(u):
            weight = G.get_weight(u,v)
            if vertex_heap.count(v) == 1 and weight < v.key:
                v.key = weight
                v.parent = u
        heapq.heapify(vertex_heap)

    print("-"*20)
graph = Graph()
#graph.load_from_file('mst_dataset/input_random_02_10.txt')
#graph.load_from_file('mst_dataset/input_random_50_10000.txt')
graph.load_from_file('mst_dataset/input_random_68_100000.txt')

starting_node = graph.V['1']
Prim(graph, starting_node)

for x in graph.V.values():
    print(f"Nodo: {x.name}, parent: {x.parent.name if x.parent else x.name}")
