(function ($, ssCore, ssEx) {

    window.themeSettings = {
        themeBreakpoint: 1024,
        isAccordionMenu: false
    }

    $(document).ready(function () {
        var responsiveAppSettings = {
            isEnabled: true,
            themeBreakpoint: window.themeSettings.themeBreakpoint,
            isSearchBoxDetachable: false,
            isHeaderLinksWrapperDetachable: false,
            doesDesktopHeaderMenuStick: true,
            doesScrollAfterFiltration: true,
            doesSublistHasIndent: true,
            displayGoToTop: true,
            hasStickyNav: true,
            selectors: {
                menuTitle: ".menu-title",
                headerMenu: ".header-menu",
                closeMenu: ".close-menu",
                sublist: ".header-menu .sublist",
                overlayOffCanvas: ".overlayOffCanvas",
                withSubcategories: ".with-subcategories",
                filtersContainer: ".nopAjaxFilters7Spikes",
                filtersOpener: ".filters-button span",
                navWrapper: ".responsive-nav-wrapper",
                navWrapperParent: ".responsive-nav-wrapper-parent",
                overlayEffectDelay: 300
            }
        };
		
		function headerSelectsPlacement() {
			var targetElement = $('.header-selectors-wrapper');
			var moveElement = function() {
				if($(window).outerWidth() > window.themeSettings.themeBreakpoint) {
					targetElement.appendTo('.header-upper .center').addClass('moved');
				}
				else {
					targetElement.insertAfter('.header-logo').removeClass('moved');
				}
			};
			moveElement();
			$(document).on('themeBreakpointPassed7Spikes', moveElement);
		}
		headerSelectsPlacement();
		
		$('.footlist .title').on('click', function() {
			if($(window).outerWidth() <= window.themeSettings.themeBreakpoint) {
				$(this).siblings('ul').slideToggle();
			}
		});

        ssEx.initResponsiveTheme(responsiveAppSettings);
    });

})(jQuery, sevenSpikesCore, sevenSpikesEx);