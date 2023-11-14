(function () {
    var system = require('system');
    var fs = require('fs');
    var config = {
        // define the location of js files
        JQUERY: 'jquery-3.6.3.min.js',
        //ESL: 'esl.js',
        ECHARTS: 'echarts.min.js',
        // default container width and height
        DEFAULT_WIDTH: '1920',
        DEFAULT_HEIGHT: '800'
    }, parseParams, render, pick, usage;

    usage = function () {
        console.log("\nUsage: phantomjs echarts-convert.js -options options -outfile filename -width width -height height"
            + "OR"
            + "Usage: phantomjs echarts-convert.js -infile URL -outfile filename -width width -height height\n");
    };

    pick = function () {
        var args = arguments, i, arg, length = args.length;
        for (i = 0; i < length; i += 1) {
            arg = args[i];
            if (arg !== undefined && arg !== null && arg !== 'null' && arg != '0') {
                return arg;
            }
        }
    };

    parseParams = function () {
        var map = {}, i, key;
        console.log("--logs:\n")
        console.log(system.args)
        if (system.args.length < 2) {
            usage();
            phantom.exit();
        }
        for (i = 0; i < system.args.length; i += 1) {
            if (system.args[i].charAt(0) === '-') {
                key = system.args[i].substr(1, i.length);
                if (key === 'infile') {
                    // get string from file
                    // force translate the key from infile to options.
                    key = 'options';
                    try {
                        map[key] = fs.read(system.args[i + 1]).replace(/^\s+/, '');
                    } catch (e) {
                        console.log('Error: cannot find file, ' + system.args[i + 1]);
                        phantom.exit();
                    }
                } else {

                    map[key] = system.args[i + 1].replace(/^\s+/, '');
                }
            }
        }
        return map;
    };

    render = function (params) {
        var page = require('webpage').create(), createChart;

        var bodyMale = config.SVG_MALE;
        page.onConsoleMessage = function (msg) {
            console.log(msg);
        };

        page.onAlert = function (msg) {
            console.log(msg);
        };

        createChart = function (inputOption, width, height, config) {
            var counter = 0;
            function decrementImgCounter() {
                counter -= 1;
                if (counter < 1) {
                    console.log(messages.imagesLoaded);
                }
            }

            function loadScript(varStr, codeStr) {
                var script = $('<script>').attr('type', 'text/javascript');
                script.html('var ' + varStr + ' = ' + codeStr);
                document.getElementsByTagName("head")[0].appendChild(script[0]);
                if (window[varStr] !== undefined) {
                    console.log('Echarts.' + varStr + ' has been parsed');
                }
            }

            function loadImages() {
                var images = $('image'), i, img;
                if (images.length > 0) {
                    counter = images.length;
                    for (i = 0; i < images.length; i += 1) {
                        img = new Image();
                        img.onload = img.onerror = decrementImgCounter;
                        img.src = images[i].getAttribute('href');
                    }
                } else {
                    console.log('The images have been loaded');
                }
            }
            // load opitons
            if (inputOption != 'undefined') {
                // parse the options
                loadScript('options', inputOption);
                // disable the animation
                options.animation = false;
            }

            // we render the image, so we need set background to white.
            $(document.body).css('backgroundColor', 'white');
            var container = $("<div>").appendTo(document.body);
            container.attr('id', 'container');
            container.css({
                width: width,
                height: height
            });
            // render the chart
            var myChart = echarts.init(container[0]);
            myChart.setOption(options);
            // load images
            loadImages();
            return myChart.getDataURL();
        };

        // parse the params
        page.open("about:blank", function (status) {
            // inject the dependency js
            page.injectJs(config.ESL);
            page.injectJs(config.JQUERY);
            page.injectJs(config.ECHARTS);


            var width = pick(params.width, config.DEFAULT_WIDTH);
            var height = pick(params.height, config.DEFAULT_HEIGHT);

            // create the chart
            var base64 = page.evaluate(createChart, params.options, width, height, config);
            //fs.write("base64.txt", base64);
            // define the clip-rectangle
            page.clipRect = {
                top: 0,
                left: 0,
                width: width,

                height: height
            };
            // render the image
            page.render(params.outfile);
            console.log('render complete:' + params.outfile);
            // exit
            phantom.exit();
        });
    };
    // get the args
    var params = parseParams();

    // validate the params
    if (params.options === undefined || params.options.length === 0) {
        console.log("ERROR: No options or infile found.");
        usage();
        phantom.exit();
    }
    // set the default out file
    if (params.outfile === undefined) {
        var tmpDir = fs.workingDirectory + '/tmp';
        // exists tmpDir and is it writable?
        if (!fs.exists(tmpDir)) {
            try {
                fs.makeDirectory(tmpDir);
            } catch (e) {
                console.log('ERROR: Cannot make tmp directory');
            }
        }
        params.outfile = tmpDir + "/" + new Date().getTime() + ".png";
    }

    // render the image
    render(params);
}());