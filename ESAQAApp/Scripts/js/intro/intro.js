function GetHelp() {
    +function ($) {
        $(function () {

            var intro = introJs();

            intro.setOptions({
                steps: [
                {
                    element: '.nav-user',
                    intro: '<p class="h4 text-uc"><strong>1: Quick Bar</strong></p><p>This is the notification, search and user information quick tool bar</p>',
                    position: 'bottom'
                },
                  {
                      element: '#nav header',
                      intro: '<p class="h4 text-uc"><strong>2: Language switch</strong></p><p>You can quick switch your local language here.</p>',
                      position: 'right'
                  },
                  {
                      element: '#nav-menu',
                      intro: '<p class="h4 text-uc"><strong>3: Left Menu</strong></p><p>Navigation page and business activity here</p>',
                      position: 'right'
                  },
                  {
                      element: '#nav footer',
                      intro: '<p class="h4 text-uc"><strong>4: Chat & Friends</strong></p><p>Start chat with your friend.</p>',
                      position: 'top'
                  }
                ],
                showBullets: true,
            });

            intro.start();
        });
    }(jQuery);
}