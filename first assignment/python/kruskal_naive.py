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
        if _is_acyclic(graph, edge):
            graph.add_from_existing_edge(edge)
        bar.next()
    bar.finish()
    return graph.E.values()

# Controlla se il grafo in input è aciclico o meno
#
# Complessità temporale: O(m)
def _is_acyclic(g: Graph, new_edge: Edge):
    # Controllo che l'arco che si vuole aggiungere non sia un self-loop
    if new_edge.u != new_edge.v:

        # Se almeno uno dei due nodi dell'arco non è presente nel grafo, sono sicuro che non introdurrà un ciclo,
        # in quanto questo arco mi porterà a scroprire almeno un nuovo nodo.
        # Se entrambi i nodi sono presenti nel grafo, allora verifico se il grafo è aciclico
        if new_edge.u.name in g.V and new_edge.v.name in g.V:
            visited = dict([(x.name, False) for x in g.V.values()])
            value = dfs(g.V[new_edge.u.name], g.V[new_edge.v.name], visited)
            return not value
        else:
            return True
    else:
        return False

# Si esegue una DFS modificata in modo da determinare se esiste o meno un percorso da source_node a destination_node
#
# Complessità temporale: O(m)
def dfs(current_node: Vertex, destination_node: Vertex, visited):
    if destination_node == current_node:
        return True

    visited[current_node.name] = True

    for u in current_node.vertices_adjacent.values():
        if not visited[u.name]:
            if dfs(u, destination_node, visited):
                return True

    return False

graph = Graph()
#graph.load_from_file('mst_dataset/input_random_45_8000.txt')
#graph.load_from_file('mst_dataset/input_random_03_10.txt')
#graph.load_from_file('mst_dataset/input_random_05_20.txt')
#graph.load_from_file('mst_dataset/input_random_11_40.txt')
#graph.load_from_file('mst_dataset/input_random_50_10000.txt')
graph.load_from_file('mst_dataset/input_random_59_40000.txt')
#graph.load_from_file('mst_dataset/input_random_68_100000.txt')


from time import perf_counter
s = perf_counter()
res = Kruskal(graph)
f = perf_counter()
print(f"Time: {f-s}")
sum_result = sum(x.weight for x in res)
print(f"MSP: {sum_result}")
