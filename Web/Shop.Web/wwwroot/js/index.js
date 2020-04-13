
//Obhvashtame celiq kod, zashtoto kodut inache za js e globalen i po tozi nachin moje da se poluchat na mesta suvpadeniq na imenata na promenlivite.
//Tuk garantirame che shtom stranicata se zaradi, shte se izpulni i tosi jquery bez da ima sblusuci na globalno nivo.
$(document).ready(function () {

    //Za da deistvame po design-a na div class-a theform ot html-a == ravno e na sushtoto otdolu samo che to e s jquery sintaksis.
    //var theform = document.getElementById("theform");
    //theform.hidden = true;

    //Sushtoto kato gore samo che s jquery sintaksis. $ oznachava jQuery , a puk # e za da markira classa- theform.
    var theform = $("#theform");
    theform.hide();


    var button = $("#buybutton");
    button.on("click", function () {
        alert("Buying item!");
    });

    var productsInfo = $(".product-props-li");
    productsInfo.on("click", function () {
        alert("You clicked on: " + $(this).text);
    });


    //id-to se turshi s #, a puk class elementite s . !!!!!!
    var $loginToggle = $("#LoginToggle");
    var $popupform = $(".Popup-form");

    $loginToggle.on("click", function () {
        $popupform.toggle(1000);
    })

    // v sintaksisa na javascript za da se oboznachi jquery se izpolzva $.
    //$.ajax
    //jQuery
});