$(window).load(function(){
    var $container = $('.portfolio-list');
   // init
   $container.isotope({
       itemSelector : '.item'
   });
 
   $('#filters').on( 'click', 'a', function() {
       var filterValue = $(this).attr('data-filter');
       $container.isotope({ filter: filterValue });
   });
});

jQuery(window).load(function(){		
   "use strict";	
   jQuery('.portfolio-list').isotope('reLayout');	
   setTimeout("jQuery('.portfolio-list').isotope('reLayout')", 300);		
});
jQuery(window).resize(function(){
   "use strict";
   jQuery('.portfolio-list').isotope('reLayout');
});
jQuery.fn.portfolio_addon = function(addon_options) {
   "use strict";
   //Set Variables
   var addon_el = jQuery(this),
       addon_base = this,
       img_count = addon_options.items.length,
       img_per_load = addon_options.load_count,
       $newEls = '',
       loaded_object = '',
       $container = jQuery('.portfolio-list');

   
   jQuery('.add-item').click(function(){
       $newEls = '';
       loaded_object = '';									   
       var loaded_images = $container.find('.added').size();
       if ((img_count - loaded_images) > img_per_load) {
           var now_load = img_per_load;
       } else {
           var now_load = img_count - loaded_images;
       }
       
       if ((loaded_images + now_load) == img_count) jQuery(this).fadeOut();

       if (loaded_images < 1) {
           var i_start = 1;
           $container.isotope('reLayout');
           
           
       } else {
           var i_start = loaded_images+1;
           $container.isotope('reLayout');
           
           
       }

       if (now_load > 0) {
           // load more elements
           for (var i = i_start-1; i < i_start+now_load-1; i++) {
               loaded_object = loaded_object + '<div class="item col-md-4 col-sm-4 col-xs-12 '+ addon_options.items[i].sortcategory +' added"><a href="'+ addon_options.items[i].zoomurl +'" class="prettyPhoto" data-rel="prettyPhoto[portfolio55]"><img src="'+ addon_options.items[i].src +'" alt="" /><div class="caption"><div class="info"><h4>'+ addon_options.items[i].title +'</h4><div class="separator"></div><p>'+ addon_options.items[i].description +'</p></div></div></a></div>';
           }			
               
               $newEls = jQuery(loaded_object);
               $container.isotope('insert', $newEls, function() {


               $("a[rel^='prettyPhoto'], .prettyPhoto").prettyPhoto();		
               $('a[data-rel]').each(function() {
                   $(this).attr('rel', $(this).data('rel'));
               });		
                   $('.portfolio-list').isotope('reLayout');	
               
           });			
       };
   });
}