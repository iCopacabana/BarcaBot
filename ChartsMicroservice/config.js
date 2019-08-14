const fs = require('fs');
const yaml = require('yaml');
    
const file = fs.readFileSync('../config.yaml', 'utf8');

module.exports = yaml.parse(file);
