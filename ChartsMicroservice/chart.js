const layout = (text) => {
    return {
        autosize: true,
        colorway: [
            "#636efa",
            "#EF553B",
            "#00cc96",
            "#ab63fa",
            "#19d3f3",
            "#e763fa",
            "#fecb52",
            "#ffa15a",
            "#ff6692",
            "#b6e880"
        ],
        title: {
            text: text
        },
        xaxis: {
            autorange: true,
            range: [-0.5, 7.5],
            type: "category"
        },
        yaxis: {
            autorange: true,
            range: [0, 57.89473684210526],
            type: "linear",
            gridcolor: "#2B2B2B"
        },
        paper_bgcolor: "#1a1a23",
        plot_bgcolor: "#1a1a23",
        font: {
            color: "#ebebeb"
        }
    }
}

module.exports.getOneTraceFigure = (x, y, text) => {
    const trace1 = {
        x: x,
        y: y,
        text: y,
        type: "bar"
    };

    const figure = { 'data': [trace1], 'layout': layout(text) };

    return figure;
}

module.exports.getTwoTraceFigure = (x, y, y2, text, name1, name2) => {
    const trace1 = {
        x: x,
        y: y,
        text: y,
        type: "bar",
        name: name1
    };

    const trace2 = {
        x: x,
        y: y2,
        text: y2,
        type: "bar",
        name: name2
    };

    const figure = { 'data': [trace1, trace2], 'layout': layout(text) };

    return figure;
}