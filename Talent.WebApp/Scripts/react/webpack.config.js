
module.exports = {
    context: __dirname,
    entry: {
        accountProfile: './ReactScripts/AccountProfile.js',
        homePage: './ReactScripts/Home.js',
        searchResult: './ReactScripts/SearchResult.js',
        homePage: './ReactScripts/Home.js',
        serviceDetail: './ReactScripts/ServiceDetail.js',
        serviceListing: './ReactScripts/ServiceListing.js'
    },
    output:
    {
        path: __dirname + "/dist",
        filename: "[name].bundle.js"
    },
    watch: true,
    mode: 'development',
    module: {
        rules: [
            {
                test: /\.jsx?$/,
                exclude: /(node_modules)/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['babel-preset-env', 'babel-preset-react']
                    }
                }
            }
        ]
    }
}