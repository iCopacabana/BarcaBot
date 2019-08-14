const express = require('express');
const config = require('./config.js');
const chart = require('./chart.js');
const plotly = require('plotly')(config.plotly.username, config.plotly.token);
const convertName = require('./convertName.js');

const app = express();
const port = 3000;
const imgOpts = {
    format: 'png',
    width: 1000,
    height: 500
};

app.use(express.json());

app.get('/', function (req, res) {
    const a = {
        apiStatus: "working"
    }

    res.send(a);
});

app.post('/stats/player', function (req, res) {
    const stats = req.body.Per90Stats;
    const name = convertName(req.body.Name);
    const x = ["Shots", "Shots on Target", "Key Passes", "Tackles", "Blocks", "Interceptions", "Duels Won", "Dribbles Attempted", "Dribbles Won", "Fouls Drawn", "Fouls Committed"];
    const y = [stats.Shots.Total, stats.Shots.OnTarget, stats.Passes.KeyPasses, stats.Tackles.TotalTackles, stats.Tackles.Blocks, stats.Tackles.Interceptions, stats.Duels.Won, stats. Dribbles.Attempted, stats.Dribbles.Won, stats.Fouls.Drawn, stats.Fouls.Committed];
    const figure = chart.getOneTraceFigure(x, y, `${name} Per 90 Stats`);
   
    plotly.getImage(figure, imgOpts, function (error, imageStream) {
        if (error) {
            res.status(500).send("error");
            console.log(error)
        } else {
            imageStream.pipe(res);
        }        
    });
});

app.post('/stats/players', function(req, res) {
    const stats1 = req.body.PlayerList[0].Per90Stats;
    const stats2 = req.body.PlayerList[1].Per90Stats;
    const name1 = convertName(req.body.PlayerList[0].Name);
    const name2 = convertName(req.body.PlayerList[1].Name);
    const x = ["Shots", "Shots on Target", "Key Passes", "Tackles", "Blocks", "Interceptions", "Duels Won", "Dribbles Attempted", "Dribbles Won", "Fouls Drawn", "Fouls Committed"];
    const y1 = [stats1.Shots.Total, stats1.Shots.OnTarget, stats1.Passes.KeyPasses, stats1.Tackles.TotalTackles, stats1.Tackles.Blocks, stats1.Tackles.Interceptions, stats1.Duels.Won, stats1.Dribbles.Attempted, stats1.Dribbles.Won, stats1.Fouls.Drawn, stats1.Fouls.Committed];
    const y2 = [stats2.Shots.Total, stats2.Shots.OnTarget, stats2.Passes.KeyPasses, stats2.Tackles.TotalTackles, stats2.Tackles.Blocks, stats2.Tackles.Interceptions, stats2.Duels.Won, stats2.Dribbles.Attempted, stats2.Dribbles.Won, stats2.Fouls.Drawn, stats2.Fouls.Committed];


    const figure = chart.getTwoTraceFigure(x, y1, y2, `${name1} vs ${name2} Per 90 Stats`, name1, name2);
   
    plotly.getImage(figure, imgOpts, function (error, imageStream) {
        if (error) {
            res.status(500).send("error");
            console.log(error)
        } else {
            imageStream.pipe(res);
        }
    });
});

app.listen(port, () => console.log(`Listening on port ${port}!`));
