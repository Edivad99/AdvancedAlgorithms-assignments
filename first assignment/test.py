from collections import defaultdict
import heapq


def create_spanning_tree(graph, starting_vertex):
    mst = defaultdict(set)
    visited = set([starting_vertex])
    edges = [
        (cost, starting_vertex, to)
        for to, cost in graph[starting_vertex].items()
    ]
    heapq.heapify(edges)

    while edges:
        cost, frm, to = heapq.heappop(edges)
        if to not in visited:
            visited.add(to)
            mst[frm].add(to)
            for to_next, cost in graph[to].items():
                if to_next not in visited:
                    heapq.heappush(edges, (cost, to, to_next))

    return mst

example_graph = {
    '1': {'2': -2650},
    '2': {'3': -3425, '9': -8320, '18': -1838, '11': 8976, '2':-2650},
    '3': {'2': -3425, '4': -2263},
    '4': {'3': -2263, '5': -9004, '7':7004},
    '5': {'4': -9004, '6': 5412},
    '6': {'5': 5412, '7': 8840},
    '7': {'4': 7004, '6':8840, '8':-5702, '16':-5635},
    '8': {'7': -5702, '9':298},
    '9': {'8': 298, '2':-8320, '10':7163},
    '10': {'9': 7163, '11':6312},
    '11': {'10': 6312, '12':-1858, '2':8976},
    '12': {'11': -1858, '13':1831},
    '13': {'12': 1831, '14':2533},
    '14': {'13': 2533, '15':530},
    '15': {'16': 396, '14':530},
    '16': {'15': 396, '7':-5635, '17':-2481},
    '17': {'16': -2481, '18':9470},
    '18': {'17': 9470, '19':3302, '2':-1838},
    '19': {'18': 3302, '20':-9459},
    '20': {'19': -9459},
}

print(dict(create_spanning_tree(example_graph, '1')))