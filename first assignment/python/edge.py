from vertex import Vertex

class Edge:
    def __init__(self, u: Vertex, v: Vertex, weight: int) -> None:
        self.u = u
        self.v = v
        self.weight = weight
        self.label = ''

    def __eq__(self, other: object) -> bool:
        return isinstance(other, self.__class__) and \
            self.u == other.u and self.v == other.v and \
            self.weight and other.weight

    def get_opposite(self, v: Vertex):
        if v == self.u:
            return self.v
        elif v == self.v:
            return self.u
        raise Exception("NOT FOUND")