const fs = require('fs');
const yaml = require('yaml');
    
const file = fs.readFileSync('../ImageManipulationMicroservice/names.yaml', 'utf8');

const names = yaml.parse(file).names;

module.exports = (name) => {
    if (name in names) {
        return names[name];
    } else {
        return name;
    }
}