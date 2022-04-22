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

    def _remove_vertex(self, old_vertex: Vertex) -> Vertex:
        if old_vertex.name in self.V:
            # Remove only if vertices_adjacent == 0
            if not old_vertex.vertices_adjacent:
                del self.V[old_vertex.name]

    def add_edge(self, u_name: str, v_name: str, w:int) -> None:
        u = self._add_vertex(Vertex(u_name, float('+inf')))
        v = self._add_vertex(Vertex(v_name, float('+inf')))

        u.add_adjacent_vertices(v)
        v.add_adjacent_vertices(u)

        new_edge = Edge(u, v, w)
        self.E[(u.name, v.name)] = new_edge
        u.add_incident_edge(new_edge)
        v.add_incident_edge(new_edge)

    def add_from_existing_edge(self, new_edge: Edge) -> None:
        self.add_edge(new_edge.u.name, new_edge.v.name, new_edge.weight)

    def remove_edge(self, remove_edge: Edge) -> None:
        del self.E[(remove_edge.u.name, remove_edge.v.name)]
        remove_edge.u.remove_incident_edge(remove_edge)
        remove_edge.v.remove_incident_edge(remove_edge)

        remove_edge.u.remove_adjacent_vertices(remove_edge.v)
        remove_edge.v.remove_adjacent_vertices(remove_edge.u)

        #self._remove_vertex(remove_edge.u)
        #self._remove_vertex(remove_edge.v)

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
            self.add_edge(v1, v2, int(w))

        assert int(vertices) == len(self.V), f"The list of vertices has a different size compared to the number read from file ({int(vertices)}!={len(self.V)})"
        assert int(edges) == len(self.E), f"The list of edges has a different size compared to the number read from file ({int(edges)}!={len(self.E)})"
        print(f"Vertices: {len(self.V)}")
        print(f"Edges: {len(self.E)}")