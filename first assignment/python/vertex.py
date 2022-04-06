from typing import List

class Vertex:
    def __init__(self, name: str, key: int, parent) -> None:
        self.name = name
        self.key = key
        self.parent = parent
        self.vertices_adjacent: List[Vertex]= []
        self.edges_incident = {}
        self.visited = False
        self.inserted = False

    def __eq__(self, other: object) -> bool:
        return isinstance(other, self.__class__) and self.name == other.name

    def __lt__(self, other: object) -> bool:
        return self.key < other.key

    def add_incident_edge(self, edge) -> None:
        self.edges_incident[(edge.u.name, edge.v.name)] = edge

    def remove_incident_edge(self, edge) -> None:
        del self.edges_incident[(edge.u.name, edge.v.name)]