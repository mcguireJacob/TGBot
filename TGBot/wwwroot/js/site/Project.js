/*
 * With this file I am saying this is a start point for the javascript files you might write for this project.
 * I am not trying to say that all of your scripts should go in this file.
 * For the most part, only the scripts needed on a page should load with that page.
 * The goal is that this file goes away and you create the stucture that fits the site design.
 * With that said, you can use this file to put all of the helpers you use from NKSites.js, in the same place, behind a common name(namespace).
 * I have added an example of how to use this to bring in a helper you can use throught your page life.
 */
var project = {

    Demo: {
        init: function () { },
        handlers: {
            formpost: function (form) {
                //Action would be read from the form
                NKAjax.sendForm($(form), formPostSuccess);
                return false;
            },
            formPostSuccess: function (data, callback) {
                alert('Whoop Whoop');
                callback && callback();
            }
        }
    }
};

$(project.init)