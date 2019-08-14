import yaml

def read_yaml_names_file():
    stream = open('names.yaml', 'r')
    names_root = yaml.safe_load(stream)
    
    return names_root['names']

def convert_name(name):
    names = read_yaml_names_file()

    if name in names:
        return names[name]
    else:
        return name