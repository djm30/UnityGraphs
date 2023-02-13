import random
import os


def generate_csr_representation(num_vertices, max_out_degree):
    csr = []
    for i in range(num_vertices):
        out_degree = min(random.randint(0, max_out_degree), num_vertices - 1)
        csr_row = [i]
        selected_nodes = set()
        for j in range(out_degree):
            destination_node = i
            while destination_node == i or destination_node in selected_nodes:
                destination_node = random.randint(0, num_vertices - 1)
            selected_nodes.add(destination_node)
            csr_row.append(destination_node)
        csr.append(csr_row)
    return csr


def save_csr_to_file(csr, filename):
    directory = '../Graphs/'
    if not os.path.exists(directory):
        os.makedirs(directory)
    file_path = os.path.join(directory, filename + '.csr')
    with open(file_path, 'w') as f:
        for row in csr:
            f.write(' '.join(str(x) for x in row) + '\n')


if __name__ == '__main__':
    num_vertices = int(input('Enter the number of vertices: '))
    max_out_degree = int(input('Enter the maximum out-degree: '))
    filename = input('Enter the filename: ')
    csr = generate_csr_representation(num_vertices, max_out_degree)
    save_csr_to_file(csr, filename)
