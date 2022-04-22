from typing import Dict

class Vertex:
    def __init__(self, name: str, key: int, parent = None) -> None:
        self.name = name
        self.key = key
        self.parent = parent
        self.vertices_adjacent: Dict[str, Vertex] = {}
        self.edges_incident = {}
        self.visited = False

    def __eq__(self, other: object) -> bool:
        return isinstance(other, self.__class__) and self.name == other.name

    def __lt__(self, other: object) -> bool:
        return self.key < other.key

    def add_incident_edge(self, edge) -> None:
        if not (edge.u.name, edge.v.name) in self.edges_incident:
            self.edges_incident[(edge.u.name, edge.v.name)] = edge

    def remove_incident_edge(self, edge) -> None:
        if (edge.u.name, edge.v.name) in self.edges_incident:
            del self.edges_incident[(edge.u.name, edge.v.name)]

    def add_adjacent_vertices(self, vertex) -> None:
        if not vertex.name in self.vertices_adjacent:
            self.vertices_adjacent[vertex.name] = vertex

    def remove_adjacent_vertices(self, vertex) -> None:
        if vertex.name in self.vertices_adjacent:
            del self.vertices_adjacent[vertex.name]