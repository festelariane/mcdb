$(window).load(function() {
    // Animate loader off screen
    $(".loader").fadeOut("slow");  
});

$(document).ready(function() {
  $('.bxslider').bxSlider({
  	auto : true,
  	mode : 'fade',
  	speed : 3000,
  	easing : 'ease-in-out',
  	pager : false,
  	autoHover : false,
  	controls: false
  });

  $('.bxslider-product').bxSlider({
    auto : true,
    pagerCustom : '#bx-pager'
  });
});

$(function() {

  // Default
  jQuery.scrollSpeed(100, 800);

});

function openNav() {
    document.getElementById("slideNav").style.width = "100%";
    document.getElementById("slideNav").style.padding = "20px";
}

function closeNav() {
    document.getElementById("slideNav").style.width = "0";
    document.getElementById("slideNav").style.padding = "0";
}

$("[data-fancybox]").fancybox({
    // Options will go here
    loop : true,
    animationEffect : "fade"
});

