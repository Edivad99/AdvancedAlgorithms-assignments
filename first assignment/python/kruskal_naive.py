from typing import Dict, List, Set
from vertex import Vertex
from edge import Edge
from graph import Graph
from progress.bar import Bar

def Kruskal(G: Graph):
    edges = list(G.E.values())
    edges.sort(key= lambda x: x.weight)

    graph = Graph()
    

    bar = Bar('Processing', max=len(edges), suffix='%(index)d/%(max)d - ETA: %(eta_td)s, AVG: %(avg)s')
    for edge in edges:
        graph.add_from_existing_edge(edge)
        print(f"Add ({edge.u.name},{edge.v.name})")
        node_visited = set()
        if dfs(graph.V[edge.u.name], None, node_visited):
            print(f"Remove ({edge.u.name},{edge.v.name})")
            graph.remove_edge(edge)
        print('*'*30)
        #bar.next()
    #bar.finish()
    return graph.E.values()

def visit(v: Vertex) -> bool:
    if v.color == 'grey':
        return True # Cycle found

    v.color = 'grey'
    for child in v.vertices_adjacent.values():
        if child.color == 'white':
            return visit(child)
    v.color = 'black'
    return False

def is_cycle_2(graph: Graph) -> bool:
    on_stack: Dict[str, bool] = {}
    st = []

    for w in graph.V.values():
        if w.visited:
            continue

        st.append(w)
        while st:
            vertex_top: Vertex = st[-1]

            if not vertex_top.visited:
                vertex_top.visited = True
                on_stack[vertex_top.name] = True
            else:
                on_stack[vertex_top.name] = False
                st.pop()

            for v in vertex_top.vertices_adjacent.values():
                if not v.visited:
                    st.append(v)
                elif v.name in on_stack and on_stack[v.name]:
                    return True
    return False

def dfs(vertex: Vertex, parent: Vertex, node_visited: Set[str]) -> bool:
    node_visited.add(vertex.name)
    for child in vertex.vertices_adjacent.values():
        if child.name in node_visited:
            if child != parent:
                return True #Back edge
        else:
            return dfs(child, vertex, node_visited)
    return False


def is_cyclic(graph: Graph, current: Edge):
    for x in graph.V.values():
        x.visited = False
    for x in graph.E.values():
        x.label = ''

    graph.add_edge(current)

    s = current.u
    s.visited = True
    l0: List[Vertex] = [s]
    while l0:
        l1: List[Vertex] = []
        for vertex in l0:
            for vertexEdge in vertex.edges_incident.values():
                if vertexEdge.label == '':
                    w = vertexEdge.get_opposite(vertex)
                    if not w.visited:
                        vertexEdge.label = 'DISCOVERY'
                        w.visited = True
                        l1.append(w)
                    else:
                        vertexEdge.label = 'CROSS'
                        graph.remove_edge(current)
                        return True
        l0.clear()
        l0.extend(l1)
    graph.remove_edge(current)
    return False

graph = Graph()
#graph.load_from_file('mst_dataset/input_random_45_8000.txt')
graph.load_from_file('mst_dataset/input_random_03_10.txt')
#graph.load_from_file('mst_dataset/input_random_05_20.txt')
#graph.load_from_file('mst_dataset/input_random_11_40.txt')
#graph.load_from_file('mst_dataset/input_random_50_10000.txt')
#graph.load_from_file('mst_dataset/input_random_59_40000.txt')
#graph.load_from_file('mst_dataset/input_random_68_100000.txt')

test = set()
print(dfs(graph.V['1'], None, test))
def test():
    from time import perf_counter
    s = perf_counter()
    res = Kruskal(graph)
    f = perf_counter()
    print(f"Time: {f-s}")
    sum_result = sum(x.weight for x in res)
    print(f"MSP: {sum_result}")

test()
