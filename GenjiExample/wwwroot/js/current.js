var fixed_menu = true;
window.jQuery = window.$ = jQuery;




/************** Custom Scripts ******************/

function calculateScroll() {
	"use strict";
	var contentTop      =   [];
	var contentBottom   =   [];
	var winTop      =   $(window).scrollTop();
	var rangeTop    =   200;
	var rangeBottom =   500;
	$('.navmenu li.scrollable').find('a').each(function(){
		contentTop.push( $( $(this).attr('href') ).offset().top );
		contentBottom.push( $( $(this).attr('href') ).offset().top + $( $(this).attr('href') ).height() );
	})
	$.each( contentTop, function(i){
		if ( winTop > contentTop[i] - rangeTop && winTop < contentBottom[i] - rangeBottom ){
			$('.navmenu li.scrollable')
			.removeClass('active')
			.eq(i).addClass('active');
			
			$('#top .navmenu li').first().addClass('active');
			jQuery('.mobile_menu_wrapper').css({'display' : 'none'});			
		}
	})
};

function block_height() {  
   if (jQuery(window).width() > 991) {
	  $(".block-wrap").each(function () {
		  var mainDivs = $(this).find(".block");
		  var maxHeight = 0;
		  for (var i = 0; i < mainDivs.length; ++i) {
			if (maxHeight < $(mainDivs[i]).innerHeight()) { 
			  maxHeight = $(mainDivs[i]).innerHeight(); 
			}
		  }
		  for (var i = 0; i < mainDivs.length; ++i) {
			$(mainDivs[i]).css({'min-height': maxHeight + 'px'});
		  }
	  });
	}else {
		$(".block-wrap").each(function () {
		  var mainDivs = $(this).find(".block");
		  for (var i = 0; i < mainDivs.length; ++i) {
			$(mainDivs[i]).css({'min-height':'auto'});
		  }
	  });
	};
};

//menu logo center
function menu_update() {
	"use strict";
	var topHeader = $('#top').innerHeight(); 
	$('.mobile_menu_wrapper').css({'top': topHeader + 'px'});
};

//full-width slider
function fullwidthslider() {
	"use strict";
	var full_slider_w = jQuery(window).width();
	var topHeader = $('#top').innerHeight(); 
	if (jQuery(window).width() > 789) {
		var full_slider_h = jQuery(window).height();
		$('#home').css({'margin-top': '0px'});
	} 
	else {
		$('#home').css({'margin-top': topHeader  + 'px'});
		var full_slider_h = jQuery(window).width()*0.7;
	}
	
	var heightTop = $('#top').innerHeight(); 
	jQuery('.full_slider .flexslider, .full_slider .flexslider .slides li').css({ 'height': full_slider_h - heightTop});	
	jQuery('.full_slider, .full_slider .flexslider .slides li img.slide_bg').attr('style', 'height: '+full_slider_h - heightTop+'px');		
	jQuery('.full_slider').css({'min-height': full_slider_h - heightTop + 'px'});
};

jQuery(document).ready(function() {
	"use strict";
	
	block_height();
	fullwidthslider();
	calculateScroll();
	
	
	
	//Fixed Menu
	if (jQuery('.fixed-menu').size() && fixed_menu == true) {		
		jQuery('.fixed-menu').append('<div class="fixed-menu-wrapper">'+jQuery('#top header').html()+'</div>');
		jQuery('.fixed-menu').find('.menu').children('li').each(function(){
			jQuery(this).children('a').append('<div class="menu_fadder"/>');
		});
		var fixd_menu = setInterval(scrolled_menu, 10);
		$('.fixed-menu .navmenu li').first().removeClass('active');
	}
			
	
	//MobileMenu
	jQuery('#top header').append('<a href="javascript:void(0)" class="menu_toggler"/>');
	jQuery('#top').append('<div class="mobile_menu_wrapper"><div class="mobile_menu container"/></div>');	
	jQuery('.mobile_menu').html(jQuery('#top header').find('.navmenu').html());
	jQuery('.mobile_menu_wrapper').hide();
	jQuery('.menu_toggler').click(function(){
		jQuery('.mobile_menu_wrapper').slideToggle(300);
	});
		
	$(window).scroll(function(event) {
		calculateScroll();
	});
	
	// link scroll
	$('.navmenu ul li.scrollable a, .mobile_menu ul li.scrollable a, .mouse, .next_section, #logo a, .go_section').click(function() {  	
		if (jQuery(window).width() < 767) {
			$('html, body').animate({scrollTop: $(this.hash).offset().top - jQuery('#logo').height() - 10}, 1000);
		} 
		else {
			$('html, body').animate({scrollTop: $(this.hash).offset().top - jQuery('#logo').height() - 30}, 1000);
		}
		return false;
	});
	
	$('ul.portfolio-menu').on('click', 'li:not(.active)', function() {  
		$(this).addClass('active').siblings().removeClass('active')
	});
	
	
	// carousel
	$('.carousel-box').each(function() {
		var owl = $(this).find('.owl-carousel');
		
		if($(this).hasClass('carousel-full')) {
			owl.owlCarousel({
				loop:true,
				margin:30,
				items:1
			});
		}else {
			owl.owlCarousel({
				loop:true,
				margin:30,
				responsive:{
					0:{items:1},
					550:{items:2},
					767:{items:3}
				}
			});
		};
		// Custom Navigation Events
		$(this).find('.btn-next').on('click',function(){
			owl.trigger('next');
		})
		$(this).find('.btn-prev').on('click',function(){
			owl.trigger('prev');
		})
	});
	
	


/* Contact Form */	
jQuery('.contact-form').submit(function () {
      var $this = $(this),
          result = true;

      if(!checkEmpty($this.find('#fname'))){
        result=false;
      }
      if(!validateEmail($this.find('#email'))) {
        result=false;
      }
      if(!checkEmpty($this.find('#mssg'))) {
        result=false;
      }
      
      if(result==false) {
        return false;
      }

      var $btn = $("#send").button('loading');

      var data = $this.serialize();

      $.ajax({
          url: "sender.php", 
          type: "POST",        
          data: data,     
          cache: false,
          success: function (html) {
          	console.log(html);
              if (html==1) {
                  $('#result-message').addClass('alert alert-success').html('<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button><strong>Success!</strong> Message Send. We will contact with you soon.').delay(500).slideDown(500).delay(10000).slideUp('slow');

                  $btn.button('reset');
                  
              } else {
                  $('#result-message').addClass('alert alert-danger').html('<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button><strong>Error!</strong> Message Sending Error! Please try again').delay(500).slideDown(500).delay(10000).slideUp('slow');
                  $btn.button('reset');
              }
          },
          error: function (a, b) {
            if (b == 'error') {
              $('#result-message').addClass('alert alert-danger').html('<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button><strong>Error!</strong> Message Sending Error! Please try again').delay(500).slideDown(500).delay(10000).slideUp('slow');
            };
            $btn.button('reset');
          }
      });

      return false;
    });



	
	// contact form
	$("#ajax-contact-form").submit(function() {
		var str = $(this).serialize();		
		$.ajax({
			type: "POST",
			url: "contact_form/contact_process.php",
			data: str,
			success: function(msg) {
				// Message Sent - Show the 'Thank You' message and hide the form
				if(msg == 'OK') {
					var result = '<div class="notification_ok">Your message has been sent. Thank you!</div>';
					$("#fields").hide();
				} else {
					var result = msg;
				}
				$('#note').html(result);
			}
		});
		return false;
	});
	$('#ajax-contact-form .shortcode_button').on('click', function() {  
		setTimeout(block_height, 100);
	});
	
	
	//Iframe transparent
	$("iframe").each(function(){
		var ifr_source = $(this).attr('src');
		var wmode = "wmode=transparent";
		if(ifr_source.indexOf('?') != -1) {
		var getQString = ifr_source.split('?');
		var oldString = getQString[1];
		var newString = getQString[0];
		$(this).attr('src',newString+'?'+wmode+'&'+oldString);
		}
		else $(this).attr('src',ifr_source+'?'+wmode);
	});
	
	
	// prettyPhoto
	$("a[rel^='prettyPhoto'], .prettyPhoto").prettyPhoto();	
	$(".not_control").prettyPhoto({
		theme: 'pp_default',
		social_tools: '<div class="pp_social" style="display:none;"></div>'
	});
	
	$('a[data-rel]').each(function() {
		$(this).attr('rel', $(this).data('rel'));
	});
	
	
	jQuery('.full_slider .flexslider li img.slide_bg').each(function(){
		jQuery(this).parent().attr('style', 'background-image:url('+$(this).attr('src')+');');		
	});
	
	jQuery('.flexslider').flexslider({
		animation: "fade",
		controlNav: false,
		directionNav: true,
		slideshowSpeed: 4000,
    	animationSpeed: 1400,
		pauseOnHover: false
    });
	
	menu_update();	
	
	jQuery(window).resize(function(){
		block_height();
		fullwidthslider();
	});
});

jQuery(window).load(function(){
	"use strict";
	block_height();
	menu_update();
	fullwidthslider();
});


jQuery(window).resize(function(){
	"use strict";
	block_height();
	menu_update();	
	fullwidthslider();
});



function scrolled_menu() {
	"use strict";
	if (jQuery(window).scrollTop() > (jQuery(window).height() - jQuery('#logo').height() - 35)) {
		jQuery('.fixed-menu').addClass('fixed_show');
	} else {
		jQuery('.fixed-menu').removeClass('fixed_show');
	}
};


