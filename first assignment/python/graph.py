from vertex import Vertex
from edge import Edge
from typing import Dict

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
        new_edge.u.add_incident_edge(new_edge)
        new_edge.v.add_incident_edge(new_edge)

    def remove_edge(self, remove_edge: Edge) -> None:
        del self.E[(remove_edge.u.name, remove_edge.v.name)]
        remove_edge.u.remove_incident_edge(remove_edge)
        remove_edge.v.remove_incident_edge(remove_edge)

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
        print(f"Vertices: {len(self.V)}")
        print(f"Edges: {len(self.E)}")