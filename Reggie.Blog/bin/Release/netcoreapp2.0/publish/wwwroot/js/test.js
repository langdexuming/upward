// Configure bundling and minification for the project.
// More info at https://go.microsoft.com/fwlink/?LinkId=808241
[{
        "outputFileName": "wwwroot/css/site.min.css",
        // An array of relative input file paths. Globbing patterns supported
        "inputFiles": [
            "wwwroot/css/site.css"

        ]
    },
    {
        "outputFileName": "wwwroot/css/animate.min.css",
        // An array of relative input file paths. Globbing patterns supported
        "inputFiles": [
            "wwwroot/css/animate.css"
        ]
    },
    {
        "outputFileName": "wwwroot/js/site.min.js",
        "inputFiles": [
            "wwwroot/js/site.js"
        ],
        // Optionally specify minification options
        "minify": {
            "enabled": true,
            "renameLocals": true
        },
        // Optionally generate .map file
        "sourceMap": false
    }
]