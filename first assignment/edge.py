from vertex import Vertex

class Edge:
    def __init__(self, u: Vertex, v: Vertex, weight: int) -> None:
        self.u = u
        self.v = v
        self.weight = weight

    def __eq__(self, other: object) -> bool:
        return isinstance(other, self.__class__) and \
            self.u == other.u and self.v == other.v and \
            self.weight and other.weight