$(document).ready(function () {

    var updateResources = function () {
        updateResource("Clay");
        updateResource("Wheat");
        updateResource("Iron");
        updateResource("Wood");
    };

    var updateResource = function (resourceName) {
        var start = new Date();
        var currentProduction = 0;
        var currentValue = parseFloat($(".res-value." + resourceName).text());
        var update = Date.parse($(".res-update." + resourceName).text());

        var mines = $(".mines").find("." + resourceName);

        $.each(mines, function (index, value) {
            currentProduction += parseInt($(value).find(".hourProduction").text());

            console.log(currentProduction);
           
        });

        var nextValue = (currentValue + ((start.getTime() - update) / 1000 / 60 / 60) * currentProduction).toFixed(4);
        console.log(nextValue);
        console.log(currentValue);

        $(".res-value." + resourceName).text(nextValue);
        $(".res-update." + resourceName).text(start.strftime("%Y-%m-%d %H:%M:%S"));
    };

    setInterval(updateResources, 500);

});