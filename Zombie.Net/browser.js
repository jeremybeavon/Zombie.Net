var Browser = require('zombie');
var getCookies = function (currentCookies) {
    return {
        all: function (data, callback) {
            callback(null, currentCookies.all());
        },
        clear: function (data, callback) {
            currentCookies.clear();
            callback(null, null);
        },
        dump: function (data, callback) {
            currentCookies.dump();
            callback(null, null);
        },
        get: function (data, callback) {
            callback(null, currentCookies.get(data));
        },
        set: function (data, callback) {
            currentCookies.set(data.name, data.value);
            callback(null, null);
        },
        remove: function (data, callback) {
            currentCookies.remove(data);
            callback(null, null);
        }
    };
};
var getDocument = function (currentDocument) {
    return {};
};
var getElement = function (currentElement) {
    return {};
};
var getElements = function (elements) {
    var newElements = [];
    for (var index = 0; index < elements.length; index++) {
        newElements.push(getElement(elements[index]));
    }

    return newElements;
};
var getHistory = function (currentHistory) {
    return {
        back: function (data, callback) {
            currentHistory.back();
            callback(null, null);
        },
        forward: function (data, callback) {
            currentHistory.forward();
            callback(null, null);
        },
        go: function (data, callback) {
            currentHistory.go(data);
            callback(null, null);
        }
    };
};
var getLocation = function (currentLocation) {
    return {
        assign: function (data, callback) {
            currentLocation.assign(data);
            callback(null, null);
        },
        hash: function (data, callback) {
            callback(null, currentLocation.hash);
        },
        host: function (data, callback) {
            callback(null, currentLocation.host);
        },
        hostname: function (data, callback) {
            callback(null, currentLocation.hostname);
        },
        href: function (data, callback) {
            callback(null, currentLocation.href);
        },
        origin: function (data, callback) {
            callback(null, currentLocation.origin);
        },
        pathname: function (data, callback) {
            callback(null, currentLocation.pathname);
        },
        port: function (data, callback) {
            callback(null, currentLocation.port);
        },
        protocol: function (data, callback) {
            callback(null, currentLocation.protocol);
        },
        reload: function (data, callback) {
            currentLocation.reload(data);
            callback(null, null);
        },
        replace: function (data, callback) {
            currentLocation.replace(data);
            callback(null, null);
        },
        search: function (data, callback) {
            callback(null, currentLocation.search);
        }
    };
};
var getResources = function (currentResources) {
    return {
        addRequestHandler: function (data, callback) {
            currentResources.addHandler(function (request, next) {
                data({
                    request: request,
                    next: function (nextData, nextCallback) {
                        nextCallback(null, null);
                        if (nextData.response) {
                            next(nextData.error, nextData.response);
                        } else if (nextData.error) {
                            next(nextData.error);
                        } else {
                            next();
                        }
                    }
                }, null)
            });
            callback(null, null);
        },
        addResponseHandler: function (data, callback) {
            currentResources.addHandler(function (request, response, next) {
                data({
                    request: request,
                    response: response,
                    next: function (nextData, nextCallback) {
                        if (nextData) {
                            next(nextData);
                        } else {
                            next();
                        }

                        nextCallback(null, null);
                    }
                }, null);
            });
            callback(null, null);
        },
        delay: function (data, callback) {
            currentResources.delay(data.url, data.delay);
            callback(null, null);
        },
        dump: function (data, callback) {
            currentResources.dump();
            callback(null, null);
        },
        get: function (data, callback) {
            currentResources.get(data.url, function (error, response) {
                data.callback(null, { error: error, response: response });
            });
            callback(null, null);
        },
        mock: function (data, callback) {
            currentResources.mock(data.url, data.response);
            callback(null, null);
        },
        post: function (data, callback) {
            currentResources.post(data.url, data.options, function (error, response) {
                data.callback(null, { error: error, response: response });
            });
            callback(null, null);
        },
        request: function (data, callback) {
            currentResources.request(data.method, data.url, data.options, function (error, response) {
                data.callback(null, { error: error, response: response });
            });
            callback(null, null);
        },
        restore: function (data, callback) {
            currentResources.restore(data);
            callback(null, null);
        }
    };
};
var getStorage = function (currentStorage) {
    return {
        clear: function (data, callback) {
            currentStorage.clear();
            callback(null, null);
        },
        dump: function (data, callback) {
            currentStorage.dump();
            callback(null, null);
        },
        getItem: function (data, callback) {
            callback(null, currentStorage.getItem());
        },
        key: function (data, callback) {
            callback(null, currentStorage.key(data));
        },
        length: function(data, callback) {
            callback(null, currentStorage.length);
        },
        removeItem: function (data, callback) {
            currentStorage.removeItem(data);
            callback(null, null);
        },
        setItem: function (data, callback) {
            currentStorage.setItem(data.name, data.value);
            callback(null, null);
        }
    };
};
var getWindow = function (currentWindow) {
    return {
        history: function (data, callback) {
            callback(null, getHistory(currentWindow.history));
        },
        location: function (data, callback) {
            callback(null, getLocation(currentWindow.location));
        },
        setLocation: function (data, callback) {
            currentWindow.location = data;
            callback(null, null);
        }
    };
};
var getBrowser = function (browser) {
    return {
        attach: function (data, callback) {
            browser.attach(data.selector, data.filename, function () {
                callback(null, null);
            });
        },
        back: function (data, callback) {
            browser.back(function () {
                callback(null, null);
            });
        },
        body: function (data, callback) {
            callback(null, getElement(browser.body));
        },
        button: function (data, callback) {
            callback(null, getElement(browser.button(data)));
        },
        check: function (data, callback) {
            browser.check(data, function () {
                callback(null, null);
            });
        },
        choose: function (data, callback) {
            browser.choose(data, function () {
                callback(null, null);
            });
        },
        clickLink: function (data, callback) {
            browser.clickLink(data, function () {
                callback(null, null);
            });
        },
        close: function (data, callback) {
            browser.close();
            callback(null, null);
        },
        cookies: function (data, callback) {
            data = data || {};
            callback(null, getCookies(browser.cookies(data.domain, data.path)));
        },
        document: function (data, callback) {
            callback(null, getDocument(browser.document));
        },
        evaluate: function (data, callback) {
            callback(null, browser.evaluate(data));
        },
        field: function (data, callback) {
            callback(null, getElement(browser.field(data)));
        },
        fill: function (data, callback) {
            browser.fill(data.field, data.value, function () {
                callback(null, null);
            });
        },
        focused: function (data, callback) {
            callback(null, getElement(browser.focused));
        },
        fork: function (data, callback) {
            callback(null, getBrowser(browser.fork));
        },
        html: function (data, callback) {
            data = data || {};
            callback(null, browser.html(data.selector, data.context));
        },
        link: function (data, callback) {
            callback(null, getElement(browser.link(data)));
        },
        load: function (data, callback) {
            browser.load(data, function () {
                callback(null, null);
            });
        },
        loadCookies: function (data, callback) {
            browser.loadCookies(data);
            callback(null, null);
        },
        loadHistory: function (data, callback) {
            browser.loadHistory(data);
            callback(null, null);
        },
        loadStorage: function (data, callback) {
            browser.loadStorage(data);
            callback(null, null);
        },
        localStorage: function (data, callback) {
            callback(null, getStorage(browser.localStorage(data)));
        },
        location: function (data, callback) {
            callback(null, getLocation(browser.location));
        },
        onAlert: function (data, callback) {
            browser.onalert(function (message) {
                data(message, null);
            });
            callback(null, null);
        },
        onConfirm: function (data, callback) {
            if (data.confirmFunction) {
                browser.onconfirm(data.confirmFunction);
            } else {
                browser.onconfirm(data.question, data.response);
            }
            callback(null, null);
        },
        onPrompt: function (data, callback) {
            if (data.promptFunction) {
                browser.onprompt(data.promptFunction);
            } else {
                browser.onprompt(data.message, data.response);
            }
            callback(null, null);
        },
        pressButton: function (data, callback) {
            browser.pressButton(data, function () {
                callback(null, null);
            });
        },
        prompted: function (data, callback) {
            callback(null, browser.prompted(data));
        },
        queryAll: function (data, callback) {
            callback(null, getElements(browser.queryAll(data.selector)));
        },
        query: function (data, callback) {
            callback(null, getElement(browser.query(data.selector)));
        },
        redirected: function (data, callback) {
            callback(null, browser.redirected);
        },
        reload: function (data, callback) {
            browser.reload(function () {
                callback(null, null);
            });
        },
        resources: function (data, callback) {
            callback(null, getResources(browser.resources));
        },
        saveCookies: function (data, callback) {
            callback(null, browser.saveCookies());
        },
        saveHistory: function (data, callback) {
            callback(null, browser.saveHistory());
        },
        saveStorage: function (data, callback) {
            callback(null, browser.saveStorage());
        },
        select: function (data, callback) {
            browser.select(data.field, data.value, function () {
                callback(null, null);
            });
        },
        sessionStorage: function (data, callback) {
            callback(null, getStorage(browser.sessionStorage(data)));
        },
        setLocation: function (data, callback) {
            currentWindow.location = data;
            callback(null, null);
        },
        statusCode: function (data, callback) {
            callback(null, browser.statusCode);
        },
        success: function (data, callback) {
            callback(null, browser.success);
        },
        text: function (data, callback) {
            callback(null, browser.text(data.selector));
        },
        uncheck: function (data, callback) {
            browser.uncheck(data, function () {
                callback(null, null);
            });
        },
        unselect: function (data, callback) {
            browser.unselect(data.field, data.value, function () {
                callback(null, null);
            });
        },
        url: function (data, callback) {
            callback(null, browser.url);
        },
        visit: function (data, callback) {
            browser.visit(data.url, data.options || {}, function (error) {
                callback(null, error);
            });
        },
        wait: function (data, callback) {
            var callbackFunction = function () {
                callback(null, null);
            };
            if (!data) {
                browser.wait(callback);
            } else if (data.duration) {
                browser.wait(data.duration, callback);
            } else {
                browser.wait(function (window) {
                    var success = false;
                    data.done(getWindow(window), function (result) {
                        success = result;
                    });
                    return success;
                }, callback);
            }
        }
    };
};
var browserFactory = function (data, callback) {
    callback(null, getBrowser(data ? new Browser(data) : Browser.create()));
};
