const path = require('path');
//const webpack = require('webpack');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = {
    mode: 'development',
    devtool: 'inline-source-map',
    entry: './src/index.js',
    output: {
        path: path.resolve(__dirname, '../wwwroot/'),
        filename: 'TUI.blazor_calendar.min.js',
    },
    plugins: [
        new MiniCssExtractPlugin(
            {
                filename: 'TUI.blazorCalendar.css'
            })
    ],
    module: {
        rules: [
            {
                test: /\.css$/i,
                use: [MiniCssExtractPlugin.loader, 'css-loader'],
            },
        ]
    }

};