/*

  Template Name: Subas Ecommerce Responsive Bootstrap Template
  Description: This is html5 template
  Author: codecarnival
  Version: 1.0
  Design and Developed by: codecarnival
  NOTE: If you have any note put here. 

*/
/*================================================
[  Table of contents  ]
================================================

    1. jQuery MeanMenu
    2. wow js active
    3. jQuery Nivo Slider (home-2)
    4. Slick Carousel 
        4.1 Active Slider - 1 (home-1)
        4.2 Active By Brand
        4.3 Active Featured Product
        4.4 Active Blog
        4.5 Active Blog 2
        4.6 Active Related Product
		4.7 Active Team Member
    5. Countdown
    6. ScrollUp
    7. Tooltip 
    8. Treeview active
    9. Price Slider
    10. Fancybox active
    11. Elevate Zoom active 
    12. single-product-zoom-image carousel
    13. Cart Plus Minus Button
    14. bootstrap accordion one open at a time
    15. Cart tab menu active
    16. Blog page manu dropdown 
    17. Background Toutube Video 
    18. STICKY sticky-header

======================================
[ End table content ]
======================================*/

(function($) {
    "use strict";

	/* ********************************************
		1. jQuery MeanMenu
	******************************************** */
    jQuery('nav#dropdown').meanmenu();

	/* ********************************************
		2. wow js active
	******************************************** */
    new WOW().init();

	/* ********************************************
		3. jQuery Nivo Slider (home-2)
	******************************************** */
    $('#nivoslider-2').nivoSlider({
        directionNav: true,
        animSpeed: 1000,
        effect: 'random',
        slices: 18,
        pauseTime: 8000,
        pauseOnHover: true,
        controlNav: true,
        prevText: '<i class="zmdi zmdi-long-arrow-up"></i>',
        nextText: '<i class="zmdi zmdi-long-arrow-down"></i>'
    });
    
	/* ********************************************
		4. Slick Carousel 
	******************************************** */

	/* -------------------------------------
			4.1 Active Slider - 1 (home-1)
	------------------------------------- */
    $('.active-slider-1').slick({
        autoplay: true,
        autoplaySpeed: 8000,
        speed: 1000,
        dots: true,
        slidesToShow: 1,
        slidesToScroll: 1,
        prevArrow: '<button type="button" class="arrow-prev"><i class="zmdi zmdi-long-arrow-left"></i></button>',
        nextArrow: '<button type="button" class="arrow-next"><i class="zmdi zmdi-long-arrow-right"></i></button>',
    });

	/*----------------------------
			4.2 Active By Brand
	------------------------------ */  
    $('.active-by-brand').slick({
        speed: 700,
        arrows: true,
        dots: false,
        slidesToShow: 4,
        slidesToScroll: 1,
        prevArrow: '<button type="button" class="arrow-prev"><i class="zmdi zmdi-long-arrow-left"></i></button>',
        nextArrow: '<button type="button" class="arrow-next"><i class="zmdi zmdi-long-arrow-right"></i></button>',
        responsive: [
            { breakpoint: 991, settings: { slidesToShow: 3 }  },
            { breakpoint: 767, settings: { slidesToShow: 1 }  },
            { breakpoint: 479, settings: { slidesToShow: 1 }  }
        ]
    });
    
	/*------------------------------------
			4.3 Active Featured Product
	----------------------------------- */  
    $('.active-featured-product').slick({
        speed: 700,
        arrows: true,
        dots: false,
        slidesToShow: 4,
        slidesToScroll: 1,
        prevArrow: '<button type="button" class="arrow-prev"><i class="zmdi zmdi-long-arrow-left"></i></button>',
        nextArrow: '<button type="button" class="arrow-next"><i class="zmdi zmdi-long-arrow-right"></i></button>',
        responsive: [
            {  breakpoint: 991,   settings: { slidesToShow: 3,  }  },
            {  breakpoint: 767,   settings: { slidesToShow: 1, }   },
            {  breakpoint: 479,   settings: { slidesToShow: 1, }   },
        ]
    });    

	/*----------------------------
			4.4 Active Blog
	------------------------------ */  
    $('.active-blog').slick({
        speed: 700,
        arrows: false,
        dots: false,
        slidesToShow: 3,
        slidesToScroll: 1,
        responsive: [
            {  breakpoint: 991,   settings: { slidesToShow: 2,  }  },
            {  breakpoint: 767,   settings: { slidesToShow: 1, }   },
            {  breakpoint: 479,   settings: { slidesToShow: 1, }   },
        ]
    });

	/*----------------------------
			4.5 Active Blog 2
	------------------------------ */ 
    $('.active-blog-2').slick({
        speed: 700,
        arrows: false,
        dots: false,
        slidesToShow: 2,
        slidesToScroll: 1,
        responsive: [
            {  breakpoint: 991,   settings: { slidesToShow: 2,  }  },
            {  breakpoint: 767,   settings: { slidesToShow: 1, }   },
            {  breakpoint: 479,   settings: { slidesToShow: 1, }   },
        ]
    });

	/*------------------------------------
			4.6 Active Related Product
	-------------------------------------- */
    $('.active-related-product').slick({
        speed: 700,
        arrows: false,
        dots: false,
        slidesToShow: 3,
        slidesToScroll: 1,
        responsive: [
            {  breakpoint: 991,   settings: { slidesToShow: 2,  }  },
            {  breakpoint: 767,   settings: { slidesToShow: 1, }   },
            {  breakpoint: 479,   settings: { slidesToShow: 1, }   },
        ]
    });

	/*----------------------------
			4.7 Active Team Member
	------------------------------ */
    $('.active-team-member').slick({
        speed: 700,
        arrows: false,
        dots: false,
        slidesToShow: 4,
        slidesToScroll: 1,
        responsive: [
            {  breakpoint: 991,   settings: { slidesToShow: 2,  }  },
            {  breakpoint: 767,   settings: { slidesToShow: 1, }   },
            {  breakpoint: 479,   settings: { slidesToShow: 1, }   },
        ]
    });

	/* ********************************************
		5. Countdown
	******************************************** */
    $('[data-countdown]').each(function() {
        var $this = $(this), finalDate = $(this).data('countdown');
        $this.countdown(finalDate, function(event) {
            $this.html(event.strftime('<span class="cdown days"><span class="time-count">%-D</span> <p>Days</p></span> <span class="cdown hour"><span class="time-count">%-H</span> <p>Hour</p></span> <span class="cdown minutes"><span class="time-count">%M</span> <p>Mint</p></span> <span class="cdown second"> <span><span class="time-count">%S</span> <p>Sec</p></span>'));
        });
    }); 

	/* ********************************************
		6. ScrollUp
	******************************************** */
	$.scrollUp({
		scrollText: '<i class="zmdi zmdi-chevron-up"></i>',
		easingType: 'linear',
		scrollSpeed: 900,
		animation: 'fade'
	}); 

	/* ********************************************
		7. Tooltip 
	******************************************** */
    $('[data-toggle="tooltip"]').tooltip(); 

	/* ********************************************
		8. Treeview active
	******************************************** */
    $("#cat-treeview ul").treeview({
        animated: "normal",
        persist: "location",
        collapsed: true,
        unique: true,
    });

    $("#cat-treeview-2 ul").treeview({
        animated: "normal",
        persist: "location",
        collapsed: true,
        unique: true,
    });

	/* ********************************************
		9. Price Slider
	******************************************** */
    $( "#slider-range" ).slider({
        range: true,
        min: 50,
        max: 2000,
        values: [ 50, 999 ],
        slide: function( event, ui ) {
            $( "#amount" ).val( "$" + ui.values[ 0 ] + " - $" + ui.values[ 1 ] );
        }
    });
    $( "#amount" ).val( "$" + $( "#slider-range" ).slider( "values", 0 ) +
    " - $" + $( "#slider-range" ).slider( "values", 1 ) ); 

	/* ********************************************
		10. Fancybox active
	******************************************** */
    $(document).ready(function() {
        $('.fancybox').fancybox();
    });

	/* ********************************************
		11. Elevate Zoom active 
	******************************************** */    
    $("#zoom_03").elevateZoom({
        constrainType: "height",
        zoomType: "lens",
        containLensZoom: true,
        gallery: 'gallery_01',
        cursor: 'pointer',
        galleryActiveClass: "active"
    });

    //pass the images to Fancybox
    $("#zoom_03").on("click", function(e) {
        var ez = $('#zoom_03').data('elevateZoom');
        $.fancybox(ez.getGalleryList());
        return false;
    });

	/* ********************************************
		12. single-product-zoom-image carousel
	******************************************** */ 
    $('.carousel-btn').slick({
        speed: 700,
        arrows: true,
        dots: false,
        slidesToShow: 4,
        slidesToScroll: 1,
        prevArrow: '<button type="button" class="arrow-prev"><i class="zmdi zmdi-long-arrow-left"></i></button>',
        nextArrow: '<button type="button" class="arrow-next"><i class="zmdi zmdi-long-arrow-right"></i></button>',
        responsive: [
            { breakpoint: 991, settings: { slidesToShow: 3 }  },
            { breakpoint: 767, settings: { slidesToShow: 3 }  },
            { breakpoint: 479, settings: { slidesToShow: 3 }  }
        ]
    });

	/* ********************************************
		13. Cart Plus Minus Button
	******************************************** */
    $(".cart-plus-minus").prepend('<div class="dec qtybutton">-</div>');
    $(".cart-plus-minus").append('<div class="inc qtybutton">+</div>');
    $(".qtybutton").on("click", function() {
        var $button = $(this);
        var oldValue = $button.parent().find("input").val();
        if ($button.text() == "+") {
            var newVal = parseFloat(oldValue) + 1;
        } 
        else {
            // Don't allow decrementing below zero
            if (oldValue > 0) {
                var newVal = parseFloat(oldValue) - 1;
            } 
            else {
                newVal = 0;
            }
        }
        $button.parent().find("input").val(newVal);
    });

	/* ********************************************
		14. bootstrap accordion one open at a time
	******************************************** */ 
    $('.payment-title a').on('click',function(e){
        if($(this).parents('.panel').children('.panel-collapse').hasClass('in')){
            e.stopPropagation();
        }
        // You can also add preventDefault to remove the anchor behavior that makes
        // the page jump
        e.preventDefault();
    });

	/* ********************************************
		15. Cart tab menu active
	******************************************** */  
    $('.cart-tab li a').on("click", function(){
        $(this).addClass("active");
        $(this).parent('li').prevAll('li').find('a').addClass("active");
        $(this).parent('li').nextAll('li').find('a').removeClass("active");
    });

	/* ********************************************
		16. Blog page manu dropdown 
	******************************************** */
    $('.dropdown .option-btn').on('click', function(){
        if($(this).siblings('.dropdown-menu').hasClass('active')){
            $(this).siblings('.dropdown-menu').removeClass('active').slideUp();
            $(this).removeClass('active');
        }
        else{
            $('.dropdown .dropdown-menu').removeClass('active').slideUp();
            $('.dropdown .option-btn').removeClass('active');
            $(this).addClass('active');
            $(this).siblings('.dropdown-menu').addClass('active').slideDown();
        }
    });

	/* ********************************************
		17. Background Toutube Video 
	******************************************** */
    $(".youtube-bg").YTPlayer({
		videoURL:"_OA2oggXehk",
		containment:'.youtube-bg',
		mute:true,
		loop:true,
	});



})(jQuery);




/* ********************************************
    18. STICKY sticky-header
******************************************** */
    $(window).scroll(function() {
        if ($(this).scrollTop() > 1){  
            $('#sticky-header').addClass("sticky");
        }
        else{
            $('#sticky-header').removeClass("sticky");
        }
    });
/* ********************************************************* */